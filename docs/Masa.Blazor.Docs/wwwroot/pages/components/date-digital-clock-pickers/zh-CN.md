---
title: Date digital clock pickers（日期数字时钟选择器）
desc: "**PDateDigitalClockPicker** 是一个带有数字时钟的日期时间选择组件。"
tag: "预置"
related:
  - /blazor/components/date-pickers
  - /blazor/components/digital-clocks
  - /blazor/components/date-time-pickers
---

## 使用 {#usage}

<masa-example file="Examples.components.date_digital_clock_pickers.Picker"></masa-example>

通过 `DateTimePickerViewType` 枚举设置选择器视图的呈现方式：

| 枚举项     | 描述                                     |
|---------|----------------------------------------|
| `Auto`    | 根据 Mobile 断点自动切换为 Desktop 或 Mobile 的视图 |
| `Compact` | 总是显示紧凑的视图，但根据 Mobile 断点自动使用菜单或对话框弹出    |
| `Dialog`  | 总是使用对话框弹出，但根据 Mobile 断点自动选择是否使用紧凑视图    |
| `Desktop` | 桌面视图，使用非紧凑视图并使用菜单弹出                    |
| `Mobile`  | 移动端视图，使用紧凑视图并使用对话框弹出                   |

## 示例 {#examples}

### 配置默认触发器 {#default-activator released-on=v1.7.0}

默认使用一个 [MTextField](/blazor/components/text-fields) 作为触发器。可以通过 **PDefaultDateTimePickerActivator** 配置默认触发器。

<masa-example file="Examples.components.date_digital_clock_pickers.DefaultActivator"></masa-example>

### 自定义触发器 {#custom-activator}

使用 `ActivatorContent` 插槽自定义触发器。

<masa-example file="Examples.components.date_digital_clock_pickers.CustomActivator"></masa-example>

## 视图组件 {#view-components}

### DateDigitalClockPickerView

<masa-example file="Examples.components.date_digital_clock_pickers.Default"></masa-example>

### DateDigitalClockCompactPickerView

<masa-example file="Examples.components.date_digital_clock_pickers.Compact"></masa-example>
