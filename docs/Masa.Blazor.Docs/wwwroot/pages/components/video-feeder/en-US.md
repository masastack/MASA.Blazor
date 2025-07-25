---
title: Video Feeder
desc: Provides a video feed similar to Douyin.
tag: "JS Wrapper"
release: v1.11.0
---

## Usage

```shell
dotnet add package Masa.Blazor.JSComponents.VideoFeeder
```

This component depends on the [MSwiper](/blazor/mobiles/swiper) and [MXgplayer](/blazor/components/xgplayer) components,
so you need to include their styles and scripts.

```html
<link rel="stylesheet" href="_content/Masa.Blazor.JSComponents.VideoFeeder/css/video-feeder.css" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.css"/>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/xgplayer@3.0.11/dist/index.min.css"/>
```

```html
<script src="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.js"></script>
```

Supports long-press to open the bottom menu.

<masa-example file="Examples.components.video_feeder.Usage"></masa-example>
