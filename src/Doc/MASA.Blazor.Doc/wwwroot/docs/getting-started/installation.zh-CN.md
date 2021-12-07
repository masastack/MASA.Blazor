---
order: 0
title: 安装
---

让我们从 MASA Blazor 开始吧，这是 .NET Core Blazor 框架的 UI 组件库，用于构建功能丰富、快速的应用程序。

> MASA 系列所有产品均基于.Net 6.0 开发,请确保已安装 [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0) 。

<video controls preload="none" poster="Install MasaBlazor">
      <source src="https://cdn.masastack.com/stack/images/website/masa-blazor/video.mp4" type="video/mp4">
</videos>

# Cli 创建

## 安装 Masa.Template 模板

MASA.Template,包含 MASA 系列所有项目模板。MASA Blazor 对应模板名为`masab`

```
dotnet new --install MASA.Template
```

## 创建项目

根据项目模板名创建项目，并指定输出目录，即项目的根文件夹。

```
dotnet new masab -o MasaBlazorApp
```

> 默认为 Server 模式，通过参数--Mode WebAssembly 创建 WebAssembly 模式项目。

## 启动项目

通过命令`cd MasaBlazorApp`切换到 MasaBlazorApp 目录下 。
`dotnet run`启动项目，根据程序输出打开对应网址，即可看到 MASA Blazor 模板项目的效果。

# 手动创建

## 创建一个 Blazor Server 项目

在命令行界面中创建名为 `BlazorApp` 的新 Blazor 应用:

```sh
$ dotnet new blazorserver -o BlazorApp
```
或
```sh
$ dotnet new blazorwasm -o BlazorApp
```

> `blazorserver`为 Blazor Server App 短名称。`blazorwasm`为 Blazor WebAssembly App 短名称

## 安装 NuGet 包

```sh
$ dotnet add package MASA.Blazor
```

## 引入资源文件

### Blazor Server

在 `Pages/_Host.cshtml` 中引入资源文件：

```html
<!--masa blazor css style-->
<link href="_content/MASA.Blazor/css/masa-blazor.css" rel="stylesheet" />
<link href="_content/MASA.Blazor/css/masa-extend-blazor.css" rel="stylesheet" />
<!--icon file,import need to use-->
<link href="https://cdn.jsdelivr.net/npm/@("@mdi")/font@6.x/css/materialdesignicons.min.css"
rel="stylesheet">
<link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet" />
<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet" />
<!--js(should lay the end of file)-->
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```

### Blazor WebAssembly

在`wwwroot\index.html`中引入资源文件：

```html
<link href="_content/MASA.Blazor/css/masa-blazor.css" rel="stylesheet" />
<link href="_content/MASA.Blazor/css/masa-extend-blazor.css" rel="stylesheet" />
<link
  href="https://cdn.jsdelivr.net/npm/@mdi/font@6.x/css/materialdesignicons.min.css"
  rel="stylesheet"
/>
<link href="https://fonts.googleapis.com/css2?family=Material+Icons" rel="stylesheet" />
<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet" />
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```

## 注入相关服务

在 Program.cs 中添加 MASA.Blazor 相关服务：

```c#
// Add services to the container.
builder.Services.AddMasaBlazor();
```

## 全局引用

修改 `_Imports.razor` 文件,添加以下内容:

```c#
@using MASA.Blazor
```

修改 `Shared/MainLayout.razor` 文件，设置 MApp 为根元素：

```html
<MApp> //layout </MApp>
```

## 使用

参考官方文档[组件](https://masa-blazor-docs-dev.lonsid.cn/components/application),加入相关组件。
`dotnet run`启动项目即可看到 MasaBlazor 效果。
