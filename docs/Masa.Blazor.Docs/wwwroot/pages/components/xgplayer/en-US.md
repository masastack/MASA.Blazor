﻿---
title: Xgplayer
desc: "A HTML5 video player component base on [Xigua Video Playerv3.0.11](https://h5player.bytedance.com/en)."
tag: "JS Wrapper"
---

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.JSComponents.Xgplayer
```

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/xgplayer@3.0.11/dist/index.min.css"/>
```

## Usage

<masa-example file="Examples.components.xgplayer.Default"></masa-example>

<app-alert type="warning" content="The `Url` parameter is the only one that can be updated in real time. Other parameters only take effect when initialized."></app-alert>

## Examples

### Props

#### StartTime

`StartTime` property can specify the start time of the video.

<masa-example file="Examples.components.xgplayer.StartTime"></masa-example>

#### Ignores

Disable plugins through the `Ignores` property. The built-in plugin list can be viewed in `BultinPlugins`.

<masa-example file="Examples.components.xgplayer.Ignores"></masa-example>

### Misc

#### Switch music and video

**MXgMusicPlayer** is a separate music player component.
This example shows how to switch between music and video.
The `_startTime` field is used to record the progress of video and audio playback.
The `_isRunning` field is used to record whether the video or audio is playing.

<masa-example file="Examples.components.xgplayer.Switch"></masa-example>

## Music player

**MXgplayer** has a base class component **MXgMusicPlayer** that can be used separately.

<masa-example file="Examples.components.xgplayer.MusicPlayer"></masa-example>

## Plugin components

Currently, only plugin components **Controls**, **Play**, **Time** and **Start** are implemented,
and other plugin components are welcome to PR.

### Controls

Use the built-in control plugin component to customize the style of the control bar.

<masa-example file="Examples.components.xgplayer.Controls"></masa-example>
