---
title: Watermark（水印）
desc: "给某个区域加上水印。"
---

> This component is in [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia) package.

## 使用 {#usage}

<watermark-usage></watermark-usage>

## 示例 {#examples}

### 属性 {#props}

#### 图片 {#image}

<masa-example file="Examples.labs.watermark.Image"></masa-example>

#### 颜色 {#color}

<masa-example file="Examples.labs.watermark.Color"></masa-example>

#### 中文 {#chinese released-on=v1.7.3}

设置中文需要提供支持中文的字体。

<masa-example file="Examples.labs.watermark.Chinese"></masa-example>

### 其他 {#misc}

#### 回退到文字 {#fallback-to-text}

当图片加载失败时，回退到文字水印。

<masa-example file="Examples.labs.watermark.ImageErrorFallbackToText"></masa-example>
