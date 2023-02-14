---
title: OTP Input
desc: "The OTP input is used for MFA procedure of authenticating users by a one-time password."
related:
  - /blazor/components/textareas
  - /blazor/components/text-fields
  - /blazor/components/forms
---

## Usage

Here we display a list of settings that could be applied within an application.

<otp-input-usage></otp-input-usage>

## Examples

### Props

#### Dark theme

Applied dark theme, listen to value fill to affect button component.

<masa-example file="Examples.components.otp_input.DarkTheme"></masa-example>

#### Finish event

You can easily compose a loader to process the OTP input when completed insertion.

<masa-example file="Examples.components.otp_input.FinishEvent"></masa-example>

#### Hidden input

The entered value can be hidden with `type="password"`

<masa-example file="Examples.components.otp_input.HiddenInput"></masa-example>