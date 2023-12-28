# SSR

<app-alert type='warning' content='The following content is only recommended in non-global interactive mode, although
there may be no error in global interactive mode, it is unnecessary and will generate redundant code.'><app-alert>

## Theme

To apply the theme in SSR, you need to add the following code to the `head` block of **App.razor**:

```razor
<MSsrThemeProvider />
```

<app-alert type='warning' content='If you need to switch themes dynamically, because of the limitations of SSR and the existing logic, we do not provide a feasible solution, it is recommended to use global interactive mode.'><app-alert>

## Delayed interaction

Blazor's SSR can be divided into the following four types:

- Static server-side rendering（Static SSR）
- Interactive server-side rendering（Interactive SSR）
- Client-side rendering（CSR）
- Automatic (Auto) rendering

When the page is first loaded, there is no specified interactive mode on the page or there is no interactive component
in the content, this page is a static server-side rendering in the traditional sense.

In Blazor, it means that there will be no websocket connection and no download of the Blazor WebAssembly runtime.

When interaction is needed, you can dynamically load or unload interactive components by updating the querystring.

### Example

For example, use the `interactive` parameter in the URL querystring to dynamically load or unload interactive component。

``` razor Home.razor
<a href="?interactive=true">Interactive component</a>

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

### Use built-in components

**MInteractiveTrigger** and **MInteractivePopup** provide encapsulation of the above features:

``` razor Home.razor
<MInteractiveTrigger QueryName="@nameof(Interactive)"
                     QueryValue="@Interactive"
                     InteractiveValue="true"
                     InteractiveComponentType="@typeof(InteractiveComponent)"
                     WithPopup
                     PopupStyle="z-index: 6;">
    Interactive component
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
