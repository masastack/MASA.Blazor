# Bidirectionality (LTR/RTL)

MASA Blazor 支持 RTL **(从右到左)** 语言，可以在引导应用程序时使用 **RTL** 选项激活。您可以在规范站点上找到有关 [双向性](https://material.io/design/usability/bidirectionality.html) 的更多信息。

```csharp Program.cs
services.AddMasaBlazor(options => {
    options.RTL = true;
})
```

您还可以随时通过修改 `MasaBlazor` 对象上的 **RTL** 值来动态更改此设置。


```razor
@inject MasaBlazor MasaBlazor

@code {
    private bool _rtl = false;

    private void ToggleRTL()
    {
        _rtl = !_rtl;
        MasaBlazor.RTL = _rtl;
    }
}
```
