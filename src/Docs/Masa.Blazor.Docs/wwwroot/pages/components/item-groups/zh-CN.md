---
title: Item Groups（项目组）
desc: "**MItemGroup** 提供从任何组件中创建一组可选项的功能。 这是 **MTabs** 和 **Mcarousel** 等组件的基本功能。"
related:
  - /blazor/components/button-groups
  - /blazor/components/carousels
  - /blazor/components/tabs
---

## 使用

**MItemGroup** 的核心用法是创建由 `Value` 控制的任何对象的组。

<masa-example file="Examples.components.item_groups.Usage"></masa-example>

## 示例

### 属性

#### 激活类

使用 `ActiveClass`属性允许您在活动项上设置自定义CSS类。

<masa-example file="Examples.components.item_groups.ActiveClass"></masa-example>

#### 必填项

使用 `Mandatory`属性的项目组必须至少选择一个项目。

<masa-example file="Examples.components.item_groups.Mandatory"></masa-example>

#### 多选

使用`Multiple`属性项目组可以选择多个项目。

<masa-example file="Examples.components.item_groups.Multiple"></masa-example>

### 其他

#### Chips（纸片）

轻松绑定自定义纸片组。

<masa-example file="Examples.components.item_groups.Chips"></masa-example>

#### 选择

当图标允许选择或取消选择单个选项（例如将项目标记为收藏）时，它们可以用作切换按钮。

<masa-example file="Examples.components.item_groups.Selection"></masa-example>
