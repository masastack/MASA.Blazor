---
title: Image Captcha（图片验证码）
tag: preset
related:
  - /components/text-fields
  - /components/image
---

## 使用

**PImageCaptcha** 是使用`SkiaSharp` 生成的图片，在Linux环境中，需要安装 `libfontconfig1`，如Dockerfile中增加 `RUN apt-get update && apt-get install -y libfontconfig1`。

<image-captcha-usage></image-captcha-usage>

## 示例

### 属性

#### TextField 样式

<example file="" />

#### 自定义验证码结果

<example file="" />