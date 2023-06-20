---
title: Popup service
desc: "Provides pop-up components such as global service invocation **Snackbar**, **Confirm**, **Prompt**, **ProgressCircular** and **ProgressLinear**."
tag: Service
related:
  - /blazor/components/dialogs
  - /blazor/components/snackbars
  - /blazor/components/enqueued-snackbars
---

## Components

### Snackbar

<masa-example file="Examples.components.popup_service.Snackbar"></masa-example>

> To customize the configuration of the enqueued snackbar, specify it in the **Program.cs**. It has the same configuration items as [PEnqueuedSnackbars](/blazor/components/enqueued-snackbars).

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

### Confirm

<masa-example file="Examples.components.popup_service.Confirm"></masa-example>

> To customize the configuration of the confirm, specify it in the **Program.cs**.

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

### Prompt

<masa-example file="Examples.components.popup_service.Prompt"></masa-example>

> To customize the configuration of the prompt, specify it in the **Program.cs**.

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

### Progress

<masa-example file="Examples.components.popup_service.Progress"></masa-example>

If you want to customize the method name, you can use the extension method:

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

Then you can use it like this:

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

> To customize the configuration of the progress, specify it in the **Program.cs**.

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

## Advance

You can use open your own popup component using `IPopupSerivce.OpenAsync`.
Here are some necessary steps:

1. Your own popup component should inherit from the **PopupComponentBase** which is in the **Masa.Blazor.Popup.Components** namespace.
2. Use the `Visible` property inherited from **PopupComponentBase** to control whether your component opens or closes.
3. When you close the popup component, the `ClosePopupAsync` method inherited from **PopupComponentBase** should be called. The `returnVal` parameter in the `ClosePopupAsync` method will be the return value of `IPopupService.OpenAsync`.
4. (optional) There is also a `PopupOkEventArgs` class that helps you leave the power to close the popup component to the user. For details, check the source code for [Confirm component](https://github.com/masastack/MASA.Blazor/blob/main/src/Masa.Blazor/Popup/Components/Confirm/Confirm.razor.cs#L69).

Here is a simple popup component named **CustomPopupComponent** that encapsulates the **MDialog** and **MAlert** components:

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
