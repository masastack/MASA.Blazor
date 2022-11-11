---
title: Selects（下拉框）
desc: "选择器组件用于从选项列表中收集用户提供的信息。"
related:
  - /components/autocompletes
  - /components/combobox
  - /components/forms
---

## 使用

<selects-usage></selects-usage>

## 注意

<!--alert:info--> 
浏览器自动补全默认设置为关闭，可能因不同的浏览器而变化或忽略。 **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 

<!--alert:warning--> 
**MenuProps** 的 **Auto** 属性只支持默认输入样式。
<!--/alert:warning--> 

<!--alert:error--> 
当使用一个Object(对象) 作为**Items**的属性时，你必须使用**ItemText**和**ItemValue**与传入的对象关联起来。 这些值默认为 **Text** 和 **Value** 且可以更改。
<!--/alert:error--> 

## 示例

### 属性

#### 自定义选项的文本和值

您可以在您的项目中指定特定属性的值和文本字段相对应。 默认情况下，将是属性 text 和 value相对应。 而在这个示例中，我们将应用 `OnSelectedItemUpdate` 属性的方式来返回所选项目的整个对象值。

<example file="" />

#### 密集

你可以使用 `Dense` 属性来降低自动完成的高度和缩小列表项的最大高度。

<example file="" />

#### 禁用

将 `Disabled` 的属性应用于 **MSelect** 将阻止用户与组件交互。

<example file="" />

#### 图标

使用自定义前置或者后置图标。

<example file="" />

#### 浅色主题

标准的单选有多种配置选项。

<example file="" />

#### 菜单属性

在这个示例中，菜单被指定向上打开并移动至顶部。

<example file="" />

#### 多选

多选择器可以使用 **MChip** 组件来显示已选项。

<example file="" />

#### 只读

您可以在 **MSelect** 上应用 `Readonly`属性来防止用户更改其值。

<example file="" />

### 插槽

#### 附加代码

**MSelect** 组件可以通过预定和附加项目进行扩展。 这完全适合自定义的 **选择所有** 功能。

<example file="" />

#### 选择

**SelectionContent** 可用于自定义选中值在输入中显示的方式。 当您想要像 `foo (+2 others)` 或不想让选区占用多行时，这是很棒的。

<example file="" />