---
title: Popup service（弹出层服务）
desc: "提供全局服务调用 **Snackbar**、**Confirm**、**Prompt** 弹出层组件。"
tag: 服务
related:
  - /blazor/components/dialogs
  - /blazor/components/snackbars
  - /blazor/components/enqueued-snackbars
---

## 使用

### 消息条

<masa-example file="Examples.components.popup_service.Snackbar"></masa-example>

<app-alert content="如需自定义消息条的配置，请在 **Program.cs** 中指定。它的配置项与 [PEnqueuedSnackbars](/blazor/components/enqueued-snackbars) 相同。"></app-alert>

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

<app-alert content="如需自定义确认框的配置，请在 **Program.cs** 中指定。"></app-alert>

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

<app-alert content="如需自定义输入确认框的配置，请在 **Program.cs** 中指定。"></app-alert>

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
