---
title: System bars（系统栏）
desc: "**MSystemBar** 组件可以用于向用户显示状态。 它看起来像Android系统栏，可以包含图标、空格和一些文本。"
related:
  - /blazor/components/buttons
  - /blazor/components/toolbars
  - /blazor/components/tabs
---

## 使用

**MSystemBar** 最简单的形式是显示一个带有默认主题的小容器。

<system-bars-usage></system-bars-usage>

## 组件结构解剖

建议在 `MSystemBar` 内部放置元素：

* 在右侧放置信息图标
* 将时间或其他文本信息放在最右边

![System Bar Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/system-bar-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | 系统栏容器有一个默认插槽，内容右对齐 |
| 2. 图标项目（可选） | 用于通过使用图标传达信息 |
| 3. 文本（可选） | 通常用于显示时间的文本内容 |

## 示例

### 属性

#### 颜色

您可以选择使用 `Color` 属性 更改 **MSystemBar** 的颜色。

<masa-example file="Examples.components.system_bars.Color"></masa-example>

#### 熄灯

您可以使用 `LightsOut` 属性来降低 **MSystemBar** 的不透明度。

<masa-example file="Examples.components.system_bars.LightOut"></masa-example>

#### 主题

可以将 `Dark` 或 `Light` 主题变量应用于 **MSystemBar**。

<masa-example file="Examples.components.system_bars.Theme"></masa-example>

#### 窗口

带有窗口控件和状态信息的窗口栏。

<masa-example file="Examples.components.system_bars.Window"></masa-example>
