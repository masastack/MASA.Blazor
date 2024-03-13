using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.SsrPlayground.Services;

/// <summary>
/// State management for the ssr app
/// </summary>
public class AppState
{
    private readonly NotificationMockService _notificationMockService;

    public AppState(NotificationMockService notificationMockService)
    {
        _notificationMockService = notificationMockService;
    }

    private readonly HashSet<EventCallback> _changeSubscriptions = new();

    public async Task AddNotificationItemAsync(NotificationItem item)
    {
        await _notificationMockService.AddItemAsync(item);
        await NotifyChangeSubscribersAsync();
    }

    public Task<IEnumerable<NotificationItem>> GetNotificationItemsAsync()
    {
        return _notificationMockService.GetListAsync();
    }

    public Task RemoveNotificationItemByIdAsync(Guid id)
    {
        return _notificationMockService.RemoveItemByIdAsync(id);
    }

    public void NotifyOnChange(EventCallback callback)
    {
        _changeSubscriptions.Add(callback);
    }

    private Task NotifyChangeSubscribersAsync()
        => Task.WhenAll(_changeSubscriptions.Select(s => s.InvokeAsync()));
}
