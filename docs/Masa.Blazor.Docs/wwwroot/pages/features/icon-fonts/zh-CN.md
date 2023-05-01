# 图标字体

开箱即用，MASA Blazor 支持4个流行的图标字库： [Material Design Icons](https://materialdesignicons.com/), [Material Icons](https://fonts.google.com/icons), [Font Awesome 4](https://fontawesome.com/v4.7.0/) 和 [Font Awesome 5](https://fontawesome.com/)。

## 使用

要更改字体库，选择一个预定义的图标集或提供自己的图标集。

```cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons); // 默认图标集，此处仅为了演示
})
;
```

``` razor
<MIcon Icon="@("mdi-home")" />
```

在上面的例子中，我们选择了 `MaterialDesignIcons` 作为默认的图标集。因为它是一个预定义的字体集，MASA Blazor 会自动加载它的别名。您可以在 [Github](https://github.com/masastack/MASA.Blazor/blob/main/src/Masa.Blazor/Icons)。

### 多个图标集

开箱即用，MASA Blazor 支持同时使用多个不同的图标集。下面的例子演示了如何将默认的图标字体更改为 Font Awesome (`fa`)，同时仍然通过使用前缀来访问原始的 Material Design Icons (`mdi`)：

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.FontAwesome);
});
```

``` razor
<MIcon Icon="@("fas fa-plus")"></MIcon> @*渲染一个FontAwesome图标*@
<MIcon Icon="@("mdi:mdi-minus")"></MIcon> @*渲染一个MDI图标*@
```

> 提供默认图标集的前缀（如 `mdi:`）是不必要的

## 安装图标字体

你需要包含指定的图标库（即使使用默认的图标来自 [Material Design Icons](https://materialdesignicons.com/)）。这可以通过包含 CDN 链接来完成。

> 在这个页面，“Material Icons”是指[官方的谷歌图标](https://fonts.google.com/icons)，而“Material Design Icons”是指[扩展的第三方库](https://materialdesignicons.com/)

### Material Design Icons

这是 MASA Blazor 使用的默认图标集。它支持使用构建过程或 CDN 链接进行本地安装。下面显示了如何将 CDN 链接添加：

:::: code-group
::: code-group-item Server
``` cshtml Pages/_Host.cshtml
<link href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">
```
:::
::: code-group-item WebAssembly
``` html wwwroot/index.html
<link href="https://cdn.masastack.com/npm/@mdi/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">
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
<link href="https://cdn.masastack.com/npm/materialicons/materialicons.css" rel="stylesheet">
```

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesign);
});
```

``` razor
<MIcon Icon="@("home")" />
```

### Font Awesome 5 Icons

``` html
<link href="https://cdn.masastack.com/npm/fontawesome/v5.0.13/css/all.css" rel="stylesheet">
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

## 自定义图标集

为了使用自定义的图标集作为默认的图标集，您还必须添加与组件使用的值相对应的必要别名。例如，我想使用 [Remix icon](https://remixicon.com/) 作为默认的字体集：

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
};

builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons("reimx", remixIconAliases);
});
```

## 扩展可用的别名

如果你正在开发自定义的 MASA Blazor 组件，你可以使用 `aliasesConfigure` 动作来利用 MASA Blazor 组件使用的相同功能。图标别名以 `$` 开头，后跟别名的名称，例如 `$support`。

``` cs Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureIcons(IconSet.MaterialDesignIcons, aliases => {
        aliaes.Complete = "mdi-check-circle-outline"; // 使用另一个图标
        aliases.UserDefined["support"] = "mdi-lifebuoy";
    });
});
```

``` razor
<MIcon Icon="@("$complete")" />
<MIcon Icon="@("$support")" />
```

## SVG

别名也支持 SVG 路径。

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
