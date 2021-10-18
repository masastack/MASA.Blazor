---
order: 0
title:
  zh-CN: 验证
  en-US: Validation
---

## zh-CN

Vuetify包括通过 规则 prop进行简单的验证。 prop 接受了各种类型 函数, 布尔值 和 字符串 的组合。 当输入值发生变化时，数组中的每个元素将被验证。 函数传递当前的v-model 作为参数，必须返回 true / false 或 字符串 包含错误消息。

## en-US

This is an example of the default application markup for Vuetify. You can place your layout elements anywhere, as long as you apply the app property. The key component in all of this is v-main. This will be dynamically sized depending upon the structure of your designated app components. You can use combinations of any or all of the above components including v-bottom-navigation.

When using vue-router it is recommended that you place your views inside v-main.
