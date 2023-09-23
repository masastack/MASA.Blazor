# Bidirectionality (LTR/RTL)

MASA Blazor supports RTL **(right to left)** languages and can be activated by using the **rtl** option when bootstrapping your application. You can find additional information about [implementing bidirectionality](https://material.io/design/usability/bidirectionality.html) on the specification site.

```csharp Program.cs
services.AddMasaBlazor(options => {
    options.RTL = true;
})
```

You can change this setting dynamically through the `RTL` value on the `IThemeService` service.

<masa-example file="Examples.features.bidirectionality.ChangeBidirectionality"></masa-example>