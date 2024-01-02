# Icon Fonts

Out of the box, MASA Blazor supports 5 popular icon font libraries:
[Material Design Icons](https://materialdesignicons.com/),
[Material Icons](https://fonts.google.com/icons),
[Font Awesome 4](https://fontawesome.com/v4/icons/),
[Font Awesome 5](https://fontawesome.com/v5/search?m=free)
and [Font Awesome 6](https://fontawesome.com/v6/search?o=r&m=free).

## Usage

To change your font library, choose a pre-defined icon sets or provide your own.

```cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons); // This is already the default value - only for display purposes
});
```

``` razor
<MIcon Icon="@("mdi-home")" />
```

In the above examples we choose `MaterialDesignIcons` as the default icon set. Because it is a pre-defined font set, MASA Blazor will load its aliases automatically. You can view the pre-defined aliases on [Github](https://github.com/masastack/MASA.Blazor/blob/main/src/Masa.Blazor/Icons).

### Multiple icon sets

Out of the box, MASA Blazor supports the use of multiple different icon sets at the same time. The following example demonstrates how to change the default icon font to Font Awesome (`fa`) while still maintaining access to the original Material Design Icons (`mdi`) through the use of a prefix:

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.FontAwesome);
});
```

``` razor
@*This renders a FontAwesome icon, no prefix is required for the default icon set*@
<MIcon Icon="@("fas fa-plus")"></MIcon>
@*This renders a MDI icon, a prefix is required for non-default icon sets*@
<MIcon Icon="@("mdi:mdi-minus")"></MIcon>
```

| Icon Set              | Prefix | Example                 |
|-----------------------|--------|-------------------------|
| Material Design Icons | `mdi:` | `mdi:mdi-home`          |
| Material Icons        | `md:`  | `md:home`               |
| Font Awesome 4        | `fa4:` | `fa4:fa-home`           |
| Font Awesome 5        | `fa:`  | `fa:fas fa-house`       |
| Font Awesome 6        | `fa6:` | `fa6:fa-solid fa-house` |

> It is not necessary to provide a prefix (such as `mdi:`) for icons from the default icon set

## Installing icon fonts

You are required to include the specified icon library (even when using the default icons from [Material Design Icons](https://materialdesignicons.com/)). This can be done by including a CDN link.

> In this page “Material Icons” is used to refer to the [official google icons](https://fonts.google.com/icons) and “Material Design Icons” refers to the [extended third-party library](https://materialdesignicons.com/)

### Material Design Icons

This is the default icon set used by MASA Blazor. It supports local installation with a build process or a CDN link. The following shows how to add the CDN link:

:::: code-group
::: code-group-item Server
``` cshtml Pages/_Host.cshtml
<link href="https://cdn.jsdelivr.net/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">
```
:::
::: code-group-item WebAssembly
``` html wwwroot/index.html
<link href="https://cdn.jsdelivr.net/npm/@mdi/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">
```
:::
::::

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons); // This is already the default value - only for display purposes
})
;
```

### Material Icons

``` html
<link href="https://fonts.googleapis.com/css?family=Material+Icons|Material+Icons+Outlined|Material+Icons+Two+Tone|Material+Icons+Round|Material+Icons+Sharp" rel="stylesheet">
```

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesign);
});
```

``` razor
<MIcon Icon="@("home")" />
```

### Font Awesome 6 Icons {released-on=v1.3.1}

``` html
<link href="https://use.fontawesome.com/releases/v6.4.0/css/all.css" rel="stylesheet">
```

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.FontAwesome6);
});
```

``` razor
<MIcon Icon="@("fa-solid fa-home")" />
```

### Font Awesome 5 Icons

``` html
<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">
```

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.FontAwesome);
});
```

``` razor
<MIcon Icon="@("fas fa-home")" />
```

### Font Awesome 4 Icons

``` html
<link href="https://cdn.jsdelivr.net/npm/font-awesome@4.x/css/font-awesome.min.css" rel="stylesheet">
```

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.FontAwesome4);
});
```

``` razor
<MIcon Icon="@("fa-home")" />
```

## Offline resources

If you need to use icon fonts offline, you can visit the [MASA.Template](https://github.com/masastack/MASA.Template/tree/main/src/Content/BlazorServer/wwwroot/css) repository on Github to download them.

You can also create a temporary project by running the command `dotnet new masablazor-server -o TempApp` and then copy the **wwwroot/css** directory to your project.
For how to install the template, please refer to the [Installation](/blazor/getting-started/installation).

## Creating a custom icon set {updated-in=v1.3.1}

The DOM structure generated by different icon font libraries is different. MASA Blazor provides two delegates `CssFormatter` and `ContentFormatter` in the **IconAliases** class to set the rendering logic of icons.

The following is the rendering logic of the built-in icon font library:

| Icon Set              | Origin HTML                          | CssFormatter               | ContentFormatter |
|-----------------------|--------------------------------------|----------------------------|------------------|
| Material Design Icons | `<i class="mdi mdi-home"></i>`       | `icon => $"mdi {icon}"`    |                  |
| Material Icons        | `<i class="material-icons">home</i>` | `icon => "material-icons"` | `icon => icon`   |
| Font Awesome 4        | `<i class="fa fa-home"></i>`         | `icon => $"fa {icon}"`     |                  |
| Font Awesome 5        | `<i class="fas fa-house"></i>`       | `icon => icon`             |                  |
| Font Awesome 6        | `<i class="fa-solid fa-house"></i>`  | `icon => icon`             |                  |

### Create a full new icon set

In order to use a custom set as the default icon set, you must also add the necessary aliases that correspond to values used by components. For example, I'd like to use [Remix icon](https://remixicon.com/) as the default font set:

``` cs Program.cs
var remixIconAliases = new IconAliases()
{
    Complete = "ri-check-line",
    Cancel = "ri-close-circle-line",
    Close = "ri-close-line",
    Delete = "ri-delete-bin-line",
    Clear = "ri-close-circle-line",
    Success = "ri-checkbox-circle-line",
    Info = "ri-information-line",
    Warning = "ri-error-warning-line",
    Error = "ri-close-circle-line",
    Prev = "ri-arrow-left-s-line",
    Next = "ri-arrow-right-s-line",
    CheckboxOn = "ri-checkbox-line",
    CheckboxOff = "ri-checkbox-blank-line",
    CheckboxIndeterminate = "ri-checkbox-indeterminate-line",
    Delimiter = "ri-record-circle-line",
    Sort = "ri-arrow-up-s-line",
    Expand = "ri-arrow-down-s-line",
    Menu = "ri-menu-line",
    Subgroup = "ri-arrow-down-s-line",
    Dropdown = "ri-arrow-down-s-line",
    RadioOn = "ri-radio-button-line",
    RadioOff = "ri-checkbox-blank-circle-line",
    Edit = "ri-edit-line",
    RatingEmpty = "ri-star-line",
    RatingFull = "ri-star-fill",
    RatingHalf = "ri-star-half-line",
    Loading = "ri-loader-4-line",
    First = "ri-step-backward-line",
    Last = "ri-step-forward-line",
    Unfold = "ri-arrow-down-s-line",
    File = "ri-attachment-line",
    Plus = "ri-add-line",
    Minus = "ri-subtract-line",
    Copy = "ri-clipboard-line",
    GoBack = "ri-arrow-go-back-line",
    Search = "ri-search-line",
    FilterOn = "ri-arrow-down-s-line",
    FilterOff = "ri-arrow-up-s-line",
    CssFormatter = icon => icon,
};

builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons("reimx", remixIconAliases);
});
```

### Keep the built-in icons in the component unchanged

Taking Remix icon and iconfont as examples:

#### Remix

```csharp
// This example uses MaterialDesignIcons as the built-in icon set unchanged
var remixIconAlias = new MaterialDesignIconsAliases()
{
  CssFormatter = icon => icon.StartsWith("mdi") ? $"mdi {icon}" : icon;
};

builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons("remix", remixIconAlias);
});
```

``` razor
<MIcon>ri-arrow-go-back-line</MIcon>
```

#### iconfont (Alibaba Icon Library)

```csharp
var iconfontAliases = new MaterialDesignIconsAliases()
{
    Custom = icon => icon.StartsWith("mdi") ? $"mdi {icon}" : $"iconfont {icon}";
};

builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons("iconfont", iconfontAliases);
});
```

## Extending available aliases

If you are developing custom MASA Blazor components, you can use the `aliasesConfigure` action to utilize the same functionality that MASA Blazor components use. Icon aliases are referenced with an initial `$` followed by the name of alias, e.g. `$support`.

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases => {
        aliaes.Complete = "mdi-check-circle-outline"; // use another icon
        aliases.UserDefined["support"] = "mdi-lifebuoy";
    });
});
```

``` razor
<MIcon Icon="@("$complete")" />
<MIcon Icon="@("$support")" />
```

## SVG

Aliases also support SVG Path.

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases => {
        aliases.Complete = new SvgPath("M12 2C6.5 2 2 6.5 2 12S6.5 22 12 22 22 17.5 22 12 17.5 2 12 2M12 20C7.59 20 4 16.41 4 12S7.59 4 12 4 20 7.59 20 12 16.41 20 12 20M16.59 7.58L10 14.17L7.41 11.59L6 13L10 17L18 9L16.59 7.58Z");

        aliases.UserDefined["support"] = new SvgPath("M19.79,15.41C20.74,13.24 20.74,10.75 19.79,8.59L17.05,9.83C17.65,11.21 17.65,12.78 17.06,14.17L19.79,15.41M15.42,4.21C13.25,3.26 10.76,3.26 8.59,4.21L9.83,6.94C11.22,6.35 12.79,6.35 14.18,6.95L15.42,4.21M4.21,8.58C3.26,10.76 3.26,13.24 4.21,15.42L6.95,14.17C6.35,12.79 6.35,11.21 6.95,9.82L4.21,8.58M8.59,19.79C10.76,20.74 13.25,20.74 15.42,19.78L14.18,17.05C12.8,17.65 11.22,17.65 9.84,17.06L8.59,19.79M12,2A10,10 0 0,1 22,12A10,10 0 0,1 12,22A10,10 0 0,1 2,12A10,10 0 0,1 12,2M12,8A4,4 0 0,0 8,12A4,4 0 0,0 12,16A4,4 0 0,0 16,12A4,4 0 0,0 12,8Z");

        aliases.UserDefined["chartPie"] = new SvgPath[]
        {
            new("M2.25 13.5a8.25 8.25 0 018.25-8.25.75.75 0 01.75.75v6.75H18a.75.75 0 01.75.75 8.25 8.25 0 01-16.5 0z"),
            new("M12.75 3a.75.75 0 01.75-.75 8.25 8.25 0 018.25 8.25.75.75 0 01-.75.75h-7.5a.75.75 0 01-.75-.75V3z")
        };
    });
});
```

``` razor
<MIcon Icon="@("$complete")" />
<MIcon Icon="@("$support")" />
<MIcon Icon="@("$chartPie")" />
```
