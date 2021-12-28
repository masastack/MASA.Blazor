---
category: Components
subtitle: 按钮
type: 按钮
title: Buttons
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/fNUKzY1sk/Buttons.svg
related:
  - /components/button-groups
  - /components/icons
  - /components/floating-action-buttons
---

`MButton`（按钮）组件采 Material Design 设计主题风格，并增加众多的配置选项替换了标准的 html 按钮。 任何颜色助手类都可以用来改变背景或文字颜色。

## API

- [MButton](/api/MButton)
- [MButtonGroup](/api/MButtonGroup)

## 注意

<!--alert:warning--> 
当使用 **Dark** 属性时，`MButton` 是唯一一种拥有不同行为的组件。 通常来说，组件使用 **Dark** 属性来表示他们将有深色背景，文本也需要是白色的。 虽然这对
`MButton` 也是起作用的，但由于禁用状态与白色背景容易造成混淆，建议仅在按钮**为**彩色背景时使用此属性。 If you need white text, simply add the `white--text` class.
