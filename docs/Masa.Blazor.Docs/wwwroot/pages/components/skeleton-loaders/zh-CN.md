---
title: Skeleton loaders（骨架装载器）
desc: "**MSkeletonLoader** 组件是一个多功能工具，可以在一个项目中填充许多角色。 在其核心部分，该组件向用户提供了一个指示，指出某些东西即将出现但尚未可用。 有超过30个预先定义的选项，可以组合成定制的示例。"
related:
  - /blazor/components/cards
  - /blazor/components/progress-circular
  - /blazor/components/buttons
---

## 使用

**MSkeletonLoader** 组件为用户提供了一个内容即将到来/加载的视觉指示器。 这比传统的全屏加载器要好。

<masa-example file="Examples.components.skeleton_loaders.Usage"></masa-example>

## 示例

### 属性

#### 样板组件

**MSkeletonLoader**可以在创建实体模型时用作样板设计。 混合和匹配各种预定义的选项或创建您自己独特的实现。 在此示例中，我们使用自定义 data 属性来将相同的属性一次应用到多个 **MSkeletonLoader**。

<masa-example file="Examples.components.skeleton_loaders.BoilerplateComponent"></masa-example>