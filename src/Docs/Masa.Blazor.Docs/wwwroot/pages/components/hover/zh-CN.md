---
title: Hover（悬停）
desc: "**MHover** 组件提供了一个干净的界面用来处理任何组件的悬停状态。"
related:
  - /blazor/components/cards
  - /blazor/components/images
  - /blazor/components/tooltips
---

## 使用

**MHover** 组件是一个包装器，它应该只包含一个子元素并且可以在悬停时可以触发事件。

<hover-usage></hover-usage>

## 示例

### 属性

#### 禁用

设置 `Disabled` 属性可以禁用悬停功能。

<masa-example file="Examples.components.hover.Disabled"></masa-example>

#### 打开和关闭延迟

通过组合或单独使用 `OpenDelay` 和 `CloseDelay` 属性延迟 **MHover** 事件。

<masa-example file="Examples.components.hover.Open"></masa-example>

### 其他

#### 悬停列表

**MHover** 可以与 `foreach`  结合使用，以便在用户与列表交互时突出单个项目。

<masa-example file="Examples.components.hover.List"></masa-example>

#### 过渡

创建响应用户交互的高度自定义的组件。

<masa-example file="Examples.components.hover.Transition"></masa-example>