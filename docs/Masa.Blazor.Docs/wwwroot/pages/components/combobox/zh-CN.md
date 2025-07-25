---
title: Combobox（组合选择框）
desc: "**MCombobox**组件是一个允许用户输入不存在于所提供项目的值的 [MAutocomplete](/blazor/components/autocompletes)。"
release: v1.10.0
related:
  - /blazor/components/autocompletes
  - /blazor/components/cascaders
  - /blazor/components/selects
---

## 使用 {#usage}

<masa-example file="Examples.components.combobox.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 分隔符 {#delimiters}

当用户键入回车和Tab键时，**MCombobox** 组件会将输入的值添加到选项中。您可以使用 `Delimiters` 属性来设置额外的分隔符。

<masa-example file="Examples.components.combobox.Delimiters"></masa-example>

#### 标签 {#chips}

双击默认的标签可以编辑文本。

<masa-example file="Examples.components.combobox.Chips"></masa-example>

### 插槽 {#contents}

#### 无内容的插槽 {#no-data}

自定义无数据内容的插槽可以提示用户输入内容。

<masa-example file="Examples.components.combobox.NoDataContent"></masa-example>
