using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlgorithmDashboard.Models;

namespace AlgorithmDashboard.Services;

public class ClaudeService(IHttpClientFactory factory, IConfiguration config)
{
    private readonly string _apiKey = config["Claude:ApiKey"] ?? "";
    private readonly string _model  = config["Claude:Model"]  ?? "claude-sonnet-4-6";

    private static readonly JsonSerializerOptions Opts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public bool IsConfigured => !string.IsNullOrWhiteSpace(_apiKey);

    public async Task<string> AskAsync(
        string userMessage,
        string algorithmContext,
        List<ChatMessage> history)
    {
        if (!IsConfigured)
            return "⚠️ API 키가 설정되지 않았습니다. `AlgorithmDashboard/appsettings.json`의 `Claude:ApiKey`에 Anthropic API 키를 입력해주세요.";

        using var client = factory.CreateClient();
        client.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        var messages = history
            .Select(m => new { role = m.Role, content = m.Content })
            .Append(new { role = "user", content = userMessage })
            .ToArray();

        var body = new
        {
            model = _model,
            max_tokens = 1024,
            system = $"""
                당신은 C# 알고리즘 전문 튜터입니다.
                현재 학생이 '{algorithmContext}' 알고리즘을 공부 중입니다.
                - 간결하고 명확하게 한국어로 답변하세요.
                - 코드 예시는 C#으로 작성하세요.
                - 핵심 개념 위주로 설명하되, 필요시 단계별로 안내하세요.
                """,
            messages
        };

        try
        {
            var response = await client.PostAsJsonAsync(
                "https://api.anthropic.com/v1/messages", body, Opts);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return $"API 오류 ({response.StatusCode}): {err[..Math.Min(200, err.Length)]}";
            }

            var result = await response.Content.ReadFromJsonAsync<AnthropicResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Content?.FirstOrDefault()?.Text
                ?? "응답을 받지 못했습니다.";
        }
        catch (Exception ex)
        {
            return $"오류: {ex.Message}";
        }
    }

    private record AnthropicResponse(
        [property: JsonPropertyName("content")] List<ContentBlock>? Content);

    private record ContentBlock(
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("text")] string Text);
}
