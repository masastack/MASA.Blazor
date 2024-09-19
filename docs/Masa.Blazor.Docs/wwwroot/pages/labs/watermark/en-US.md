---
title: Watermark
desc: "Add specific text or image to the page."
---

> This component is in [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia) package.

## Usage

<watermark-usage></watermark-usage>

## Examples

### Props

#### Image

Use `Grayscale` to make the image gray.

<masa-example file="Examples.labs.watermark.Image"></masa-example>

#### Color

<masa-example file="Examples.labs.watermark.Color"></masa-example>

#### Chinese {released-on=v1.7.3}

To display Chinese, you need to provide a font that supports Chinese.

<masa-example file="Examples.labs.watermark.Chinese"></masa-example>

### Misc

#### Fallback to text

Fallback to text when image load failed.

<masa-example file="Examples.labs.watermark.ImageErrorFallbackToText"></masa-example>
