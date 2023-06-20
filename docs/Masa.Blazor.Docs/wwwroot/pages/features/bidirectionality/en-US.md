# Bidirectionality (LTR/RTL)

MASA Blazor supports RTL **(right to left)** languages and can be activated by using the **rtl** option when bootstrapping your application. You can find additional information about [implementing bidirectionality](https://material.io/design/usability/bidirectionality.html) on the specification site.

```csharp Program.cs
services.AddMasaBlazor(options => {
    options.RTL = true;
})
```

You can also change this dynamically at any point by modifying the **rtl** value on the `MasaBlazor` object.

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
