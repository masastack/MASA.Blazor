<p align="center">
  <a href="https://blazor.masastack.com" target="_blank">
    <img alt="MASA Blazor Logo" width="150" src="./imgs/logo.png">
  </a>
</p>

<h1 align="center">MASA Blazor</h1>

<div align="center">

一套基于Material设计规范和BlazorComponent的交互能力提供标准的基础组件库

[![MASA.Blazor](https://img.shields.io/badge/license-MIT-informational)](https://github.com/masastack/Masa.Blazor/blob/develop/LICENSE)
[![.NET](https://github.com/masastack/Masa.Blazor/actions/workflows/mirror-gitlab.yml/badge.svg)](https://github.com/masastack/Masa.Blazor/actions/workflows/mirror-gitlab.yml)
[![Nuget](https://img.shields.io/nuget/v/Masa.Blazor)](https://www.nuget.org/packages/Masa.Blazor)
[![Nuget](https://img.shields.io/nuget/dt/Masa.Blazor)](https://www.nuget.org/packages/Masa.Blazor)

</div>

[English](./README.md) | 简体中文

## 🚀 MASA Blazor是什么？

基于Material设计规范和BlazorComponent的交互能力提供标准的基础组件库。提供如布局、弹框标准、Loading、全局异常处理等标准场景的预置组件。从更多实际场景出发，满足更多用户和场景的需求，最大的减少开发者的时间成本。缩短开发周期提高开发效率。并提供一套Web解决方案示例 - [MASA Blazor Pro](https://github.com/masastack/Masa.Blazor.Pro) 有多种常见场景和预设布局等精彩内容。

## ❓ 为什么选择MASA Blazor？

MASA Blazor 基于Material设计规范，每一个组件都经过精心设计，具有模块化、响应式和优秀的性能。MASA Blazor 是由一支专业的全职技术团队进行定期维护升级，高效的响应速度，多元化的解决方案，长期提供支持，并提供企业级支持。目前已在几家知名企业使用，后续MASA Stack产品系列也将持续使用，除了可以保证项目质量，还可以持续的增加新的组件和功能。MASA Stack除了为开发者提供众多中台类开源项目，其最基础的组成部分之一MASA Blazor也希望可以打造成最实用的组件库。

## 🎉 特性

- 丰富组件：包含Vuetify 1:1还原的基础组件，以及很多实用的预置组件和.Net深度集成功能，包括Url、面包屑、导航三联动，高级搜索，i18n等
- UI设计语言：设计风格现代，UI 多端体验设计优秀
- 专业示例：[MASA Blazor Pro](https://github.com/masastack/Masa.Blazor.Pro) 提供多种常见场景的预设布局
- 简易上手：丰富详细的上手文档，免费的视频教程（制作中）
- 社区活跃鼓励：用户参与实时互动，做出贡献加入我们，构建最开放的开源社区
- 长期支持：全职团队维护，长期提供支持，并提供企业级支持
- 知名企业选择：该技术框架被多家知名企业选择使用，未来MASA Stack产品线也将一直使用，持续增加新功能

## 📊 统计

![Alt](https://repobeats.axiom.co/api/embed/d2284637ff2024bc3301ffce6cdaa1706cfcdc5c.svg "Repobeats analytics image")

## 🖥️ 文档

查看文档，请访问 [docs.masastack.com](https://docs.masastack.com/blazor/introduction/why-masa-blazor)。

## 📂 相关项目

- [MASA Blazor Pro（Contains examples of various business scenarios）](https://github.com/masastack/Masa.Blazor.Pro)
- [MASA Template](https://github.com/masastack/MASA.Template)

## 👨‍💻 本地开发

### 开发环境搭建

- 安装 [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- 安装 [Visual Studio Code](https://code.visualstudio.com/Download/) or [Visual Studio 2022](https://docs.microsoft.com/en-us/visualstudio/releases/2022/release-notes)

### 克隆代码

```shell
git clone --recursive https://github.com/masastack/Masa.Blazor.git
cd Masa.Blazor
git submodule foreach git checkout main
```

### 运行文档站点

```shell
cd docs/Masa.Doc.Server
dotnet run
```

### 访问

推荐使用 chrome 或 edge 访问 `http://localhost:5000/`

## 🌐 浏览器支持

![chrome](https://img.shields.io/badge/chrome->%3D57-success.svg?logo=google%20chrome&logoColor=red)![firefox](https://img.shields.io/badge/firefox->522-success.svg?logo=mozilla%20firefox&logoColor=red)![edge](https://img.shields.io/badge/edge->%3D16-success.svg?logo=microsoft%20edge&logoColor=blue)![ie](https://img.shields.io/badge/ie->%3D11-success.svg?logo=internet%20explorer&logoColor=blue)![Safari](https://img.shields.io/badge/safari->%3D14-success.svg?logo=safari&logoColor=blue)![oper](https://img.shields.io/badge/opera->%3D4.4-success.svg?logo=opera&logoColor=red)

### 移动设备

![ios](https://img.shields.io/badge/ios-supported-success.svg?logo=apple&logoColor=white)![Andriod](https://img.shields.io/badge/andriod-suported-success.svg?logo=android)

|         | Chrome    | Firefox   | Safari    | Microsoft Edge |
| ------- | --------- | --------- | --------- | -------------- |
| iOS     | Supported | Supported | Supported | Supported      |
| Android | Supported | Supported | N/A       | Supported      |

### 桌面设备

![macOS](https://img.shields.io/badge/macOS-supported-success.svg?logo=apple&logoColor=white)![linux](https://img.shields.io/badge/linux-suported-success.svg?logo=linux&logoColor=white)![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows)

|         | Chrome    | Firefox   | Safari        | Opera     | Microsoft Edge | Internet Explorer |
| ------- | --------- | --------- | ------------- | --------- | -------------- | ----------------- |
| Mac     | Supported | Supported | Supported     | Supported | N/A            | N/A               |
| Linux   | Supported | Supported | N/A           | N/A       | N/A            | N/A               |
| Windows | Supported | Supported | Not supported | Supported | Supported      | Supported, IE11+  |

> 由于 [WebAssembly](https://webassembly.org) 的限制，Blazor WebAssembly 不支持 IE 浏览器，但 Blazor Server 支持 IE 11†。 详见[官网说明](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/supported-platforms?view=aspnetcore-3.1&WT.mc_id=DT-MVP-5003987)。

## 💁‍♂️ 如何贡献

1. Fork & Clone
2. Create Feature_xxx branch
3. Commit with commit message, like `feat: add MButton`
4. Create Pull Request

感谢所有为本项目做出过贡献的朋友。

<a href="https://github.com/masastack/Masa.Blazor/graphs/contributors">
    <img src="https://contrib.rocks/image?repo=BlazorComponent/Masa.Blazor" />
</a>

## 💬 交流

| QQ群                                                | 微信公众号                                                               | 微信客服                                                                  |
|:--------------------------------------------------:|:-------------------------------------------------------------------:|:---------------------------------------------------------------------:|
| ![masa.blazor-qq](./imgs/masa.blazor-qq-group.png) | ![masa.blazor-weixin](./imgs/masa.blazor-wechat-public-account.png) | ![masa.blazor-weixin](./imgs/masa.blazor-wechat-customer-service.png) |

## 👥 开发团队

数闪技术团队，是一支高效，稳定，创新的团队。团队秉承着丰富Blazor生态的初心，去不断努力，为开发人员带来更好的体验是数闪技术团队的追求。感谢各位的支持和使用。

## 📜 行为准则

本项目采用了《贡献者公约》所定义的行为准则，以明确我们社区的预期行为。更多信息请见 [MASA Stack Community Code of Conduct](https://github.com/masastack/community/blob/main/CODE-OF-CONDUCT.md).

## 📄 许可声明

[![Masa.Blazor](https://img.shields.io/badge/license-MIT-informational)](https://github.com/masastack/Masa.Blazor/blob/develop/LICENSE)

Copyright (c) 2021-present Masa.Blazor
