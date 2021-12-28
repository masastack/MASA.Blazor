---
category: Components
subtitle: 下拉框
type: 下拉框
title: Selects
cols: 1
related:
  - /components/autocompletes
  - /components/combobox
  - /components/forms
---

选择器组件用于从选项列表中收集用户提供的信息。

## API

- [MSelect](/api/MSelect)

## 注意

<!--alert:info--> 
浏览器自动补全默认设置为关闭，可能因不同的浏览器而变化或忽略。 **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 

<!--alert:warning--> 
**MenuProps** 的 **Auto** 属性只支持默认输入样式。
<!--/alert:warning--> 

<!--alert:error--> 
当使用一个Object(对象) 作为**Items**的属性时，你必须使用**ItemText**和**ItemValue**与传入的对象关联起来。 这些值默认为 **Text** 和 **Value** 且可以更改。
<!--/alert:error--> 