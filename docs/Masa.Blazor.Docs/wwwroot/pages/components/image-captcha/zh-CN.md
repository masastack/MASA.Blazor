---
title: Image Captcha（图片验证码）
tag: 预置
related:
  - /blazor/components/text-fields
  - /blazor/components/image
---

> 此组件在 [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia) 包中。

## 使用 {#usage}

**PImageCaptcha** 是使用`SkiaSharp` 生成的图片，在Linux环境中，需要安装 `libfontconfig1`，如Dockerfile中增加
`RUN apt-get update && apt-get install -y libfontconfig1`。

<masa-example file="Examples.components.image_captcha.Usage" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### TextField 样式 {#textfield-style}

<masa-example file="Examples.components.image_captcha.TextFieldStyle" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>

#### 自定义验证码结果 {#custom-result}

<masa-example file="Examples.components.image_captcha.VerifyCode" disable-reason="目前 dotnet9 wasm 使用 skiasharp
存在问题：https://github.com/mono/SkiaSharp/issues/3224"></masa-example>