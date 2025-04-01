using System.Text.Json;
using System.Text.Json.Serialization;

namespace Masa.Docs.Core.Services;

public class ExpiryLocalStorage(IJSRuntime jsRuntime) : LocalStorage(jsRuntime)
{
    public async Task SetExpiryItemAsync(string key, string value, TimeSpan expiry)
    {
        var expiryValue = new ExpiryValue(value, DateTime.UtcNow.Add(expiry));
        await SetItemAsync(key, expiryValue);
    }

    public async Task<string?> GetExpiryItemAsync(string key)
    {
        var jsonValue = await GetItemAsync(key);
        if (jsonValue == null)
        {
            return null;
        }

        var expiryValue = JsonSerializer.Deserialize<ExpiryValue>(jsonValue);
        if (expiryValue == null || expiryValue.IsExpired)
        {
            return null;
        }

        return expiryValue.Value;
    }

    public async Task SetExpiryItemAsync<T>(string key, T value, TimeSpan expiry)
    {
        var json = JsonSerializer.Serialize(value);
        await SetExpiryItemAsync(key, json, expiry);
    }

    public async Task<T?> GetExpiryItemAsync<T>(string key)
    {
        var expiryValue = await GetExpiryItemAsync(key);
        return expiryValue == null ? default : JsonSerializer.Deserialize<T>(expiryValue);
    }

    public async Task<string?> GetOrSetExpiryItemAsync(string key, Func<Task<string>> valueFactory, TimeSpan expiry)
    {
        var value = await GetExpiryItemAsync(key);
        if (value != null)
        {
            return value;
        }

        value = await valueFactory();
        await SetExpiryItemAsync(key, value, expiry);
        return value;
    }

    public async Task<T?> GetOrSetExpiryItemAsync<T>(string key, Func<Task<T>> valueFactory, TimeSpan expiry)
    {
        var value = await GetExpiryItemAsync(key);
        if (value != null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        var newValue = await valueFactory();
        await SetExpiryItemAsync(key, newValue, expiry);
        return newValue;
    }
}

public record ExpiryValue(string Value, DateTime ExpiredAt)
{
    [JsonIgnore]
    public bool IsExpired => DateTime.UtcNow > ExpiredAt;
}