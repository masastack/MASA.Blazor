# 开始使用 MASA Blazor

开始使用 MASA Blazor，构建功能丰富、快速的应用程序。

> MASA Blazor 基于 .NET 6.0 开发，请确保已安装 [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) 或者更高的版本。

## 自动安装 {updated-in=v1.3.0}

要快速入门，可以使用 MASA.Template 模板快速创建项目。

### 安装 MASA.Template 模板 

```shell
dotnet new install MASA.Template
```

[MASA.Template](https://github.com/masastack/MASA.Template) 提供了以下模板：

- `masablazor`: MASA Blazor Web App 模板 **（添加自v1.3.0）**
- `masablazor-server`: MASA Blazor Server 模板
- `masablazor-wasm`: MASA Blazor WebAssembly 模板
- `masablazor-empty-server`: MASA Blazor Server 空模板
- `masablazor-empty-wasm`: MASA Blazor WebAssembly 空模板
- `masablazor-pro-server`: MASA Blazor Pro Server 模板
- `masablazor-pro-wasm`: MASA Blazor Pro WebAssembly 模板
- `masablazor-maui`: MASA Blazor MAUI 模板
- `masablazor-wpf`: MASA Blazor WPF 模板
- `masablazor-photino`: MASA Blazor Photino 模板
- `masablazor-winform`: MASA Blazor Winform 模板

每个模板都提供了相应的模板选项，可以通过 `dotnet new <模板名称> -h` 查看。

例如 `masablazor-server` 提供了以下模板选项：

```shell
-c, --cdn                        使用CDN，否则使用本地资源。
                                   类型: bool
--no-restore                     如果指定，则在创建时跳过项目的自动还原。
                                   类型: bool
                                   默认: false
-f, --framework <net6.0|net7.0>  项目的目标框架。
                                   类型: choice
                                     net6.0  Target net6.0
                                     net7.0  Target net7.0
                                   默认: net7.0
```

> 如果你对模板不了解，请查阅 [dotnet new](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new) 文档。

### 创建项目

以 `masablazor-server` 模板为例，创建项目，并使用 `-o` 指定输出目录和项目名称。

```shell
dotnet new masablazor-server -o MasaBlazorApp
```

### 启动项目

一旦搭建完成，通过运行以下命令启动项目：

```shell
cd MasaBlazorApp
dotnet run
```

## 手动安装（Blazor Server/WebAssembly）

以 .NET 7 提供的空模板为例。

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

### 安装 Masa.Blazor

```shell
cd BlazorApp
dotnet add package Masa.Blazor
```

### 引入资源文件

:::: code-group
::: code-group-item Server
```cshtml Pages/_Host.cshtml l:2,4,7
<base href="~/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link href="css/site.css" rel="stylesheet" />
<link href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.server.js"></script>
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```
:::
::: code-group-item WebAssembly
```html wwwroot\index.html l:2,4,7
<base href="/" />
<link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
<link href="css/app.css" rel="stylesheet" />
<link href="https://cdn.masastack.com/npm/@mdi/font@7.1.96/css/materialdesignicons.min.css" rel="stylesheet">

<script src="_framework/blazor.webassembly.js"></script>
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```
:::
::::

### 添加全局 using

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

### 注入服务

```csharp Program.cs
builder.Services.AddMasaBlazor();
```

### 设置布局

```razor MainLayout.razor
<MApp>
    <MMain> 
        @Body
    </MMain>
</MApp>
```

## 手动安装（Blazor Web App） {released-on=v1.1.0}

> 要求 Masa.Blazor 最小版本为 `1.1.0` 和 .NET 版本最小为 `8.0`。

### 全局交互

在根级别应用交互性。

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

#### 安装 Masa.Blazor

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

#### 添加全局 using

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

#### 引入资源文件

:::: code-group
::: code-group-item Auto
```razor BlazorApp\Components\App.razor l:5,8,16
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="_content/Masa.Blazor/css/masa-blazor.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="BlazorApp.styles.css" />
    <link rel="stylesheet" href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css">
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet @rendermode="InteractiveAuto" />
</head>

<body>
    <Routes @rendermode="InteractiveAuto" />
    <script src="_framework/blazor.web.js"></script>
    <script src="_content/BlazorComponent/js/blazor-component.js"></script>
</body>
```
:::
::: code-group-item Server
```razor Components\App.razor l:5,8,16
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="_content/Masa.Blazor/css/masa-blazor.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="BlazorApp.styles.css" />
    <link rel="stylesheet" href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css">
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet @rendermode="InteractiveServer" />
</head>

<body>
    <Routes @rendermode="InteractiveServer" />
    <script src="_framework/blazor.web.js"></script>
    <script src="_content/BlazorComponent/js/blazor-component.js"></script>
</body>
```
:::
::: code-group-item WebAssembly
```razor BlazorApp\Components\App.razor l:5,8,16
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="_content/Masa.Blazor/css/masa-blazor.min.css" />
    <link rel="stylesheet" href="app.css" />
    <link rel="stylesheet" href="BlazorApp.styles.css" />
    <link rel="stylesheet" href="https://cdn.masastack.com/npm/@("@mdi")/font@7.1.96/css/materialdesignicons.min.css">
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet @rendermode="InteractiveWebAssembly" />
</head>

<body>
    <Routes @rendermode="InteractiveWebAssembly" />
    <script src="_framework/blazor.web.js"></script>
    <script src="_content/BlazorComponent/js/blazor-component.js"></script>
</body>
```
:::
::::

#### 注入服务

:::: code-group
::: code-group-item Auto
```csharp
// BlazorApp\Program.cs
// BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor();
```
:::
::: code-group-item Server
```csharp
// Program.cs
builder.Services.AddMasaBlazor();

```
:::
::: code-group-item WebAssembly
```csharp
// BlazorApp\Program.cs
// BlazorApp.Client\Program.cs
builder.Services.AddMasaBlazor();
```
:::
::::

#### 设置布局

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

### 每页/组件 {released-on=v1.3.0}

基于每页或每组件应用交互。

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

#### 安装 Masa.Blazor

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

#### 添加全局 using

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

#### 引入资源文件

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
    <script src="_content/BlazorComponent/js/blazor-component.js"></script>
</body>
```

#### 注入服务

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

#### 设置布局

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

## SSR {released-on=v1.3.0}

在使用每页/组件应用交互时，对于一些状态如主题、语言、RTL、布局等需要在客户端保存，MASA Blazor 提供了一些 SSR 组件来解决这些问题。

### 切换主题

当需要切换主题时，你需要将 **MSsrThemeProvider** 组件设置为可交互的。

```razor App.razor l:6-7
<head>
...

<HeadOutlet />

@*此示例使用Server交互模式*@
<MSsrThemeProvider @rendermode="InteractiveServer" />

</head>
```

然后在需要切换主题的地方注入 **MasaBlazorProvider**，调用 **ToggleTheme** 方法即可。

```razor 
@inject MasaBlazorProvider MasaBlazorProvider

@code {
    private void ToggleTheme()
    {
        MasaBlazorProvider.ToggleTheme();
    }
}
```

### 切换 RTL

在需要切换 RTL 的地方注入 **MasaBlazorProvider**，调用 **ToggleRtl** 方法即可。

```razor
@inject MasaBlazorProvider MasaBlazorProvider

@code {
    private void ToggleRtl()
    {
        MasaBlazorProvider.ToggleRtl();
    }
}
```

### 客户端状态

当你使用了 **MAppBar**、**MNavigationManager** 和 **MFooter** 组件时，MASA Blazor 自动计算 **MMain** 组件大小。
但这在 SSR 中无效，因为计算依赖 JavaScript，而服务端无法执行 JavaScript。

因此提供了以下组件来解决这些问题：

- **MSsrStateProvider** 组件用于在 SSR 中设置和保存客户端状态。必须是可交互的。
- **MSsrPageScript** 组件用于页面间跳转时恢复客户端状态。

另外还有一个脚本文件 `ssr-state-restore.js`，用于在初次加载时恢复客户端状态。

```razor App.razor l:4-8
<body>
    <Routes />

    @*初次加载时恢复客户端状态*@
    <script src="_content/Masa.Blazor/js/ssr-state-restore.js"></script>

    @*此示例使用Server交互模式*@
    <MSsrStateProvider @rendermode="InteractiveServer" />
    
    <script src="_framework/blazor.web.js"></script>
    ...
</body>
```

```razor MainLayout.razor l:14-15
<MApp>
    <MAppBar App>
        ...
    </MAppBar>
    <MNavigationManager App>
       ...
    </MNavigationManager>

    <MMain>
        @Body
    </MMain>
</MApp>

@*页面间跳转恢复客户端状态*@
<MSsrPageScript />
```

## 下一步

- 想要了解布局规则，请查看 [Application](/blazor/components/application)。
- 想要快速搭建布局，请查看 [预置布局](/blazor/getting-started/wireframes)。
- 更换默认图标集，请查看 [图标字体](/blazor/features/icon-fonts)。
- 使用内置的原子化CSS，请查看 [样式和动画](/blazor/styles-and-animations/border-radius)。
- 更多组件，请查看 [组件](/blazor/components/all)。
