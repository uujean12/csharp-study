using System.Text.Json;
using AlgorithmDashboard.Models;

namespace AlgorithmDashboard.Services;

public class ProgressService(IWebHostEnvironment env)
{
    private readonly string _progressPath = Path.GetFullPath(
        Path.Combine(env.ContentRootPath, "..", "data", "progress.json"));
    private readonly string _recordsPath = Path.GetFullPath(
        Path.Combine(env.ContentRootPath, "..", "data", "records.json"));

    private static readonly JsonSerializerOptions Opts = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    private readonly SemaphoreSlim _lock = new(1, 1);

    // ── Progress ──────────────────────────────────────

    public async Task<List<AlgorithmProgress>> GetAllAsync()
    {
        if (!File.Exists(_progressPath)) return [];
        var json = await File.ReadAllTextAsync(_progressPath);
        return JsonSerializer.Deserialize<ProgressFile>(json, Opts)?.Items ?? [];
    }

    public async Task<AlgorithmProgress?> GetAsync(string id)
    {
        var all = await GetAllAsync();
        return all.FirstOrDefault(a => a.Id == id);
    }

    public async Task SaveProgressAsync(string id, string concept, string userCode)
    {
        await _lock.WaitAsync();
        try
        {
            var all = await GetAllAsync();
            var idx = all.FindIndex(a => a.Id == id);
            var item = idx >= 0 ? all[idx] : new AlgorithmProgress { Id = id };
            item = item with { Concept = concept, UserCode = userCode };
            if (idx >= 0) all[idx] = item; else all.Add(item);
            await WriteProgressAsync(all);
        }
        finally { _lock.Release(); }
    }

    public async Task MarkCompletedAsync(string id, string userCode)
    {
        await _lock.WaitAsync();
        try
        {
            var all = await GetAllAsync();
            var idx = all.FindIndex(a => a.Id == id);
            var today = DateOnly.FromDateTime(DateTime.Today).ToString("yyyy-MM-dd");
            var item = idx >= 0 ? all[idx] : new AlgorithmProgress { Id = id };
            item = item with { IsCompleted = true, CompletedAt = today, UserCode = userCode };
            if (idx >= 0) all[idx] = item; else all.Add(item);
            await WriteProgressAsync(all);
            await AddToRecordsAsync(id, today);
        }
        finally { _lock.Release(); }
    }

    private async Task WriteProgressAsync(List<AlgorithmProgress> items)
    {
        var json = JsonSerializer.Serialize(new ProgressFile { Items = items }, Opts);
        await File.WriteAllTextAsync(_progressPath, json);
    }

    // ── Records (contribution graph) ──────────────────

    public async Task<Dictionary<DateOnly, DayRecord>> GetRecordsByDateAsync()
    {
        if (!File.Exists(_recordsPath)) return [];
        var json = await File.ReadAllTextAsync(_recordsPath);
        var file = JsonSerializer.Deserialize<RecordsFile>(json, Opts) ?? new();
        return file.Dates
            .Where(d => DateOnly.TryParse(d.Date, out _))
            .ToDictionary(d => DateOnly.Parse(d.Date));
    }

    private async Task AddToRecordsAsync(string algorithmId, string dateStr)
    {
        RecordsFile records = new();
        if (File.Exists(_recordsPath))
        {
            var json = await File.ReadAllTextAsync(_recordsPath);
            records = JsonSerializer.Deserialize<RecordsFile>(json, Opts) ?? new();
        }

        var list = records.Dates.ToList();
        var idx = list.FindIndex(d => d.Date == dateStr);

        if (idx < 0)
        {
            list.Add(new DayRecord { Date = dateStr, CompletedAlgorithms = [algorithmId] });
        }
        else if (!list[idx].CompletedAlgorithms.Contains(algorithmId))
        {
            list[idx] = list[idx] with
            {
                CompletedAlgorithms = [.. list[idx].CompletedAlgorithms, algorithmId]
            };
        }

        records = records with { Dates = list };
        var outJson = JsonSerializer.Serialize(records, Opts);
        await File.WriteAllTextAsync(_recordsPath, outJson);
    }
}
