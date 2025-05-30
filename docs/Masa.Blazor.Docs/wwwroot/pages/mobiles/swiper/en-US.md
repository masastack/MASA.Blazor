---
title: Swiper
desc: "A mobile touch slider component base on [Swiper](https://github.com/nolimits4web/swiper)."
tag: "JS Proxy"
related:
  - /blazor/components/carousels
  - /blazor/components/windows
  - /blazor/components/slide-groups
---

## Installation {released-on=v1.10.0}

```shell
dotnet add package Masa.Blazor.JSComponents.Swiper
```

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.css"/>
```

```html
<script src="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.js"></script>
```

## Usage

<masa-example file="Examples.mobiles.swiper.Default"></masa-example>

## Examples

### Props

#### Vertical

<masa-example file="Examples.mobiles.swiper.Vertical"></masa-example>

#### Loop

<masa-example file="Examples.mobiles.swiper.Loop"></masa-example>

#### Space between

<masa-example file="Examples.mobiles.swiper.SpaceBetween"></masa-example>

#### Auto height

By default, the highest slide determines the height of the Swiper.
If you need to adapt the height, you can set the `AutoHeight` property.

For scenarios where the content in the **MSwiperSlide** is loaded asynchronously, 
it is recommended to add a conditional judgment outside the **MSwiperSlide** component
to recalculate the height after the content is loaded.

```razor
<MSwiper AutoHeight>
    @if (content is not null) {
        <MSwiperSlide>@content</MSwiperSlide>
    } 
</MSwiper>
```

<masa-example file="Examples.mobiles.swiper.AutoHeight"></masa-example>

#### Virtual {released-on=v1.10.0}

<app-alert type="warning" content="Currently, incremental updates to the list are not supported."></app-alert>

<masa-example file="Examples.mobiles.swiper.Virtual"></masa-example>

### Module components

#### Navigation

<masa-example file="Examples.mobiles.swiper.Navigation"></masa-example>

#### Pagination

<masa-example file="Examples.mobiles.swiper.Pagination"></masa-example>

#### Autoplay

<masa-example file="Examples.mobiles.swiper.Autoplay"></masa-example>
