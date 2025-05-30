---
title: Swiper（移动端滑块）
desc: "一个基于 [Swiper](https://github.com/nolimits4web/swiper) 的移动端触摸滑动组件。"
tag: "JS代理"
related:
  - /blazor/components/carousels
  - /blazor/components/windows
  - /blazor/components/slide-groups
---

## 安装 {#installation released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.JSComponents.Swiper
```

```html
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Swiper/10.2.0/swiper-bundle.min.css"/>
```

```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/Swiper/10.2.0/swiper-bundle.min.js"></script>
```

## 使用 {#usage}

<masa-example file="Examples.mobiles.swiper.Default"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 竖向 {#vertical}

<masa-example file="Examples.mobiles.swiper.Vertical"></masa-example>

#### 循环 {#loop}

<masa-example file="Examples.mobiles.swiper.Loop"></masa-example>

#### 间隔 {#space-between}

<masa-example file="Examples.mobiles.swiper.SpaceBetween"></masa-example>

#### 自动高度 {#auto-height}

默认情况下，Swiper 的高度是由高度最高的 slide 决定的，如果需要自适应高度，可以设置 `AutoHeight` 属性。

对于 **MSwiperSlide** 中的内容是异步加载的场景，建议在 **MSwiperSlide** 组件外加一个条件判断，以便在内容加载完成后重新计算高度。

```razor
<MSwiper AutoHeight>
    @if (content is not null) {
        <MSwiperSlide>@content</MSwiperSlide>
    }
</MSwiper>
```

<masa-example file="Examples.mobiles.swiper.AutoHeight"></masa-example>

#### 虚拟 {#virtual released-on=v1.10.0}

<app-alert type="warning" content="目前无法对列表做增量更新"></app-alert>

<masa-example file="Examples.mobiles.swiper.Virtual"></masa-example>

### 模块组件 {#module-components}

#### 导航 {#navigation}

<masa-example file="Examples.mobiles.swiper.Navigation"></masa-example>

#### 分页 {#pagination}

<masa-example file="Examples.mobiles.swiper.Pagination"></masa-example>

#### 自动播放 {#autoplay}

<masa-example file="Examples.mobiles.swiper.Autoplay"></masa-example>
