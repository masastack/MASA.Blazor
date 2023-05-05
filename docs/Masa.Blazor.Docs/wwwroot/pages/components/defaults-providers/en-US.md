---
title: Defaults providers
desc: "The defaults provider allows you to provide specific default prop values to components in a section of your application."
related:
  - /blazor/components/enqueued-snackbars
  - /blazor/components/popup-service
---

## Usage

**MDefaultsProvider** 组件用于为其作用域内的组件提供默认道具。

<masa-example file="Examples.components.defaults_providers.Usage"></masa-example>

<app-alert content="If you want to provide specific default prop values globally, do it in **Program.cs**."></app-alert>

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
