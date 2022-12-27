---
title: OTP Input（一次性密码输入）
desc: "OTP 输入用于通过一次性密码对用户进行身份验证的 MFA 程序"
related:
  - /blazor/components/textareas
  - /blazor/components/text-fields
  - /blazor/components/forms
---

## 使用

在这里，我们显示了可以在应用程序中使用的设置列表。

<otp-input-usage></otp-input-usage>

## 示例

### 属性

#### 深色主题

应用深色主题，改变组件的颜色。

<masa-example file="Examples.components.otp_input.DarkTheme"></masa-example>

#### 完成事件

您可以很容易的编写一个处理程序来接收组件完成输入时的结果

<masa-example file="Examples.components.otp_input.FinishEvent"></masa-example>

#### Hidden input

输入的值可以用 `type="password"` 隐藏

<masa-example file="Examples.components.otp_input.HiddenInput"></masa-example>