---
title: Swiper
desc: "A mobile touch slider component base on [Swiper](https://github.com/nolimits4web/swiper)."
tag: "JS Proxy"
related:
  - /blazor/components/carousels
  - /blazor/components/windows
  - /blazor/components/slide-groups
---

You need to reference the following files before using it:

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.css"/>

<script src="https://cdn.jsdelivr.net/npm/swiper@10/swiper-bundle.min.js"></script>
```

## Usage

<masa-example file="Examples.labs.swiper.Default"></masa-example>

## Examples

### Props

#### Vertical

<masa-example file="Examples.labs.swiper.Vertical"></masa-example>

#### Loop

<masa-example file="Examples.labs.swiper.Loop"></masa-example>

#### Space between

<masa-example file="Examples.labs.swiper.SpaceBetween"></masa-example>

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

<masa-example file="Examples.labs.swiper.AutoHeight"></masa-example>

### Module components

#### Navigation

<masa-example file="Examples.labs.swiper.Navigation"></masa-example>

#### Pagination

<masa-example file="Examples.labs.swiper.Pagination"></masa-example>

#### Autoplay

<masa-example file="Examples.labs.swiper.Autoplay"></masa-example>
