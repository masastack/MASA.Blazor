---
title: Avatars（头像）
desc: "**MAvatar** 组件通常用于显示用户个人资料图片。 此组件允许您动态添加设置响应图像、图标和文字的边框半径。 `Tile` 变量可用来显示无边框半径的头像。"
related:
  - /components/badges
  - /components/icons
  - /components/lists
---

## 使用

头像以最简单的形式在圆形容器中显示内容。

<avatars-usage></avatars-usage>

## 解剖学

## API

- [MAvatar](/api/MAvatar)

## 示例

### 属性

#### 尺寸

`Size` 属性允许你定义m-avatar的高度和宽度。此属性以1比1的纵横比均匀进行缩放。如果`Size` 属性与 `Height` 和 `Width` 属性一起使用，那么 `Size` 属性将会被覆盖。

<example file="" />

#### 方块

`Tile` 属性移除了 **MAvatar** 的边界半径，只留下一个简单的方形头像。

<example file="" />

### 插槽

#### 默认值

**MAvatar** 默认插槽可以配合 **MIcon** 组件、图片或文本一起使用。 将这些 props与其他 props 混合搭配以创造出独一无二的东西。

<example file="" />

### 其他

#### 高级用法

将头像与其他组件组合在一起，你就可以构建漂亮的用户界面。

<example file="" />

例：将头像和菜单结合起来。

<example file="" />

#### 个人名片

使用 `Tile` prop，我们可以创建一个名片。

<example file="" />


