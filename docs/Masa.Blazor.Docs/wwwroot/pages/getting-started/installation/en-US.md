
# Get started with MASA Blazor

Get started with MASA Blazor, building feature-rich and fast applications.

> MASA Blazor is developed based on .NET 6.0, please make sure that you have installed [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) or later.

## Automatic Installation

To get started quickly, you can use the MASA.Template template to quickly create a project.

### Install MASA.Template {updated-in=v1.3.0}

```shell
dotnet new install MASA.Template
```

[MASA.Template](https://github.com/masastack/MASA.Template) provides the following templates:

- `masablazor`: MASA Blazor Web App template **(Added in v1.3.0)**
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

## Manual Installation (Blazor Server/WebAssembly)

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

### Install Masa.Blazor NuGet package

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
<script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```
:::
::: code-group-item WebAssembly
```html wwwroot\index.html l:2,4,7
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/@mdi/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.webassembly.js"></script>
<script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```
:::
::::

### Add global using

```razor _Imports.razor
@using Masa.Blazor
@using Masa.Blazor.Presets
```

```csharp _Imports.cs
global using Masa.Blazor;
global using Masa.Blazor.Presets;
```

### Add services

```csharp Program.cs
builder.Services.AddMasaBlazor();
```

### Build the layout

```razor MainLayout.razor
<MApp>
    <MMain>
        @Body
    </MMain>
</MApp>
```

## Manual Installation (Blazor Web App)

### Interactivity at root level {released-on=v1.1.0}

> Requires Masa.Blazor minimum version `1.1.0` and .NET minimum version `8.0`.

:::: code-group
::: code-group-item Auto
```shell
dotnet new blazor --empty --interactivity Auto --all-interactive -o BlazorApp
```
:::
::: code-group-item Server
```shell
dotnet new blazor --empty --interactivity Server --all-interactive -o BlazorApp
```
:::
::: code-group-item WebAssembly
```shell
dotnet new blazor --empty --interactivity WebAssembly --all-interactive -o BlazorApp
```
:::
::::

#### Install Masa.Blazor NuGet package

:::: code-group
::: code-group-item Auto
```shell
cd BlazorApp\BlazorApp.Client
dotnet add package Masa.Blazor
```
:::
::: code-group-item Server
```shell
cd BlazorApp
dotnet add package Masa.Blazor
```
:::
::: code-group-item WebAssembly
```shell
cd BlazorApp\BlazorApp.Client
dotnet add package Masa.Blazor
```
:::
::::

#### Add resource files

:::: code-group
::: code-group-item Auto
```razor BlazorApp\Components\App.razor l:2,5,8
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link rel="stylesheet" href="app.css" />
<link rel="stylesheet" href="BlazorApp.styles.css" />
<link href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.web.js"></script>
<script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```
:::
::: code-group-item Server
```razor Components\App.razor l:2,5,8
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link rel="stylesheet" href="app.css" />
<link rel="stylesheet" href="BlazorApp.styles.css" />
<link href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.web.js"></script>
<script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```
:::
::: code-group-item WebAssembly
```razor BlazorApp\Components\App.razor l:2,5,8
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link rel="stylesheet" href="app.css" />
<link rel="stylesheet" href="BlazorApp.styles.css" />
<link href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.web.js"></script>
<script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```
:::
::::

#### Add global using

```razor _Imports.razor
@using Masa.Blazor
@using Masa.Blazor.Presets
```

```csharp _Imports.cs
global using Masa.Blazor;
global using Masa.Blazor.Presets;
```

#### Add services

:::: code-group
::: code-group-item Auto
```csharp BlazorApp\Program.cs,BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor();
```
:::
::: code-group-item Server
```csharp Program.cs
builder.Services.AddMasaBlazor();
```
:::
::: code-group-item WebAssembly
```csharp BlazorApp\Program.cs,BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor();
```
:::
::::

#### Build the layout

```razor MainLayout.razor l:3-4,6-7
@inherits LayoutComponentBase

<MApp>
    <MMain>
        @Body
    </MMain>
</MApp>

<div id="blazor-error-ui">
    An unhandled error has occurred.
    <a href="" class="reload">Reload</a>
    <a class="dismiss">🗙</a>
</div>
```

### Interactivity on per page/component {released-on=v1.3.0}

Interactivity is applied on a per-page or per-component basis.


:::: code-group
::: code-group-item Auto
```shell
dotnet new blazor --empty --interactivity Auto -o BlazorApp
```
:::
::: code-group-item Server
```shell
dotnet new blazor --empty --interactivity Server -o BlazorApp
```
:::
::: code-group-item WebAssembly
```shell
dotnet new blazor --empty --interactivity WebAssembly -o BlazorApp
```
:::
::::

#### Install Masa.Blazor NuGet package

:::: code-group
::: code-group-item Auto
```shell
cd BlazorApp\BlazorApp.Client
dotnet add package Masa.Blazor
```
:::
::: code-group-item Server
```shell
cd BlazorApp
dotnet add package Masa.Blazor
```
:::
::: code-group-item WebAssembly
```shell
cd BlazorApp\BlazorApp.Client
dotnet add package Masa.Blazor
```
:::
::::

#### Add global using

```razor _Imports.razor
@using Masa.Blazor
@using Masa.Blazor.Presets
```

```csharp _Imports.cs
global using Masa.Blazor;
global using Masa.Blazor.Presets;
```

#### Add resource files

```razor App.razor l:5,8,11,17
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="_content/Masa.Blazor/css/masa-blazor.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="BlazorApp.styles.css" />
    <link rel="stylesheet" href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css">
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet />
    <MSsrThemeProvider />
</head>

<body>
    <Routes />
    <script src="_framework/blazor.web.js"></script>
    <script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
</body>
```

#### Add services

:::: code-group
::: code-group-item Auto
```csharp
// BlazorApp\Program.cs
// BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureSsr();
});
```
:::
::: code-group-item Server
```csharp
// Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureSsr();
});

```
:::
::: code-group-item WebAssembly
```csharp
// BlazorApp\Program.cs
// BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor(options => {
    options.ConfigureSsr();
});
```
:::
::::

#### Build the layout

```razor MainLayout.razor l:3-4,6-7
@inherits LayoutComponentBase

<MApp>
    <MMain>
        @Body
    </MMain>
</MApp>

<div id="blazor-error-ui">
    An unhandled error has occurred.
    <a href="" class="reload">Reload</a>
    <a class="dismiss">🗙</a>
</div>
```

## Next

- Want to know the layout rules, please see [Application](/blazor/components/application).
- Want to quickly build layouts, please see [Preset Layouts](/blazor/getting-started/wireframes).
- Want to change the default icon set, please see [Icon fonts](/blazor/features/icon-fonts).
- Use the built-in atomic CSS, please see [Styles and Animations](/blazor/styles-and-animations/border-radius).
- More components, please see [Components](/blazor/components/all).
