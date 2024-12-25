---
title: Tooltips（工具提示）
desc: "当用户悬停在元素上时， **MTooltip** 组件可用于传递信息。 您还可以通过 `@bind-Value` 来控制提示的显示。 当激活时，提示将显示用于标识元素的文本，例如其功能的描述。"
related:
  - /blazor/components/badges
  - /blazor/components/icons
  - /blazor/components/menus
---

> v1.9.0 开始可以使用 `Text` 属性来设置文本类型的提示。

## 使用 {#usage}

提示组件可以包装任何元素。

<masa-example file="Examples.components.tooltips.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 对其 {#alignment}

提示可以对齐到激活器元素的四个侧面。

<masa-example file="Examples.components.tooltips.Alignment"></masa-example>

#### 可见性 {#visibility}

可使用 `@bind-Value` 编程性修改提示可见性。

<masa-example file="Examples.components.tooltips.Visibility"></masa-example>