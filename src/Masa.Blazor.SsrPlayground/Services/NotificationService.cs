namespace Masa.Blazor.SsrPlayground.Services;

public class NotificationMockService
{
    private readonly List<NotificationItem> _notificationItems =
    [
        new NotificationItem()
        {
            Id = Guid.NewGuid(),
            Title = "🎉 New Component",
            Message =
                "<b>MInteractiveTrigger</b> is a new component that trigger a interactive component when clicked."
        },

        new NotificationItem()
        {
            Id = Guid.NewGuid(),
            Title = "🎉 New Component",
            Message =
                "<b>MInteractiveTriggers</b> is a new component that trigger a interactive component when clicked."
        },

        new NotificationItem()
        {
            Id = Guid.NewGuid(),
            Title = "🎉 New Component",
            Message =
                "<b>[MInteractivePopup]</b> A abstract class for components that use the MInteractiveTrigger or MInteractiveTriggers component as a trigger"
        }
    ];

    public async Task<IEnumerable<NotificationItem>> GetListAsync()
    {
        await Task.Delay(500);
        return _notificationItems;
    }

    public async Task AddItemAsync(NotificationItem item)
    {
        await Task.Delay(500);
        _notificationItems.Add(item);
    }

    public async Task RemoveItemByIdAsync(Guid id)
    {
        await Task.Delay(500);
        var item = _notificationItems.FirstOrDefault(u => u.Id == id);
        if (item is not null)
        {
            _notificationItems.Remove(item);
        }
    }
}
