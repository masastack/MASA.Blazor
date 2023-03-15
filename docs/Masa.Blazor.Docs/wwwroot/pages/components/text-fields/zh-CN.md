---
title: Text fields（文本框）
desc: "文本框组件用于收集用户提供的信息。"
related:
  - /blazor/components/forms
  - /blazor/components/selects
  - /blazor/components/textareas
---

## 使用

带有占位符和/或标签的简单文本字段。

<text-fields-usage></text-fields-usage>

## 组件结构解剖

建议在 `MTextField` 中放置元素：

* 在输入或标签的开头放置 `MIcon`
* 将标签放在预先准备好的内容之后

![Text-field Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/text-field-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | 文本字段容器包含 `MInput` 和 `MField` 组件 |
| 2. 前置图标 | 位于 `MTextField` 前面的自定义图标 |
| 3. 前置内部图标 | 位于 `MTextField` 开始处的自定义图标 |
| 4. 标签 | 用于向用户显示与输入相关的文本的内容区域 |
| 5. 附加内部图标 | 位于 `MTextField` 组件末尾的自定义图标 |
| 6. 附加图标  | 位于 `MTextField` 组件后面的自定义图标 |

## 示例

### 属性

#### 计数器

使用 `Counter` 属性通知用户 **MTextarea** 的字符限制。计数器本身不执行任何验证—您需要将其与内部验证系统或第三方库配对。计数器可以定制 **CounterValue** 和 **CounterContent**。

<masa-example file="Examples.components.text_fields.Counter"></masa-example>

#### 可清除

您可以使用 `Clearable` 属性清除文本，并可以使用 `ClearIcon` 属性自定义与 `Clearable` 一起使用的图标。

<masa-example file="Examples.components.text_fields.Clearable"></masa-example>

#### 自定义颜色

您可以选择将文本字段更改为材料设计调色板中的任何颜色。下面是一个带有验证的自定义表单的示例实现。

<masa-example file="Examples.components.text_fields.CustomColors"></masa-example>

#### 自定义文字颜色

您可以选择将输入框中的文本更改为材料设计调色板中的任何颜色。

<masa-example file="Examples.components.text_fields.CustomTextColors"></masa-example>

#### 密集

您可以使用 `Dense` 属性降低文件输入高度。

<masa-example file="Examples.components.text_fields.Dense"></masa-example>

#### 禁用且只读

使用 `Disabled` 或 `Readonly` 属性可以将文本设置成禁用或只读状态。

<masa-example file="Examples.components.text_fields.DisabledAndReadonly"></masa-example>

#### 填充

文本字段可以与其他(布局)盒子一起使用。

<masa-example file="Examples.components.text_fields.Filled"></masa-example>

#### 隐藏详细信息

当 `HideDetails` 设置为 `auto` 时，只有在有信息（提示、错误信息等）显示的情况下，才会显示信息。

<masa-example file="Examples.components.text_fields.HideDetails"></masa-example>

#### 提示

文本字段上的 `Hint` 属性添加了文本字段下方提供的字符串。 使用 `PersistentHint` 在文本字段未被焦点时显示提示。`Solo` 模式不支持提示。

<masa-example file="Examples.components.text_fields.Hint"></masa-example>

#### 图标

您可以添加图标到文本字段使用 `PrependIcon`, `AppendIcon` 和 `AppendOuterIcon`。

<masa-example file="Examples.components.text_fields.Icons"></masa-example>

#### 数字输入框

仅限数字的输入框。

<masa-example file="Examples.components.text_fields.Number"></masa-example>

#### 轮廓

文本字段可以与其他轮廓设计一起使用。

<masa-example file="Examples.components.text_fields.Outlined"></masa-example>

#### 前缀和后缀

`Prefix` 和 `Suffix` 属性允许您在文本字段旁边添加和附加不可更改的内联文本。

<masa-example file="Examples.components.text_fields.PrefixesAndSuffixes"></masa-example>

#### 形状

`Shaped` 文本字段如果是 `Outlined` 则是圆角的；如果是 `Filled`，则具有更高的   [border-radius](/blazor/styles-and-animations/border-radius)

<masa-example file="Examples.components.text_fields.Shaped"></masa-example>

#### 单行亮色主题

`SingleLine` 文本框的标签不会浮动到焦点或数据之上。

<masa-example file="Examples.components.text_fields.SingleLine"></masa-example>

#### 单独

文本字段可以与另一种 Solo 设计一起使用

<masa-example file="Examples.components.text_fields.Solo"></masa-example>

#### 验证

MASA Blazor 包括通过 `Rules` 属性进行的简单验证。属性接受类型函数、布尔值和字符串的混合数组。当输入值改变时，数组中的每个元素都会被验证。函数将当前值作为参数传递，并且必须返回 `true` / `false` 或包含错误消息的字符串。

<masa-example file="Examples.components.text_fields.Validation"></masa-example>

### 事件

#### 图标事件

`OnPrependClick`, `OnAppendClick`, `OnAppendOuterClick`, 和 `OnClearClick`  将在点击相应的图标时发出。 请注意，如果使用图标插槽，将不会触发这些事件。

<masa-example file="Examples.components.text_fields.IconEvents"></masa-example>

### 插槽

#### 图标插槽

可以使用插槽来扩展输入的功能，而不是使用 `Prepend`/`Append`/`AppendOuter` 图标。

<masa-example file="Examples.components.text_fields.IconSlots"></masa-example>

#### 标签

文本字段标签可以在 **LabelContent** 中定义。

<masa-example file="Examples.components.text_fields.Label"></masa-example>

#### 进度条

您可以显示进度条而不是底线。 您可以使用与文本字段颜色相同的默认不确定进度，也可以使用 **ProgressContent** 指定自定义进度。

<masa-example file="Examples.components.text_fields.Progress"></masa-example>

### 其他

#### 带计数器的全宽度

全宽文本字段允许您创建无限输入。 在此示例中，我们使用 **MDivider** 分隔字段。

<masa-example file="Examples.components.text_fields.FullWidthWithCounter"></masa-example>

#### 密码输入

使用 HTML 输入类型密码可以与附加的图标和回调一起使用来控制可见性。

<masa-example file="Examples.components.text_fields.PasswordInput"></masa-example>