---
title: Popup service（弹出层服务）
desc: "提供全局服务调用 **Snackbar**、**Confirm**、**Prompt**、**ProgressCircular** 和 **ProgressLinear**  弹出层组件。"
tag: 服务
related:
  - /blazor/components/dialogs
  - /blazor/components/snackbars
  - /blazor/components/enqueued-snackbars
---

## 组件

### 消息条

<masa-example file="Examples.components.popup_service.Snackbar"></masa-example>

> 如需自定义消息条的配置，请在 **Program.cs** 中指定。它的配置项与 [PEnqueuedSnackbars](/blazor/components/enqueued-snackbars) 相同。

```cs
services.AddMasaBlazor(options => 
{
    options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
    {
        {
            PopupComponents.SNACKBAR, new Dictionary<string, object?>()
            {
                { nameof(PEnqueuedSnackbars.Closeable), true },
                { nameof(PEnqueuedSnackbars.Position), SnackPosition.TopRight }
            }
        }
    };
})
```

### 确认

<masa-example file="Examples.components.popup_service.Confirm"></masa-example>

> 如需自定义确认框的配置，请在 **Program.cs** 中指定。

```cs
services.AddMasaBlazor(options => 
{
    options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
    {
        {
            PopupComponents.CONFIRM, new Dictionary<string, object?>()
            {
                {
                    nameof(PromptOptions.OkProps), (Action<ModalButtonProps>)(u =>
                    {
                        u.Class = "text-capitalize";
                        u.Text = false;
                    })
                },
                { nameof(ConfirmOptions.CancelProps), (Action<ModalButtonProps>)(u => u.Class = "text-capitalize") },
            }
        }
    };
})
```

### 输入确认

<masa-example file="Examples.components.popup_service.Prompt"></masa-example>

> 如需自定义输入确认框的配置，请在 **Program.cs** 中指定。

```cs
services.AddMasaBlazor(options => 
{
    options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
    {
        {
            PopupComponents.PROMPT, new Dictionary<string, object?>()
            {
                {
                    nameof(PromptOptions.OkProps), (Action<ModalButtonProps>)(u =>
                    {
                        u.Class = "text-capitalize";
                        u.Text = false;
                    })
                },
                { nameof(PromptOptions.CancelProps), (Action<ModalButtonProps>)(u => u.Class = "text-capitalize") },
            }
        }
    };
})
```

### 进度

封装了 [MCircleProgress](/blazor/components/progress-circle) 和 [MLinearProgress](/blazor/components/progress-linear) 组件。

<masa-example file="Examples.components.popup_service.Progress"></masa-example>

如果你想自定义方法名，可以使用扩展方法：

```cs
public static class PopupServiceExtensions
{
    public static void StartLoading(this IPopupService service)
    {
        service.ShowProgressCircular();
    }
    
    public static void StopLoading(this IPopupService service)
    {
        service.HideProgressCircular();
    }
}
```

然后你可以像这样使用它：

```razor
@inject IPopupService PopupService

@code
{
    public async Task FetchData()
    {
        await PopupService.StartLoading();
        await HttpClient.GetAsync();
        await PopupService.StopLoading();
    }
}
```

> 如需自定义进度的配置，请在 **Program.cs** 中指定。

```cs
services.AddMasaBlazor(options => 
{
    options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
    {
        {
            PopupComponents.PROGRESS_CIRCULAR, new Dictionary<string, object?>()
            {
                { nameof(ProgressCircularOptions.Color), "pink" },
            }
        },
        {
            PopupComponents.PROGRESS_LINEAR, new Dictionary<string, object?>()
            {
                { nameof(ProgressLinearOptions.Color), "pink" },
            }
        },
    };
})
```

## 高级用法

你可以使用 `IPopupService.OpenAsync` 打开你自己的组件。
这里有一些必要的步骤：

1. 你自己的弹出组件应该继承自 **PopupComponentBase**，它在 **Masa.Blazor.Popup.Components** 命名空间下。
2. 使用继承自 **PopupComponentBase** 的 `Visible` 属性来控制你的组件显示和关闭。
3. 当你关闭你的弹出组件时，需要调用 继承自 **PopupComponentBase** 的 `ClosePopupAsync` 方法。 这个方法的 `returnVal` 入参将会作为 `IPopupService.OpenAsync` 的返回值。
4. （可选）还有一个 `PopupOkEventArgs` 类，能帮你把关闭组件的权力留给用户。具体用法请查看 [Confirm 组件](https://github.com/masastack/MASA.Blazor/blob/cc2e3178db40c0d6bacbe9b66f8e371afbe4cba2/src/Masa.Blazor/Popup/Components/Confirm/Confirm.razor.cs#L69) 的源码。

下面是一个简单的弹出组件：**CustomPopupComponent**，它封装了 **MDialog** 和 **MAlert**组件：

```razor
@inherits Masa.Blazor.Popup.Components.PopupComponentBase

<MDialog Value="Visible" MaxWidth="360">
    <MAlert Class="mb-0" Type="@Type" Dismissible Value="Visible" ValueChanged="ValueChanged">@Content</MAlert>
</MDialog>

@code {
    [Parameter, EditorRequired]
    public string? Content { get; set; }

    [Parameter]
    public AlertTypes Type { get; set; }

    private async Task ValueChanged(bool _)
    {
        await ClosePopupAsync();
    }
}
```

<masa-example file="Examples.components.popup_service.Advance"></masa-example>
