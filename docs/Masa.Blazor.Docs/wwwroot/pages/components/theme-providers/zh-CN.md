---
title: Theme providers（主题提供程序）
desc: 主题提供程序允许您以与默认主题不同的主题来设计应用程序的一部分
related:
  - /blazor/features/theme
---

## 示例 {#examples}

### 属性 {#props}

#### 背景

默认情况下，**MThemeProvider** 属于非渲染的组件，且让你可以修改它的所有子组件的主题。当使用 `WithBackground` 属性时，**MThemeProvider** 将器子元素包含在一个元素中，并使用主题的背景颜色来替换其本身的背景颜色。

<masa-example file="Examples.components.theme_providers.Background"></masa-example>