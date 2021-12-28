---
category: Components
subtitle: 自动补全
type: 自动补全
title: Autocompletes
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/Autocomplete.svg
related:
  - /components/combobox
  - /components/forms
  - /components/selects
---

`MAutocomplete` 组件提供简单且灵活的自动补全功能 且支持查找大规模数据甚至是从API请求的动态数据

## API

- [MAutocomplete](/api/MAutocomplete)

## 注意

<!--alert:error--> 
当使用一个Object(对象) 作为**Items**的属性时，你必须使用**ItemText**和**ItemValue**与传入的对象关联起来。 这些值默认为 **Text** 和 **Value** 且可以更改。
<!--/alert:error--> 

<!--alert:warning--> 
**MenuProps** 的 **Auto** 属性只支持默认输入样式。
<!--/alert:warning--> 

<!--alert:info--> 
浏览器自动补全默认设置为关闭，可能因不同的浏览器而变化或忽略。 **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 