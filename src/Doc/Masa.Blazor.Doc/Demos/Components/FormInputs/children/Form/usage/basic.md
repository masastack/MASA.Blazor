---
order: 0
title:
  zh-CN: 使用
  en-US: Usage
---

## zh-CN

内部 `MForm` 组件使得很容易添加验证到表单输入。 所有输入组件都有一个 **规则** prop，它接受不同类型的组 函数, 布尔值 和 字符串。 这些允许您指定输入无效的 __ 或 __ 的条件。 每当输入值被更改时，数组中的每个函数将收到新的值，每个数组元素将被评分。 如果函数或数组元素返回 fals 或 字符串, 验证失败， 字符串 值将作为错误信息显示。

## en-US

The internal `MForm` component makes it easy to add validation to form input. All input components have a * * rule * * prop, which accepts different types of group functions, Boolean values and strings. These allow you to specify that the input is invalid__ or__ Conditions. Whenever the input value is changed, each function in the array will receive a new value and each array element will be scored. If the function or array element returns false or string, and the verification fails, the string value will be displayed as an error message.
