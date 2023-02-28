---
title: Autocompletes（自动补全）
desc: "**MAutocomplete** 组件提供简单且灵活的自动补全功能 且支持查找大规模数据甚至是从API请求的动态数据"
related:
  - /blazor/components/combobox
  - /blazor/components/forms
  - /blazor/components/selects
---

## 使用

自动补全组件拓展了 **MSelect** 并且添加了过滤项目的功能

<autocompletes-usage></autocompletes-usage>

## 注意

<app-alert type="error" content="当使用一个Object(对象) 作为`Items`的属性时，你必须使用`ItemText`和`ItemValue`与传入的对象关联起来。 这些值默认为 `Text` 和 `Value` 且可以更改。"></app-alert>

<app-alert type="warning" content="`MenuProps` 的 `Auto` 属性只支持默认输入样式。"></app-alert>

<app-alert type="info" content="浏览器自动补全默认设置为关闭，可能因不同的浏览器而变化或忽略。 **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**"></app-alert>

## 示例

### 属性

#### 紧凑

你可以使用 `dense` 属性来降低自动补全的高度和缩小列表项的最大高度。

<masa-example file="Examples.components.autocompletes.Dense"></masa-example>

#### 过滤器

`filter` 属性可以用自定义的逻辑来过滤单独的项目，在这个例子中我们使用数据源 `_states` 中的Name来过滤项目

<masa-example file="Examples.components.autocompletes.Filter"></masa-example>

### 插槽

#### 项目和选择项

你可以使用插槽自定义被选中时的视觉效果。 在这个示例中，我们为 **MChip** 和 **MListItem** 添加了头像。

<masa-example file="Examples.components.autocompletes.ItemAndSelection"></masa-example>

### 其他

#### API查找

轻松绑定动态数据并创建独特的体验。 **MAutocomplete** 的扩展支持列表使得很容易调节每个方面的输入。

<masa-example file="Examples.components.autocompletes.ApiSearch"></masa-example>

#### 异步项目

有时您需要基于搜索查询加载外部的数据。 使用 `OnSearchInputUpdate` 与 `CacheItems` 属性时这将保持一个唯一的列表，它的所有项目都被传递到items属性。当使用异步项目和`Multiple` 属性时 `CacheItems` 是必须的。

<masa-example file="Examples.components.autocompletes.AsynchronousItems"></masa-example>

#### 加密货币选择器

**MAutocomplete** 组件非常灵活，可以适合任何使用情况。使用 **SelectionContent** 插槽可以轻松自定义您的应用程序所需要的外观以提供独特的用户体验. 

<masa-example file="Examples.components.autocompletes.CryptocurrencySelector"></masa-example>

#### 状态选择器

结合使用 **MAutocomplete** 插槽和过渡，您可以创建一个现代的的可切换的自动补全栏，例如这个状态选择器。

<masa-example file="Examples.components.autocompletes.StateSelector"></masa-example>

