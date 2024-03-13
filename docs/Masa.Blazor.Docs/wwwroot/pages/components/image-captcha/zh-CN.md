---
title: Image Captcha（图片验证码）
tag: 预置
related:
  - /blazor/components/text-fields
  - /blazor/components/image
---

> 此组件在 [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia) 包中。

## 使用

**PImageCaptcha** 是使用`SkiaSharp` 生成的图片，在Linux环境中，需要安装 `libfontconfig1`，如Dockerfile中增加 `RUN apt-get update && apt-get install -y libfontconfig1`。

<masa-example file="Examples.components.image_captcha.Usage"></masa-example>

## 示例

### 属性

#### TextField 样式

<masa-example file="Examples.components.image_captcha.TextFieldStyle"></masa-example>

#### 自定义验证码结果

<masa-example file="Examples.components.image_captcha.VerifyCode"></masa-example>