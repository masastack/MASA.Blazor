---
title: Cards（卡片）
desc: "**MCard** 组件是一个可用于从面板到静态图像的多功能组件。 **MCard** 组件有许多帮助程序组件来尽可能简单地进行标记。 没有列出选项的组件使用Blazor的功能组件来更快渲染并充当标记糖以使构建变得更加容易。"
related:
  - /blazor/components/buttons
  - /blazor/components/images
  - /blazor/styles-and-animations/text-and-typography
---

## 使用

卡中有4个基本组件。 **MCardTitle**, **MCardSubtitle**, **MCardText** 和 **MCardActions**

<cards-usage></cards-usage>

## 组件结构解剖

建议在 `MCard` 内部放置元素：

* 将 `MCardTitle`, `MCardSubtitle` 或其他标题文本放在顶部
* 将 `MCardText` 和其他形式的媒体放在卡头下方
* 在卡片内容后放置 `MCardActions`

![Card Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/card-anatomy.png)

| 元素 / 区域 | 描述 |
| - | - |
| 1. 容器 | 卡片容器包含所有 `MCard` 组件。 由3个主要部分组成：`MCardItem`, `MCardText`, 和 `MCardActions` |
| 2. 标题（可选） | 增强 **font-size** 的标题 |
| 3. 副标题 （可选） | 具有较低强调文本颜色的副标题 |
| 4. 文本（可选） | 具有较低强调文本颜色的内容区域 |
| 5. 操作（可选） | 通常包含一个或多个 [MButton](blazor/components/buttons) 组件的内容区域 |


## 功能组件

#### MCardActions

`MCardActions` 的空间可用于放置用于卡片的选项，例如[MButton](/blazor/components/buttons)按钮组件或者 [MMenu](/blazor/components/menus)菜单组件。 它也在选项按钮上应用了特殊边距排版，使其能够和卡片上的其他内容正确排列起来。

#### MCardSubtitle

`MCardSubtitle` 组件能为卡片的小标或字幕提供其默认的 **字体大小** 和 **填充** 排版。 字体大小可以使用文本和排版中的[文字排版属性](/blazor/styles-and-animations/text-and-typography)进行调整。

#### MCardText

`MCardText` 主要用于显示卡片中的文本内容。 它使卡片中的文本能够进行填充排版，并将字体大小减小至 .875rem 的大小。

#### MCardTitle

`MCardTitle` 为卡片的大标提供了默认的 **字体大小** 和 **填充排版**。 字体大小可以使用文本和排版中的[文字排版属性](/blazor/styles-and-animations/text-and-typography) 进行调整。

## 示例

### 属性

#### 加载

处理用户操作时，卡片可以设置为加载状态。 这会禁用进一步的操作，并通过 [MProgressLinear](/blazor/components/progress-linear) 提供视觉反馈  

<masa-example file="Examples.components.cards.Loading"></masa-example>

#### 轮廓

轮廓卡的高度为 0，并包含软边框。

<masa-example file="Examples.components.cards.Outlined"></masa-example>

### 其他

#### 卡片显示

使用 [ExpandTransition](/blazor/styles-and-animations/transitions) 和 `OnClick` 事件，您的卡片可以获得单击按钮就会激活显示隐藏卡片从而展示更多信息的功能。

<masa-example file="Examples.components.cards.CardReveal"></masa-example>

#### 包含内容

**MCard** 组件可用于包装内容。

<masa-example file="Examples.components.cards.ContentWrapping"></masa-example>

#### 自定义操作

使用简单的条件，您就可以轻松添加隐藏的补充文本直到您开启显示。

<masa-example file="Examples.components.cards.CustomActions"></masa-example>

#### 栅格

使用网格，您可以创建漂亮的布局。 

<masa-example file="Examples.components.cards.Grids"></masa-example>

#### 水平卡片

使用 **MCol** ，您可以创建自定义的水平卡片。 使用 `Contain` 属性缩小 **MImage** 以适应容器内部，而不是覆盖。

<masa-example file="Examples.components.cards.HorizontalCards"></masa-example>

#### 信息卡片

卡片是获取更详细信息的入口点。 为了保持简洁，请确保限制用户可以执行的操作数量。 

<masa-example file="Examples.components.cards.InformationCard"></masa-example>

#### 推特卡片

**MCard** 组件有多个子组件，可以帮助您构建复杂的示例，而不必担心间距。此示例由 **MCardTitle**、**MCardText** 和 **MCardActions** 组件组成。

<masa-example file="Examples.components.cards.TwitterCard"></masa-example>

#### 天气卡片

使用 [MListItem](/blazor/components/lists) 和 [MSlider](/brazor/components/sliders)，我们可以创建一个独特的天气卡。列表组件确保我们具有一致的间距和功能，而滑块组件允许我们为用户提供有用的选择界面。

<masa-example file="Examples.components.cards.WeatherCard"></masa-example>

#### 加载

使用不确定 [MProgressLinear](/blazor/components/progress-linear) 表示加载状态。

<masa-example file="Examples.components.cards.Loading"></masa-example>




