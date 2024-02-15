using System.Collections.Concurrent;

namespace Merchandising.IntegrationTests.Models;

public class SharedContext
{
    private ConcurrentDictionary<string, object?> Objects { get; } = new();

    public void TryAdd(string key, object? value)
    {
        var isExist = Objects.TryGetValue(key, out var _);
        if (isExist)
        {
            Objects.Remove(key, out var _);
        }

        Objects.TryAdd(key, value);
    }

    public T? Get<T>(string key)
    {
        var isExist = Objects.TryGetValue(key, out var value);

        if (!isExist)
        {
            return default;
        }

        return (T)value;
    }
}