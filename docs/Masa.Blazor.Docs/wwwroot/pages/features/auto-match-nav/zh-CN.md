# 导航和路由自动匹配

与 Blazor
提供的 [NavLink和NavMenu](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing#navlink-and-navmenu-components)
组件相似，MASA Blazor 提供的 **MBottomNavigation**、**MBreadcrumbs**、**MList** 和 **MTabs** 也提供了根据其子组件的 `Href`
是否与当前 URL 匹配来切换 `ActiveClass` CSS类。

## 使用

以 **MList** 组件为例，应用 `Routable` 属性开启路由自动匹配功能。使用 `Exact` 或 `MatchPattern` 属性设置匹配规则。

``` razor
<MList Routable Nav>
    <MListItem Href="/" ActiveClass="primary--text">Dashboard</MListItem>
    <MListItem Href="/user" Exact ActiveClass="primary--text">Userinfo</MListItem>
    <MListItem Href="/user/list" MatchPattern="/user/[^/]+" ActiveClass="primary--text">UserList</MListItem>
</MList>
```

| 属性               | 类型       | 说明                                                                                                                                                                              |
|------------------|----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Href**         | `string` | 跳转的链接。                                                                                                                                                                          |
| **Exact**        | `bool`   | 精确匹配。如果 Userinfo 没有使用精确匹配，当访问 _UserList(/user/list)_ 时，_Userinfo(/user)_ 和 UserList  项会同时激活。                                                                                    |
| **MatchPattern** | `string` | 自定义正则表达式匹配。试想一下当你在 _UserList(/user/list)_ 页面点击了某个用户的详情，页面跳转到了 _UserDetail(/user/detail/tom)_ 页面，而导航的激活项消失了，使用体验就不好了。通过配置 `MatchPattern`，即使访问用户详情，导航也会激活 _UserList(/user/list)_。 |
| **ActiveClass**  | `string` | 激活时应用的CSS类。                                                                                                                                                                     |

## 示例

下面是一个类似文档站点应用的布局示例，主要演示了路由匹配的功能：左侧导航和顶部的标签页组件会根据当前 URL 自动匹配激活项。

以下是示例有用到但没有展示的Page组件：

- Introduction.razor: `@page "/"`
- GettingStarted.razor: `@page "/getting-started"`
- Alerts.razor: `@page "/components/alerts"`
- Buttons.razor: `@page "/components/buttons"`

> 使用 **MListGroup** 需要设置 `Group` 属性，否则子项匹配成功后不会自动展开。`Group` 的值为一个 `List<string>` ，包含了该组的所有子项的 `Href`。

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


