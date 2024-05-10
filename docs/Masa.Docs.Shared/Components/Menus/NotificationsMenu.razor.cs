namespace Masa.Docs.Shared.Components;

public partial class NotificationsMenu
{
    private enum NotificationType
    {
        Release
    }

    private class NotificationItem
    {
        public NotificationItem(string title,
            string content,
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

        public string Content { get; init; }

        public NotificationType Type { get; init; }

        public string Href { get; init; }

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
    }

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

    protected override void OnParametersSet()
    {
        RefreshNotifications();
    }

    private void RefreshNotifications()
    {
        if (_previousCulture != Culture)
        {
            _previousCulture = Culture;
            _allNotifications = GetNotifications();
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

    private List<NotificationItem> GetNotifications()
    {
        return
        [
            new NotificationItem(
                "v1.5.1 Release",
                I18n.T("release-notifications.patch"),
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.5.1",
                "release notes",
                new DateOnly(2024, 5, 10)),
            new NotificationItem(
                "v1.5.0 Release",
                $"""
                 Highlights:
                 - **Button**: {I18n.T("release-notifications.v1_5_0.button")}
                 - **Card**: {I18n.T("release-notifications.v1_5_0.card")}
                 - **DataTable**: {I18n.T("release-notifications.v1_5_0.data-table")}
                 - **ListItem**: {I18n.T("release-notifications.v1_5_0.list-item")}
                 """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.5.0",
                "release notes",
                new DateOnly(2024, 5, 7)),
            new NotificationItem(
                "v1.4.2 Release",
                """
                Highlights:
                - **EnqueuedSnackbars**: Reset position when app bottom or top changed.
                - **BottomNavigation**: Reset the bottom of app to 0 if disposing.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.4.2",
                "release notes",
                new DateOnly(2024, 4, 22)),
            new NotificationItem(
                "v1.4.1 Release",
                """
                Highlights:
                - **Theme**: add support for OnError, OnInfo, OnSuccess, OnWarning, InverseSurface, InverseOnSurface and InversePrimary color roles.
                - **Snackbar**: Use reverse-surface color as the background color.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.4.1",
                "release notes",
                new DateOnly(2024, 4, 17)),
            new NotificationItem(
                "v1.4.0 Release",
                """
                We provides a best practice example of how to integrate MAUI hybrid with MASA Blazor. You can find the repository at [Masa.Blazor.MauiDemo](https://github.com/masastack/Masa.Blazor.MauiDemo).
                Highlights:
                - **Overlay**: add fade transition animation, use *block* scroll strategy, and update the bg color of scrim.
                - **PageStack**: new component that provides a container similar to a page stack, mainly for mobile.
                - **PageTabs**: add the `Closeable` state to tabs, hover to display the close button.
                - **Sortable**: new component for replacing the DragZone component.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.4.0",
                "release notes",
                new DateOnly(2024, 4, 7)),
            new NotificationItem(
                "v1.3.4 Release",
                """
                Highlights:
                - **ScrollToTarget**: new component that supports automatic scrolling to the specified element and highlighting of the active item.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.3.4",
                "release notes",
                new DateOnly(2024, 1, 26)),
            new NotificationItem(
                "v1.3.3 Release",
                """
                Highlights:
                - **Dialog**: fix the issue that dialog may not be clickable in low network speed scenarios.
                - **Image**: should show placeholder when set `Src` to null.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.3.3",
                "release notes",
                new DateOnly(2024, 1, 22)),
            new NotificationItem(
                "v1.3.2 Release",
                """
                Highlights:
                - **Theme**: support for more color roles.
                """,
                NotificationType.Release,
                "/blazor/getting-started/release-notes?v=v1.3.2",
                "release notes",
                new DateOnly(2024, 1, 9)),
        ];
    }
}