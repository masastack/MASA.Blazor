﻿---
title: Xgplayer（西瓜视频播放器）
desc: "一个基于 [Xigua Video Playervv3.0.11](https://h5player.bytedance.com/) 的 HTML5 视频播放器组件。"
tag: "基于JS封装"
---

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.JSComponents.Xgplayer
```

```html
<link rel="stylesheet" href="https://cdn.masastack.com/npm/xgplayer/3.0.11/xgplayer.min.css"/>
```

## 使用

<masa-example file="Examples.components.xgplayer.Default"></masa-example>

<app-alert type="warning" content="仅支持`Url`参数实时更新，其他参数只有初始化时才会生效。"></app-alert>

## 示例

### 属性

#### 开始时间

`StartTime` 属性可以指定视频的开始播放时间。

<masa-example file="Examples.components.xgplayer.StartTime"></masa-example>

#### 禁用插件

通过 `Ignores` 属性禁用插件，内置的插件列表在 `BultinPlugins` 中可以查看。

<masa-example file="Examples.components.xgplayer.Ignores"></masa-example>

### 其他

#### 切换音乐和视频

**MXgMusicPlayer** 是单独的音乐播放器组件，这个例子演示了如何在音乐和视频之间切换。
`_startTime` 字段用于记录视频和音频播放的进度。`_isRunning` 字段用于记录视频或音频是否正在播放。

<masa-example file="Examples.components.xgplayer.Switch"></masa-example>

## 音乐播放器

**MXgplayer** 结构上有个 **MXgMusicPlayer** 的基类组件，可以单独使用。

<masa-example file="Examples.components.xgplayer.MusicPlayer"></masa-example>

## 插件组件

目前仅实现了 **Controls**、**Play**、**Time** 和 **Start** 插件组件，其他插件组件欢迎 PR。

### 控制条

使用内置的控制插件组件，可以自定义控制条的样式。

<masa-example file="Examples.components.xgplayer.Controls"></masa-example>
