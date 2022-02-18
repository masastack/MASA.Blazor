---
order: 0
title:
  zh-CN: 提交与验证 & 清除
  en-US: Validation with submit & clear
---

## zh-CN

`MForm` 组件有三个功能，可以通过在组件上设置ref来访问。 ref允许我们访问组件的内部方法，例如 `<MForm @ref="_form">`。 _form.Validate() 将验证所有输入并返回它们是否都有效。 _form.Reset() 将清除所有输入并重置验证错误。 _form.ResetValidation() 将只重置输入验证，不改变他们的状态。

## en-US

`MForm` component has three functions, which can be accessed by setting ref on the component. Ref allows us to access the internal methods of components, such as `<MForm @ref = "_form" > `_ form.Validate() validates all inputs and returns whether they are valid._form.Reset() clears all inputs and resets validation errors._ form.Resetvalidation() will only reset input validation without changing their state.
