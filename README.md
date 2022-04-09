<p align="center">
  <a href="https://masa-blazor-docs-dev.lonsid.cn" target="_blank">
    <img alt="MASA Blazor Logo" width="150" src="./imgs/logo.png">
  </a>
</p>

<h1 align="center">MASA Blazor</h1>

<div align="center">

A set of standard basic component libraries based on Material design specifications and BlazorComponent interaction capabilities

[![MASA.Blazor](https://img.shields.io/badge/license-MIT-informational)](https://github.com/BlazorComponent/Masa.Blazor/blob/develop/LICENSE) 
[![.NET](https://github.com/BlazorComponent/Masa.Blazor/actions/workflows/mirror-gitlab.yml/badge.svg)](https://github.com/BlazorComponent/Masa.Blazor/actions/workflows/mirror-gitlab.yml)
[![Nuget](https://img.shields.io/nuget/v/Masa.Blazor)](https://www.nuget.org/packages/Masa.Blazor)
[![Nuget](https://img.shields.io/nuget/dt/Masa.Blazor)](https://www.nuget.org/packages/Masa.Blazor)

</div>

English| [简体中文](./README.zh-CN.md)

## What is MASA Blazor?

Provide a standard basic component library based on Material design specifications and BlazorComponent's interactive capabilities. Provides preset components for standard scenarios such as layout, frame standard, Loading, and global exception handling. Starting from more practical scenarios, to meet the needs of more users and scenarios, and to minimize the time cost of developers. Shorten the development cycle and improve development efficiency. And provide a set of examples of Web solutions - [MASA Blazor Pro](https://github.com/BlazorComponent/Masa.Blazor.Pro) has a variety of common scenes and preset layouts and other exciting content.



## Why choose MASA Blazor?

MASA Blazor is based on the Material design specification, and each component is carefully designed, with modularity, responsiveness and excellent performance. MASA Blazor is regularly maintained and upgraded by a professional full-time technical team, efficient response speed, diversified solutions, long-term support, and enterprise-level support. At present, it has been used in several well-known companies, and the follow-up MASA Stack product series will continue to be used. In addition to ensuring the quality of the project, it can also continue to add new components and functions. In addition to providing developers with many mid- and Taiwan-based open source projects, MASA Stack, one of its most basic components, MASA Blazor, also hopes to be the most practical component library.



## Feature

- Rich components: Contains the basic components of Vuetify 1:1 restoration, as well as many practical preset components and deep integration functions of .Net, including three linkages of Url, breadcrumbs, navigation, advanced search, i18n, etc.
- UI design language: modern design style, excellent UI multi-end experience design
- Professional example: [MASA Blazor Pro](https://github.com/BlazorComponent/Masa.Blazor.Pro) provides preset layouts for a variety of common scenarios
- Easy to get started: rich and detailed getting started documents, free video tutorials (in production)
- Active community encouragement: users participate in real-time interaction, make contributions to join us, and build the most open open source community
- Long-term support: full-time team maintenance, long-term support, and enterprise-level support
- Choice of well-known companies: This technology framework has been chosen by many well-known companies, and the MASA Stack product line will continue to be used in the future, and new functions will continue to be added

# Stats
![Alt](https://repobeats.axiom.co/api/embed/2728adbcfa084a3f50de3587080404ee203c80e6.svg "Repobeats analytics image")

# Getting started

### Development environment setup

- Install [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- Install [Visual Studio Code](https://code.visualstudio.com/Download/) or [Visual Studio 2022](https://docs.microsoft.com/en-us/visualstudio/releases/2022/release-notes)

**Install Template**

```c#
dotnet new --install Masa.Template
```

**Create Project**

* Blazor Server

```shell
dotnet new masabp -o Masa.Test
```

- Blazor WebAssembly

```shell
dotnet new masabp --mode Wasm -o Masa.TestWasm
```

- Blazor RCL

```shell
dotnet new masabp --mode ServerAndWasm -o Masa.TestRcl
```

**Go to the Server project directory**

```shell
cd Masa.Test
```

**Run**

```shell
dotnet run
```

![masabp](imgs/masabp.gif)



### Existing project

* Install Nuget package 

```shell
dotnet add package Masa.Blazor
```

- Add Masa.Blazor related services to `Startup.ConfigureServices`:

```c#
services.AddMasaBlazor();
```

- Introduce styles, fonts, scripts in `wwwroot/index.html`(WebAssembly) or `Pages/_Host.cshtml`(Server):

```html
<html lang="en">
	<head>
		<!--Style-->
		<link href="_content/Masa.Blazor/css/masa-blazor.css" rel="stylesheet">
		<link href="_content/Masa.Blazor/css/masa-extend-blazor.css" rel="stylesheet">
		<!--Font-->
		<link href="https://cdn.jsdelivr.net/npm/@("@mdi")/font@5.x/css/materialdesignicons.min.css" rel="stylesheet">
		<link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
		<link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">	
	</head>
	<body>
		<!--Script (try to put it at the end of the body)-->
		<script src="_content/BlazorComponent/js/blazor-component.js"></script>
	</body>
</html>
```

- Add the namespace to the `_Imports.razor` file:

```c#
@using Masa.Blazor
@using BlazorComponent
```

- Modify the `Shared/MainLayout.razor` file to make MApp the root element:

```html
<MApp>
	//Other layout content
</MApp>
```

> See more ：[https://blazor.masastack.com/](https://blazor.masastack.com/ "https://blazor.masastack.com/")



## Local development

### Development environment setup

- Install [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- Install [Visual Studio Code](https://code.visualstudio.com/Download/) or [Visual Studio 2022](https://docs.microsoft.com/en-us/visualstudio/releases/2022/release-notes)

### Clone code

```shell
git clone git@github.com:BlazorComponent/Masa.Blazor.git
cd Masa.Blazor/src
git clone git@github.com:BlazorComponent/BlazorComponent.git
```

### Run doc server

```shell
cd Doc/Masa.Blazor.Doc.Server
dotnet run
```

### Visit

Recommended use chrome or edge to visit `http://localhost:5000/`



# Related resources

- [Blazor official document](https://docs.microsoft.com/en-us/aspnet/core/blazor/?WT.mc_id=DT-MVP-5004174)
- [Build a Blazor web application](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/?WT.mc_id=DT-MVP-5004174)
- [What is Blazor](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/2-understand-blazor-webassembly?WT.mc_id=DT-MVP-5004174)
- [Exercise-Configure the development environment](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/3-exercise-configure-enviromnent?WT.mc_id=DT-MVP-5004174)
- [Blazor components](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/4-blazor-components?WT.mc_id=DT-MVP-5004174)
- [Exercise-add components ](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/5-exercise-add-component?WT.mc_id=DT-MVP-5004174)
- [Data binding and events ](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/6-csharp-razor-binding?WT.mc_id=DT-MVP-5004174)
- [Exercise-data binding and events ](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/7-exercise-razor-binding?WT.mc_id=DT-MVP-5004174)
- [Summarize](https://docs.microsoft.com/en-us/learn/modules/build-blazor-webassembly-visual-studio-code/8-summary?WT.mc_id=DT-MVP-5004174)



# Related projects 

- [BlazorComponent（Unstyled underlying component framework）](https://github.com/BlazorComponent/BlazorComponent)
- [MASA Blazor Pro（Contains examples of various business scenarios）](https://github.com/BlazorComponent/Masa.Blazor.Pro)



## Supported browsers

![chrome](https://img.shields.io/badge/chrome->%3D57-success.svg?logo=google%20chrome&logoColor=red)![firefox](https://img.shields.io/badge/firefox->522-success.svg?logo=mozilla%20firefox&logoColor=red)![edge](https://img.shields.io/badge/edge->%3D16-success.svg?logo=microsoft%20edge&logoColor=blue)![ie](https://img.shields.io/badge/ie->%3D11-success.svg?logo=internet%20explorer&logoColor=blue)![Safari](https://img.shields.io/badge/safari->%3D14-success.svg?logo=safari&logoColor=blue)![oper](https://img.shields.io/badge/opera->%3D4.4-success.svg?logo=opera&logoColor=red)

### Mobile devices

![ios](https://img.shields.io/badge/ios-supported-success.svg?logo=apple&logoColor=white)![Andriod](https://img.shields.io/badge/andriod-suported-success.svg?logo=android)

|         |  Chrome     |  Firefox     |  Safari     | Microsoft Edge |
| ------- | ---------   | ---------    | ------      | -------------- |
| iOS     | Supported   | Supported    | Supported   | Supported      |
| Android | Supported   | Supported    | N/A         | Supported      |

### Desktop devices

![macOS](https://img.shields.io/badge/macOS-supported-success.svg?logo=apple&logoColor=white)![linux](https://img.shields.io/badge/linux-suported-success.svg?logo=linux&logoColor=white)![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows)

|         | Chrome    | Firefox   | Safari        | Opera     | Microsoft Edge | Internet Explorer |
| ------- | --------- | --------- | ------------- | --------- | -------------- | ----------------- |
| Mac     | Supported | Supported | Supported     | Supported | N/A            | N/A               |
| Linux   | Supported | Supported | N/A           | N/A       | N/A            | N/A               |
| Windows | Supported | Supported | Not supported | Supported | Supported      | Supported, IE11+  |

> Due to WebAssembly restriction, Blazor WebAssembly doesn't support IE browser, but Blazor Server supports IE 11† with additional polyfills. See official documentation



## How to contribute 

1. Fork & Clone
2. Create Feature_xxx branch
3. Commit with commit message, like `feat:add MButton`
4. Create Pull Request



## Contributors

Thanks to all the friends who have contributed to this project.

<a href="https://github.com/BlazorComponent/Masa.Blazor/graphs/contributors"> 
    <img src="https://contrib.rocks/image?repo=BlazorComponent/Masa.Blazor" /> 
</a>



## Interactive 

QQ group | WX public account| WX Customer Service
:---:|:---:|:---:
![masa.blazor-qq](./imgs/masa.blazor-qq-group.png) | ![masa.blazor-weixin](./imgs/masa.blazor-wechat-public-account.png) | ![masa.blazor-weixin](./imgs/masa.blazor-wechat-customer-service.png)

## Development team

The Digital Flash technical team is an efficient, stable and innovative team. The team adheres to the original intention of enriching the Blazor ecosystem, and it is the pursuit of the Digital Flash technical team to continue to work hard to bring a better experience to the developers. Thank you for your support and use.



## Code of conduct 

This project adopts the code of conduct defined in the "Contributors Convention" to clarify the expected behavior of our community. For more information, see  [MASA Stack Community Code of Conduct](https://github.com/masastack/community/blob/main/CODE-OF-CONDUCT.md).



## License

[![Masa.Blazor](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/BlazorComponent/Masa.Blazor/blob/develop/LICENSE)

Copyright (c) 2021-present Masa.Blazor
