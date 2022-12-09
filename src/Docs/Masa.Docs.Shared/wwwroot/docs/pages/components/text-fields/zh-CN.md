---
title: Text fields（文本框）
desc: "文本框组件用于收集用户提供的信息。"
related:
  - /components/forms
  - /components/selects
  - /components/textareas
---

## 使用

带有占位符和/或标签的简单文本字段。

<text-fields-usage></text-fields-usage>

## 示例

### 属性

#### 计数器

使用 `Counter` 属性通知用户 **MTextarea** 的字符限制。计数器本身不执行任何验证—您需要将其与内部验证系统或第三方库配对。计数器可以定制 **CounterValue** 和 **CounterContent**。

<example file="" />

#### 可清除

您可以使用 `Clearable` 属性清除文本，并可以使用 `ClearIcon` 属性自定义与 `Clearable` 一起使用的图标。

<example file="" />

#### 自定义颜色

您可以选择将文本字段更改为材料设计调色板中的任何颜色。下面是一个带有验证的自定义表单的示例实现。

<example file="" />

#### 自定义文字颜色

您可以选择将输入框中的文本更改为材料设计调色板中的任何颜色。

<example file="" />

#### 密集

您可以使用 `Dense` 属性降低文件输入高度。

<example file="" />

#### 禁用且只读

使用 `Disabled` 或 `Readonly` 属性可以将文本设置成禁用或只读状态。

<example file="" />

#### 填充

文本字段可以与其他(布局)盒子一起使用。

<example file="" />

#### 隐藏详细信息

当 `HideDetails` 设置为 `auto` 时，只有在有信息（提示、错误信息等）显示的情况下，才会显示信息。

<example file="" />

#### 提示

文本字段上的 `Hint` 属性添加了文本字段下方提供的字符串。 使用 `PersistentHint` 在文本字段未被焦点时显示提示。`Solo` 模式不支持提示。

<example file="" />

#### 图标

您可以添加图标到文本字段使用 `PrependIcon`, `AppendIcon` 和 `AppendOuterIcon`。

<example file="" />

#### 数字输入框

仅限数字的输入框。

<example file="" />

#### 轮廓

文本字段可以与其他轮廓设计一起使用。

<example file="" />

#### 前缀和后缀

`Prefix` 和 `Suffix` 属性允许您在文本字段旁边添加和附加不可更改的内联文本。

<example file="" />

#### 形状

`Shaped` 文本字段如果是 `Outlined` 则是圆角的；如果是 `Filled`，则具有更高的   [border-radius](/stylesandanimations/border-radius)

<example file="" />

#### 单行亮色主题

`SingleLine` 文本框的标签不会浮动到焦点或数据之上。

<example file="" />

#### 单独

文本字段可以与另一种 Solo 设计一起使用

<example file="" />

#### 验证

MASA Blazor 包括通过 rules 属性进行的简单验证。属性接受类型函数、布尔值和字符串的混合数组。当输入值改变时，数组中的每个元素都会被验证。函数将当前值作为参数传递，并且必须返回 true / false 或包含错误消息的字符串。

<example file="" />

### 事件

#### 图标事件

`OnPrependClick`, `OnAppendClick`, `OnAppendOuterClick`, 和 `OnClearClick`  将在点击相应的图标时发出。 请注意，如果使用图标插槽，将不会触发这些事件。

<example file="" />

### 插槽

#### 图标插槽

可以使用插槽来扩展输入的功能，而不是使用 `Prepend`/`Append`/`AppendOuter` 图标。

<example file="" />

#### 标签

文本字段标签可以在 **LabelContent** 中定义。

<example file="" />

#### 进度条

您可以显示进度条而不是底线。 您可以使用与文本字段颜色相同的默认不确定进度，也可以使用 **ProgressContent** 指定自定义进度。

<example file="" />

### 其他

#### 带计数器的全宽度

全宽文本字段允许您创建无限输入。 在此示例中，我们使用 **MDivider** 分隔字段。

<example file="" />

#### 密码输入

使用 HTML 输入类型密码可以与附加的图标和回调一起使用来控制可见性。

<example file="" />