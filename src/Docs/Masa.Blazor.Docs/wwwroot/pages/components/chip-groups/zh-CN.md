---
title: Chip Groups（纸片组）
desc: "**MChipGroup** 通过提供可分组的功能来增强 **MChip** 组件。 它使用纸片来创建选择组。"
related:
  - /blazor/components/chips
  - /blazor/components/slide-groups
  - /blazor/components/item-groups
---

## 使用

纸片组使得用户可以很容易地为更复杂的实现选择过滤选项。 默认情况下 **MChipGroup** 会溢出到右边, 但可以更改为只允许 `Column` 的模式。

<chip-groups-usage></chip-groups-usage>

## 示例

### 属性

#### 列

使用 `Column` 属性的纸片组可以包装它们的纸片。

<masa-example file="Examples.components.chip_groups.Column"></masa-example>

#### 过滤结果

使用 `Filter` 属性轻松创建提供额外反馈的纸片组。 这就与用户选中的纸片产生了一种可供选择的视觉样式。

<masa-example file="Examples.components.chip_groups.FilterResults"></masa-example>

#### 必填项

具有 `Mandatory` 属性的纸片组必须始终选择一个值。

<masa-example file="Examples.components.chip_groups.Mandatory"></masa-example>

#### 多选

具有 `Multiple` 属性的纸片组可以选择多个值。

<masa-example file="Examples.components.chip_groups.Multiple"></masa-example>

### 其他

#### 产品卡

**MChip** 组件可以有一个用于其 `@bind-Value` 的明确值。 这会传递到 **MChipGroup** 组件并且在您不想使用纸片索引作为其值时非常有用。

<masa-example file="Examples.components.chip_groups.ProductCard"></masa-example>

#### 牙刷卡

纸片组允许创建自定义接口，这些接口执行与项组或单选控件相同的操作，但在风格上有所不同。

<masa-example file="Examples.components.chip_groups.ToothbrushCard"></masa-example>