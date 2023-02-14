---
title: File inputs（文件上传）
desc: "**MFileInput** 是一个定制的输入组件，它提供了一个干净的选择界面，显示详细的选择信息和上传进度。 它意在直接取代标准文件输入。"
related:
  - /blazor/components/text-fields
  - /blazor/components/forms
  - /blazor/components/icons
---

## 使用

**MFileInput** 组件的核心是一个基于 [MTextField](/blazor/components/text-fields) 拓展的基本容器。

<file-inputs-usage></file-inputs-usage>

## 示例

### 属性

#### 接受格式

**MFileInput** 组件可以选择接收你想要的媒体格式/文件类型 查看 [accept attribute](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/file#accept) 文档来获取更多信息。

<masa-example file="Examples.components.file_inputs.Accept"></masa-example>

#### 纸片

上传的文件可以作为 [chip（纸片）](/blazor/components/chips) 显示。同时启用 `Chips`（纸片）和 `Multiple`（多选）属性时，每个文件作为一个纸片显示（与选中文件数相反）。

<masa-example file="Examples.components.file_inputs.Chips"></masa-example>

#### 计数器

当 `ShowSize` 属性和 `Counter` 一同启用时，会下输入框下方显示文件总数和大小。

<masa-example file="Examples.components.file_inputs.Counter"></masa-example>

#### 密集

您可以使用 `Dense` 属性降低文件输入高度。

<masa-example file="Examples.components.file_inputs.Dense"></masa-example>

#### 多选

启用 `Multiple`（多选）属性可以使 **MFileInput** 同时包含多个文件。

<masa-example file="Examples.components.file_inputs.Multiple"></masa-example>

#### 前置图标

**MFileInput** 拥有一个默认 `PrependIcon`属性可以直接在组件上设置Icon图标或者全局调整。 有关更改全局组件的更多信息，请参见[自定义图标页面](/blazor/features/icon-fonts)。

<masa-example file="Examples.components.file_inputs.PrependIcon"></masa-example>

#### 显示大小

`ShowSize`（显示大小）属性可以配置显示所选文件的尺寸 显示文件大小可以选择_1024_进位（提供**true**时默认使用）或_1000_进位。

<masa-example file="Examples.components.file_inputs.ShowSize"></masa-example>

#### 验证

与其他输入类似，您可以使用 `Rules` 属性来创建您自己的自定义验证参数。

<masa-example file="Examples.components.file_inputs.Validation"></masa-example>

### 插槽

#### 选项

使用 **SelectionContent**，您可以自定义输入选择的外观。 通常使用 [chips](/blazor/components/chips)完成，但您可以使用任何组件或标记。

<masa-example file="Examples.components.file_inputs.Selection"></masa-example>

### 其他

#### 复杂选项插槽

选项插槽的灵活性使其可以容纳复杂的用途。 在本示例中我们展示了如何只显示前两个文件并将剩余数量以计数器显示（当选中三个以上的文件时）。

<masa-example file="Examples.components.file_inputs.ComplexSelectionContent"></masa-example>