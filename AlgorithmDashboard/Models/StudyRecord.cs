using System.Text.Json.Serialization;

namespace AlgorithmDashboard.Models;

public record StudyRecordFile
{
    [JsonPropertyName("records")]
    public List<StudyRecord> Records { get; init; } = [];
}

public record StudyRecord
{
    [JsonPropertyName("date")]
    public string Date { get; init; } = "";

    [JsonPropertyName("studyMinutes")]
    public int StudyMinutes { get; init; }

    [JsonPropertyName("notes")]
    public string Notes { get; init; } = "";

    [JsonPropertyName("algorithms")]
    public List<AlgorithmEntry> Algorithms { get; init; } = [];
}

public record AlgorithmEntry
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = "";

    [JsonPropertyName("title")]
    public string Title { get; init; } = "";

    [JsonPropertyName("category")]
    public string Category { get; init; } = "";

    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; } = [];

    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    [JsonPropertyName("complexity")]
    public Complexity Complexity { get; init; } = new();

    [JsonPropertyName("sourceFile")]
    public string SourceFile { get; init; } = "";
}

public record Complexity
{
    [JsonPropertyName("time")]
    public string Time { get; init; } = "";

    [JsonPropertyName("space")]
    public string Space { get; init; } = "";
}
