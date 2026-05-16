using System.Text.Json.Serialization;

namespace AlgorithmDashboard.Models;

// ── progress.json ─────────────────────────────────────
public record ProgressFile
{
    [JsonPropertyName("items")]
    public List<AlgorithmProgress> Items { get; init; } = [];
}

public record AlgorithmProgress
{
    [JsonPropertyName("id")]       public string Id { get; init; } = "";
    [JsonPropertyName("concept")]  public string Concept { get; init; } = "";
    [JsonPropertyName("userCode")] public string UserCode { get; init; } = "";
    [JsonPropertyName("isCompleted")] public bool IsCompleted { get; init; }
    [JsonPropertyName("completedAt")] public string CompletedAt { get; init; } = "";
}

// ── records.json ──────────────────────────────────────
public record RecordsFile
{
    [JsonPropertyName("dates")]
    public List<DayRecord> Dates { get; init; } = [];
}

public record DayRecord
{
    [JsonPropertyName("date")]
    public string Date { get; init; } = "";

    [JsonPropertyName("completedAlgorithms")]
    public List<string> CompletedAlgorithms { get; init; } = [];
}

// ── Curriculum types ──────────────────────────────────
public record AlgorithmDef(
    string Id,
    string Title,
    string SolutionCode,
    string[] GradeKeywords);

public record Stage(string Title, List<AlgorithmDef> Algorithms);

// ── AI Chat ───────────────────────────────────────────
public record ChatMessage(string Role, string Content);
