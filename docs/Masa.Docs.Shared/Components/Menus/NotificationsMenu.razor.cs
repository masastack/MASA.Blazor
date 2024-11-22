using System.Net.Http.Json;

namespace Masa.Docs.Shared.Components;

public partial class NotificationsMenu
{
    private enum NotificationType
    {
        Release
    }

    private class NotificationItem
    {
        public NotificationItem()
        {
        }
        
        public NotificationItem(string title,
            Dictionary<string, string> content,
            NotificationType type,
            string href,
            string hrefAlias,
            DateOnly createdAt,
            bool read = false)
        {
            Title = title;
            Content = content;
            Type = type;
            Href = href;
            CreatedAt = createdAt;
            Read = read;
        }

        public string Title { get; init; }

        public Dictionary<string, string> Content { get; init; }

        public NotificationType Type { get; init; }

        public string Href { get; init; }
        
        public string? UpgradeGuide { get; init; }

        public DateOnly CreatedAt { get; init; }

        public bool Read { get; set; }

        public string Emoji => Type switch
        {
            NotificationType.Release => "🎉",
            _ => "✅"
        };

        public string HrefText => Type switch
        {
            NotificationType.Release => "release-notes",
            _ => "view-details"
        };

        public string GetContent(string key)
        {
            if (Content.TryGetValue(key, out var content))
            {
                return content;
            }

            return Content.First().Value;
        }
    }

    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = null!;

    [CascadingParameter(Name = "Culture")] public string Culture { get; set; } = null!;

    private bool _showArchived;
    private bool _menu;
    private string? _previousCulture;
    private UserStorage _userStorage = new();
    private List<NotificationItem> _allNotifications = new();

    private List<NotificationItem> UnreadNotifications => _allNotifications.Where(x => !x.Read).ToList();

    private List<NotificationItem> ReadNotifications => _allNotifications.Where(x => x.Read).ToList();

    private List<NotificationItem> CurrentNotifications => _showArchived ? ReadNotifications : UnreadNotifications;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GetNotifications();

            var userStorage = await LocalStorage.GetItemAsync<UserStorage>("masablazor@user");
            if (userStorage is null)
            {
                _userStorage = new();
            }
            else
            {
                _userStorage = userStorage;
                _userStorage.Notifications.Read.ForEach(u =>
                {
                    var notifi = _allNotifications.FirstOrDefault(n => n.Title == u);
                    if (notifi is not null)
                    {
                        notifi.Read = true;
                    }
                });

                StateHasChanged();
            }
        }
    }

    private void ToggleNotification(NotificationItem item)
    {
        item.Read = true;
        _userStorage.Notifications.Read.Add(item.Title);
        _ = LocalStorage.SetItemAsync("masablazor@user", _userStorage);
    }

    private void OnClick(NotificationItem item)
    {
        ToggleNotification(item);
        _menu = false;
    }

    private HttpClient? _httpClient;

    private async Task GetNotifications()
    {
        _httpClient ??= HttpClientFactory.CreateClient("masa-docs");

        var list = await _httpClient.GetFromJsonAsync<List<NotificationItem>>(
            "_content/Masa.Blazor.Docs/data/notifications.json");
        _allNotifications = (list ?? []).OrderByDescending(u => u.CreatedAt).ToList();
    }
}