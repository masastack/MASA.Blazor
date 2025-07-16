---
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

#### Switch between video and audio {#switch-video-audio updated-in=v1.11.0}

**MXgMusicPlayer** is a separate music player component.
This example shows how to switch between music and video.

<masa-example file="Examples.components.xgplayer.Switch"></masa-example>

## Music player

**MXgplayer** has a base class component **MXgMusicPlayer** that can be used separately.

<masa-example file="Examples.components.xgplayer.MusicPlayer"></masa-example>

## Plugin components {updated-in=v1.11.0}

Currently, the following plugin components are implemented, and other plugin components are welcome to contribute:

- **Controls**: Control bar
- **CssFullscreen**: Page fullscreen button
- **Download**: Download plugin for PC
- **DynamicBg**: Dynamic background Gaussian blur rendering plugin
- **Fullscreen**: Fullscreen toggle component in the control bar, used to switch the current video to fullscreen. The fullscreen component defaults to using system fullscreen. To address inconsistent fullscreen behavior on mobile devices, it supports using web fullscreen or rotation fullscreen as an alternative.
- **Mobile**: Interaction plugin for the player on mobile web
- **Play**: Play/pause control plugin in the control bar
- **Star**: Button in the center of the player to toggle pause/play
- **Time**: Plugin for displaying playback time and duration in the control bar
- **Volume**: Volume control plugin

### Controls

Use the built-in control plugin component to customize the style of the control bar.

<masa-example file="Examples.components.xgplayer.Controls"></masa-example>
