---
title: Video Feeder（视频流）
desc: 类似于抖音的视频流组件。
tag: "基于JS封装"
release: v1.11.0
---

## 使用 {#usage}

```shell
dotnet add package Masa.Blazor.JSComponents.VideoFeeder
```

该组件依赖于 [MSwiper](/blazor/mobiles/swiper) 组件和 [MXgplayer](/blazor/components/xgplayer) 组件，因此需要引用它们的样式和脚本。

```html
<link rel="stylesheet" href="_content/Masa.Blazor.JSComponents.VideoFeeder/css/video-feeder.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Swiper/10.2.0/swiper-bundle.min.css"/>
<link rel="stylesheet" href="https://cdn.masastack.com/npm/xgplayer/3.0.11/xgplayer.min.css"/>
```

```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/Swiper/10.2.0/swiper-bundle.min.js"></script>
```

支持长按打开底部菜单。

<masa-example file="Examples.components.video_feeder.Usage"></masa-example>
