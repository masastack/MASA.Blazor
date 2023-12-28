# 服务端渲染

<app-alert type='warning' content='以下内容仅推荐在非全局交互模式下使用。虽然全局交互下可能不会报错，但是没有必要且会生成冗余的代码。'><app-alert>

## 主题

在 SSR 中应用主题需要在 **App.razor** 的 `head` 代码块中添加以下代码：

```razor
<MSsrThemeProvider />
```

<app-alert type='warning' content='如需动态切换主题，因为SSR和现有逻辑的限制，我们不提供可行的方案，建议改用全局交互模式。'><app-alert>

## 延迟交互

Blazor 的 SSR 可以分为以下四种：

- 静态服务端渲染（Static SSR）
- 交互式服务端渲染（Interactive SSR）
- （交互式）客户端渲染（CSR）
- 自动（Auto）渲染

当初次加载时，页面上没有指定交互模式或内容没有任何交互组件时，此页面就是传统意义上的静态服务端渲染。

在 Blazor 中意味着不会有 websocket 连接，也不会下载 Blazor WebAssembly 的运行时。

当需要交互时，可以通过更新 querystring 的方式来动态加载或卸载交互组件。

### 示例

例如通过更新 URL querystring 的 `interactive` 参数来实现动态加载或卸载交互组件。

``` razor Home.razor
<a href="?interactive=true">交互窗口</a>

@if (Interactive)
{
    <InteractiveComponent />
}

@code {
    [SupplyParameterFromQuery(Name = "interactive")]
    public bool Interactive { get; set; }
}
```

``` razor InteractiveComponent.razor
@rendermode InteractiveServer

<div style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0, 0, 0, 0.5); z-index: 6;">
    <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: white; padding: 20px;">
        <h1>Interactive Component</h1>
        <button @onclick="Close">Close</button>
    </div>
</div>

@code {
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private void Close()
    {
        NavigationManager.NavigateTo(NavigationManager.GetUriWithQueryParameter("interactive", (bool?)null));
    }
}
```

### 使用内置的组件

**MInteractiveTrigger** 和 **MInteractivePopup** 提供以上功能的封装：

``` razor Home.razor
<MInteractiveTrigger QueryName="@nameof(Interactive)"
                     QueryValue="@Interactive"
                     InteractiveValue="true"
                     InteractiveComponentType="@typeof(InteractiveComponent)"
                     WithPopup
                     PopupStyle="z-index: 6;">
    交互窗口
</MInteractiveTrigger>

@code {
    [SupplyParameterFromQuery]
    public bool Interactive { get; set; }
}
```

``` razor InteractiveComponent.razor
@rendermode InteractiveServer
@inherits MInteractivePopup

<div style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0, 0, 0, 0.5);" @onclick="CloseAsync">
    <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: white; padding: 20px;" @onclick:stopPropagation>
        <h1>Interactive Component</h1>
        <button @onclick="@CloseAsync">Close</button>
    </div>
</div>
```
