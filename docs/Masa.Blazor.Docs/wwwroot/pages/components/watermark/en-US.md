---
title: Watermark
desc: "Add specific text or image to the page."
---

## Usage

```shell
dotnet add package Masa.Blazor.SomethingSkia
```

[//]: # (<watermark-usage></watermark-usage>)

## Examples

### Props

#### Image

Use `Grayscale` to make the image gray.

<masa-example file="Examples.components.watermark.Image" disable-reason="Currently dotnet9 wasm use skiasharp exist
issue: https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

#### Color

<masa-example file="Examples.components.watermark.Color" disable-reason="Currently dotnet9 wasm use skiasharp exist
issue: https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

#### Chinese {released-on=v1.7.3}

To display Chinese, you need to provide a font that supports Chinese.

<masa-example file="Examples.components.watermark.Chinese" disable-reason="Currently dotnet9 wasm use skiasharp exist
issue: https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

### Misc

#### Fallback to text

Fallback to text when an image load failed.

<masa-example file="Examples.components.watermark.ImageErrorFallbackToText" disable-reason="Currently dotnet9 wasm use
skiasharp exist issue: https://github.com/mono/SkiaSharp/issues/3224"></masa-example>
