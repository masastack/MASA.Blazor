---
category: Components
subtitle: 工具提示
type: 工具提示
title: Tooltips
cols: 1
related:
  - /components/badges
  - /components/icons
  - /components/menus
---

# Tooltips（工具提示）

当用户悬停在元素上时， **MTooltip** 组件可用于传递信息。 您还可以通过 `@bind-Value` 来控制提示的显示。 当激活时，提示将显示用于标识元素的文本，例如其功能的描述。

## 使用

提示组件可以包装任何元素。

<tooltips-usage></tooltips-usage>

## API

- [MTooltip](/api/MTooltip)

## 注意

<!--alert:info-->
为了正确定位 **MTooltip**，需要一个位置支撑(`Top` | `Bottom` | `Left` | `Right`)。

## 示例

### 属性

#### 对其

提示可以对齐到激活器元素的四个侧面。

<example file="" />

#### 可见性

可使用 `@bind-Value` 编程性修改提示可见性。

<example file="" />