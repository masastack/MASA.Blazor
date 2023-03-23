---
title: Defaults providers（默认值提供程序）
desc: "默认值提供程序允许您为应用程序某个部分中的组件提供特定的默认值。"
related:
  - /blazor/components/enqueued-snackbars
  - /blazor/components/popup-service
---

## 使用

<masa-example file="Examples.components.defaults_providers.Usage"></masa-example>

<app-alert content="如果要在全局范围内提供特定的默认属性值，请在 **Program.cs** 中提供。"></app-alert>

```cs
services.AddMasaBlazor(options => 
{
    options.Defaults = new Dictionary<string, IDictionary<string, object?>?>()
    {
        {
            nameof(MCard), new Dictionary<string, object?>()
            {
                { nameof(MCard.Elevation), (StringNumber)10 }
            }
        }
    };
}
```
