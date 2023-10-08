---
title: Inputs filter（输入筛选器）
desc: "**MInputsFilter** 组件提供了在用户按下回车键、选择一个项目或清除输入时触发筛选操作的能力。通常与 **MDataTable** 组件一起使用。"
related:
  - /blazor/components/text-fields
  - /blazor/labs/date-digital-clock-pickers
---

## 使用

支持以下表单组件：

| 组件            | 触发筛选的动作                   |
|---------------|---------------------------|
| MAutocomplete | 选择一个项目，或者多选时点击外部，或者点击清除按钮 | 
| MCascader     | 选择最后一个项目，或者点击清除按钮         |                                 
| MCheckbox     | 点击复选框切                    |
| MRadioGroup   | 点击单选按钮                    |
| MSelect       | 选择一个项目，或者多选时点击外部，或者点击清除按钮 |
| MSwitch       | 点击开关切换                    |                                                                
| MTextField    | 按下回车键，或者点击清除按钮            | 

<masa-example file="Examples.labs.inputs_filter.Usage"></masa-example>

## 数据筛选器

**PDataFilter** 组件内置了 **MInputsFilter** 组件，其中包含内置的重置、搜索和展开/折叠操作。

<masa-example file="Examples.labs.inputs_filter.DataFilter"></masa-example>
