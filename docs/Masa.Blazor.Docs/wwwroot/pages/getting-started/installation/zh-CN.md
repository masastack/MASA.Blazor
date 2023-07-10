# 开始使用 MASA Blazor

开始使用 MASA Blazor，构建功能丰富、快速的应用程序。

> MASA Blazor 基于 .NET 6.0 开发，请确保已安装 [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) 或者更高的版本。

## 自动安装

要快速入门，可以使用 Masa.Template 模板快速创建项目。

### 安装 Masa.Template 模板

```shell
dotnet new install Masa.Template::1.0.0-rc.2
```

[Masa.Template](https://github.com/masastack/MASA.Template) 提供了以下模板：

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

## 手动安装

使用Blazor官方提供的模板创建项目，然后安装 Masa.Blazor NuGet 包，最后引入资源文件。

### 创建 Blazor Server 或 Blazor WebAssembly 项目

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

### 安装 Masa.Blazor NuGet 包

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

### 开始构建

```razor MainLayout.razor
<MApp>
    //layout 
</MApp>
```

> 启动项目的步骤与上面的自动安装一致。

## 下一步

- 想要了解布局规则，请查看 [Application](/blazor/components/application)。
- 想要快速搭建布局，请查看 [预置布局](/blazor/getting-started/wireframes)。
- 更换默认图标集，请查看 [图标字体](/blazor/features/icon-fonts)。
- 使用内置的原子化CSS，请查看 [样式和动画](/blazor/styles-and-animations/border-radius)。
- 更多组件，请查看 [组件](/blazor/components/all)。
