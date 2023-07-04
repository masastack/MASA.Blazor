
# Get started with MASA Blazor

Get started with MASA Blazor, building feature-rich and fast applications.

> MASA Blazor is developed based on .NET 6.0, please make sure that you have installed [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) or later.

## Automatic Installation

To get started quickly, you can use the Masa.Template template to quickly create a project.

### Install Masa.Template

```shell
dotnet new install Masa.Template::1.0.0-rc.2
```

[Masa.Template](https://github.com/masastack/MASA.Template) provides the following templates:

- `masablazor-server`: MASA Blazor Server template
- `masablazor-wasm`: MASA Blazor WebAssembly template
- `masablazor-empty-server`: MASA Blazor Server Empty template
- `masablazor-empty-wasm`: MASA Blazor WebAssembly Empty template
- `masablazor-pro-server`: MASA Blazor Pro Server template
- `masablazor-pro-wasm`: MASA Blazor Pro WebAssembly template
- `masablazor-maui`: MASA Blazor MAUI template
- `masablazor-wpf`: MASA Blazor WPF template
- `masablazor-photino`: MASA Blazor Photino template
- `masablazor-winform`: MASA Blazor Winform template

Every template provides the corresponding template options, which can be viewed by `dotnet new <template name> -h`.

For example, the `masablazor-server` template provides the following template options:

```shell
-c, --cdn                        Use CDN, otherwise use the local files.
                                   type: bool
--no-restore                     If specified, skips the automatic restore of the project on create.
                                   type: bool
                                   default: false
-f, --framework <net6.0|net7.0>  The target framework for the project.
                                   type: choice
                                     net6.0  Target net6.0
                                     net7.0  Target net7.0
                                   default: net7.0
```

> If you are not familiar with the template, you can refer to the [dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new) document.

### Create project

for example, create a project using the `masablazor-server` template, and use `-o` to specify the output directory and project name.

```shell
dotnet new masablazor-server -o MasaBlazorApp
```

### Run project

As soon as the project is built, run the following command to start the project:

```shell
cd MasaBlazorApp
dotnet run
```

## Manual Installation

Use the official template provided by Blazor to create a project, then install the Masa.Blazor NuGet package, and finally import the resource file.

### Create Blazor Server or Blazor WebAssembly project

Take the empty template provided with .NET 7.

:::: code-group
::: code-group-item Server
```shell
dotnet new blazorserver-empty -o BlazorApp
```
:::
::: code-group-item WebAssembly
```shell
dotnet new blazorwasm-empty -o BlazorApp
```
:::
::::

### Add Masa.Blazor NuGet package

```shell
cd BlazorApp
dotnet add package Masa.Blazor
```

### Add resource files

:::: code-group
::: code-group-item Server
```cshtml Pages/_Host.cshtml l:2,4,7
<base href="~/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link href="css/site.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.server.js"></script>
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```
:::
::: code-group-item WebAssembly
```html wwwroot\index.html l:2,4,7
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/@mdi/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.webassembly.js"></script>
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```
:::
::::

### Add global using

```razor _Imports.razor
@using BlazorComponent
@using Masa.Blazor
@using Masa.Blazor.Presets
```

```csharp _Imports.cs
global using BlazorComponent;
global using Masa.Blazor;
global using Masa.Blazor.Presets;
```

### Add services

```csharp Program.cs
builder.Services.AddMasaBlazor();
```

### Let's build!

```razor MainLayout.razor
<MApp>
    //layout
</MApp>
```

> The steps to run the project are the same as the automatic installation above.

## Next

- Want to know the layout rules, please see [Application](/blazor/components/application).
- Want to quickly build layouts, please see [Preset Layouts](/blazor/getting-started/wireframes).
- Want to change the default icon set, please see [Icon fonts](/blazor/features/icon-fonts).
- Use the built-in atomic CSS, please see [Styles and Animations](/blazor/styles-and-animations/border-radius).
- More components, please see [Components](/blazor/components/all).
