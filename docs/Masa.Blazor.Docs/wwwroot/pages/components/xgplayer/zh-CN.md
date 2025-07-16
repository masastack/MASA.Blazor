---
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

## 使用 {#usage}

<masa-example file="Examples.components.xgplayer.Default"></masa-example>

<app-alert type="warning" content="仅支持`Url`参数实时更新，其他参数只有初始化时才会生效。"></app-alert>

## 示例 {#examples}

### 属性 {#props}

#### 开始时间 {#startTime}

`StartTime` 属性可以指定视频的开始播放时间。

<masa-example file="Examples.components.xgplayer.StartTime"></masa-example>

#### 禁用插件 {#ignores}

通过 `Ignores` 属性禁用插件，内置的插件列表在 `BultinPlugins` 中可以查看。

<masa-example file="Examples.components.xgplayer.Ignores"></masa-example>

### 其他 {#misc}

#### 切换音乐和视频 {#switch-video-audio updated-in=v1.11.0}

**MXgMusicPlayer** 是单独的音乐播放器组件，这个例子演示了如何在音乐和视频之间切换。

<masa-example file="Examples.components.xgplayer.Switch"></masa-example>

## 音乐播放器 {#music-player}

**MXgplayer** 结构上有个 **MXgMusicPlayer** 的基类组件，可以单独使用。

<masa-example file="Examples.components.xgplayer.MusicPlayer"></masa-example>

## 插件组件 {#plugin-components updated-in=v1.11.0}

目前实现了以下插件组件，其他插件组件欢迎 PR：

- **Controls**：控制栏
- **CssFullscreen**: 页面全屏按钮
- **Download**: PC端下载插件
- **DynamicBg**: 动态背景高斯模糊渲染插件
- **Fullscreen**: 位于控制栏的全屏切换组件，用于将当前视频全屏切换 全屏组件默认调用系统全屏，为了兼容移动端全屏不统一问题，支持使用网页全屏或者旋转全屏替代全屏效果
- **Mobile**: 播放器在移动web端交互插件
- **Play**：控制栏上的播放/暂停控制插件
- **Star**: 播放器中间切换暂停/播放的按钮
- **Time**：控制栏播放时间、时长显示插件
- **Volume**：音量控制插件

### 控制条 {#controls}

使用内置的控制插件组件，可以自定义控制条的样式。

<masa-example file="Examples.components.xgplayer.Controls"></masa-example>
