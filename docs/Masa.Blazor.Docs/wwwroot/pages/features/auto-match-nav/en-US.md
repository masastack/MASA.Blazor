# Automatic matching of navigation and route

Same as
the [NavLink and NavMenu](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing#navlink-and-navmenu-components),
the **MBottomNavigation**, **MBreadcrumbs**, **MList** and **MTabs** components provided by MASA Blazor also provide the
ability to switch the `ActiveClass` CSS class based on whether the `Href` of its child component matches the current
URL.

## Usage

For example, the **MList** component, use the `Routable` property to enable the automatic matching of navigation and
route. Use the `Exact` or `MatchPattern` property to set the matching rules.

``` razor
<MList Routable Nav>
    <MListItem Href="/" ActiveClass="primary--text">Dashboard</MListItem>
    <MListItem Href="/user" Exact ActiveClass="primary--text">Userinfo</MListItem>
    <MListItem Href="/user/list" MatchPattern="/user/[^/]+" ActiveClass="primary--text">UserList</MListItem>
</MList>
```

| Parameter        | Type     | Description                                                                                                                                                                                                                                                                                                                                                                                        |
|------------------|----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Href**         | `string` | The URL to jump.                                                                                                                                                                                                                                                                                                                                                                                   |
| **Exact**        | `bool`   | Exact match. If Userinfo is not used for exact matching, when _UserList(/user/list)_ is accessed, _Userinfo(/user)_ and UserList items will be activated at the same time.                                                                                                                                                                                                                         |
| **MatchPattern** | `string` | Custom regular expression matching. Imagine that when you click on the details of a user on the _UserList(/user/list)_ page, the page jumps to the _UserDetail(/user/detail/tom)_ page, and the active item of the navigation disappears, the user experience is not good. By configuring `MatchPattern`, even if you visit the user details, the navigation will activate _UserList(/user/list)_. |
| **ActiveClass**  | `string` | The CSS class applied when activated.                                                                                                                                                                                                                                                                                                                                                              |

## Example

The following is a layout example similar to the document site application, which mainly demonstrates the feature of route matching: the left navigation and the top tab components will automatically match the active item based on the current URL.

There are some Page components that are used in the example but not shown:

- Introduction.razor: `@page "/"`
- GettingStarted.razor: `@page "/getting-started"`
- Alerts.razor: `@page "/components/alerts"`
- Buttons.razor: `@page "/components/buttons"`

> Using **MListGroup** requires setting the `Group` property, otherwise the child item will not be automatically expanded after the match is successful. The value of `Group` is a `List<string>`, which contains the `Href` of all child items in the group.

```razor MainLayout.razor
@inherits LayoutComponentBase

<MApp>
    <MAppBar App>
        <MTabs Routable>
            <MTab Href="/getting-started" MatchPattern="^/$|/getting-started">Documentation</MTab>
            <MTab Href="/components/alerts" MatchPattern="/components/[^/]*">Components</MTab>
        </MTabs>
    </MAppBar>
    <MNavigationDrawer App>
        <MList Routable Nav>
            @foreach (var navItem in _navItems)
            {
                if (navItem.Children?.Any() is true)
                {
                    <MListGroup NoAction Group="@navItem.Group">
                        <ActivatorContent>
                            <MListItemIcon>
                                <MIcon>@navItem.Icon</MIcon>
                            </MListItemIcon>
                            <MListItemContent>
                                <MListItemTitle>@navItem.Name</MListItemTitle>
                            </MListItemContent>
                        </ActivatorContent>
                        <ChildContent>
                            @foreach (var subNavItem in navItem.Children)
                            {
                                <MListItem Href="@subNavItem.Url">
                                    <MListItemContent>
                                        <MListItemTitle>@subNavItem.Name</MListItemTitle>
                                    </MListItemContent>
                                </MListItem>
                            }
                        </ChildContent>
                    </MListGroup>
                }
                else
                {
                    <MListItem Href="@navItem.Url"
                               ActiveClass="primary--text"
                               Exact="@navItem.Exact"
                               MatchPattern="@navItem.MatchPattern">
                        <MListItemContent>
                            <MListItemTitle>@navItem.Name</MListItemTitle>
                        </MListItemContent>
                    </MListItem>
                }
            }
        </MList>
    </MNavigationDrawer>

    <MMain>
        <MContainer>
            @Body
        </MContainer>
    </MMain>
</MApp>
```

``` cs MainLayout.razor.cs
public partial class MainLayout
{
    private class NavItem
    {
        public string Name { get; set; }

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public List<NavItem>? Children { get; set; }

        public List<string>? Group => Children?.Select(x => x.Url)?.ToList();

        public bool Exact { get; set; }

        public string? MatchPattern { get; set; }

        public NavItem(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public NavItem(string name, string url, bool exact) : this(name, url)
        {
            Exact = exact;
        }

        public NavItem(string name, string url, string matchPattern) : this(name, url)
        {
            MatchPattern = matchPattern;
        }

        public NavItem(string name, string icon, List<NavItem> children)
        {
            Name = name;
            Icon = icon;
            Children = children;
        }
    }

    private List<NavItem> _navItems = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _navItems = new List<NavItem>()
            {
                new NavItem("Introduction", "/"),
                new NavItem("Get started", "/getting-started"),
                new NavItem("Components", "mdi-view-dashboard", new List<NavItem>()
                {
                    new NavItem("Alert", "/components/alerts"),
                    new NavItem("Button", "/components/buttons")
                })
            };

            StateHasChanged();
        }
    }
}
```


