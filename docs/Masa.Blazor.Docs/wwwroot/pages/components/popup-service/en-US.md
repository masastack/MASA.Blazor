---
title: Popup service
content: "Provides pop-up components such as global service invocation **Snackbar**, **Confirm** and **Prompt**."
tag: Service
related:
  - /blazor/components/dialogs
  - /blazor/components/snackbars
  - /blazor/components/enqueued-snackbars
---

## Examples

### Props

#### Snackbar

<masa-example file="Examples.components.popup_service.Snackbar"></masa-example>

<app-alert content="To customize the configuration of the enqueued snackbar, specify it in the **Program.cs**. It has the same configuration items as [PEnqueuedSnackbars](/blazor/components/enqueued-snackbars)."></app-alert>

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

#### Confirm

<masa-example file="Examples.components.popup_service.Confirm"></masa-example>

<app-alert content="To customize the configuration of the confirm, specify it in the **Program.cs**."></app-alert>

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

#### Prompt

<masa-example file="Examples.components.popup_service.Prompt"></masa-example>

<app-alert content="To customize the configuration of the prompt, specify it in the **Program.cs**."></app-alert>

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
