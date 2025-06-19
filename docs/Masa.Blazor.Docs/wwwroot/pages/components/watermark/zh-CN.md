---
title: Watermark（水印）
desc: "给某个区域加上水印。"
---

> This component is in [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia) package.

## 使用 {#usage}

[//]: # (<watermark-usage></watermark-usage>)

## 示例 {#examples}

### 属性 {#props}

#### 图片 {#image}

<masa-example file="Examples.components.watermark.Image" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

#### 颜色 {#color}

<masa-example file="Examples.components.watermark.Color" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

#### 中文 {#chinese released-on=v1.7.3}

设置中文需要提供支持中文的字体。

<masa-example file="Examples.components.watermark.Chinese" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

### 其他 {#misc}

#### 回退到文字 {#fallback-to-text}

当图片加载失败时，回退到文字水印。

<masa-example file="Examples.components.watermark.ImageErrorFallbackToText" disable-reason="目前 dotnet9 wasm 使用
skiasharp 存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>
