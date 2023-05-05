---
title: Avatars（头像）
desc: "**MAvatar** 组件通常用于显示用户个人资料图片。 此组件允许您动态添加设置响应图像、图标和文字的边框半径。 `Tile` 属性可用来显示无边框半径的头像。"
related:
  - /blazor/components/badges
  - /blazor/components/icons
  - /blazor/components/lists
---

## 使用

头像以最简单的形式在圆形容器中显示内容。

<avatars-usage></avatars-usage>

## 组件结构解剖

建议在 `MAvatar` 内部放置元素：

* 将 [MImage](blazor/components/images/) 或 [MIcon](blazor/components/images/) 组件放在默认 *插槽* 中
* 将文本内容放在默认的 *插槽* 中

![Avatar Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/avatar-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | Avatar容器，通常包含 [MIcon](blazor/components/icons/) 或 [MImage](blazor/components/images/) 组件 |

## 示例

### 属性

#### 尺寸

`Size` 属性允许你定义 **MAvatar** 的高度和宽度。此属性以1比1的纵横比均匀进行缩放。如果 `Size` 属性与 `Height` 和 `Width` 属性一起使用，那么 `Size` 属性将会被覆盖。

<masa-example file="Examples.components.avatars.Size"></masa-example>

#### 方块

`Tile` 属性移除了 **MAvatar** 的边界半径，只留下一个简单的方形头像。

<masa-example file="Examples.components.avatars.Tile"></masa-example>

### 插槽

#### 默认值

**MAvatar** 默认插槽可以配合 **MIcon** 组件、图片或文本一起使用。 将这些属性与其他组件混合搭配以创造出独一无二的东西。

<masa-example file="Examples.components.avatars.Default"></masa-example>

### 其他

#### 高级用法

将头像与其他组件组合在一起，您就可以构建出漂亮的用户界面。

<masa-example file="Examples.components.avatars.Other"></masa-example>

#### 菜单

例：将头像和菜单结合起来。

<masa-example file="Examples.components.avatars.Menu"></masa-example>

#### 个人名片

使用 `Tile` 属性，我们可以创建一个名片。

<masa-example file="Examples.components.avatars.BusinessCard"></masa-example>


