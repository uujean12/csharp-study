using System.Text.Json;
using AlgorithmDashboard.Models;

namespace AlgorithmDashboard.Services;

public class StudyRecordService(IWebHostEnvironment env)
{
    private readonly string _dataPath = Path.GetFullPath(
        Path.Combine(env.ContentRootPath, "..", "data", "records.json"));

    private Dictionary<DateOnly, StudyRecord>? _cache;

    public async Task<Dictionary<DateOnly, StudyRecord>> GetRecordsByDateAsync()
    {
        if (_cache is not null) return _cache;

        if (!File.Exists(_dataPath))
            return _cache = [];

        var json = await File.ReadAllTextAsync(_dataPath);
        var file = JsonSerializer.Deserialize<StudyRecordFile>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        _cache = (file?.Records ?? [])
            .Where(r => DateOnly.TryParse(r.Date, out _))
            .ToDictionary(r => DateOnly.Parse(r.Date));

        return _cache;
    }

    public void InvalidateCache() => _cache = null;
}
