---
order: 0
title: 安装
---

让我们从 MASA Blazor 开始吧，这是 .NET Core Blazor 框架的UI组件库，用于构建功能丰富、快速的应用程序。

## 创建一个 Blazor Server 项目

> 在创建项目之前，请确保[.NET Core SDK 6.0.100-preview.5+](https://dotnet.microsoft.com/download/dotnet/6.0)已被成功安装。

在命令行界面中创建名为`BlazorApp`的新Blazor应用:

```sh
$ dotnet new blazorserver -o BlazorApp
```

上述命令将创建一个带有 `-o|--output` 选项的 `BlazorApp` 文件夹来保存应用。 `BlazorApp` 文件夹是项目的根文件夹。 使用以下命令将目录切换到 `BlazorApp` 文件夹：

```sh
cd BlazorApp
```

## 安装Nuget包

```sh
$ dotnet add package MASA.Blazor
```

## 引入样式、字体、脚本

在`Pages/_Host.cshtml`中引入：

```html
<!--样式-->
<link href="_content/MASA.Blazor/css/masa-blazor.css" rel="stylesheet">
<link href="_content/MASA.Blazor/css/masa-extend-blazor.css" rel="stylesheet">
<!--字体-->
<link href="https://cdn.masastack.com/npm/@mdi/font@5.x/css/materialdesignicons.min.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/materialicons/materialicons.css" rel="stylesheet">
<link href="https://cdn.masastack.com/npm/fontawesome/v5.0.13/css/all.css" rel="stylesheet">
<!--脚本(尽量放到body的最后位置，而不是跟上面的css放到一起)-->
<script src="_content/BlazorComponent/js/blazor-component.js"></script>
```

## 注入相关服务

将 MASA.Blazor 的相关服务添加到`Startup.ConfigureServices`：

```c#
services.AddMasaBlazor();
```

## 全局配置

修改`_Imports.razor`文件,添加以下内容:

```c#
@using MASA.Blazor
```

修改`Shared/MainLayout.razor`文件，让MApp成为根元素：

```html
<MApp>
	//其它布局内容
</MApp>
```

去查看组件示例，开始使用吧！

