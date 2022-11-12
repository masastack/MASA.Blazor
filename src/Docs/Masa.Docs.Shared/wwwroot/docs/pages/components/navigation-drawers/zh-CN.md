---
title: Navigation drawers（导航抽屉）
desc: "**MNavigationDrawer** 是用于导航应用程序的组件。 通常被包装在 **MCard** 元素中使用。"
related:
  - /components/lists
  - /components/icons
  - /getting-started/wireframes
---

## 使用

最简单形式的副标题显示带有默认主题的副标题。

<navigation-drawers-usage></navigation-drawers-usage>

## 注意

<!--alert:error-->
如果使用启用了 **App** 属性的 **MNavigationDrawer**，则不需要像示例中那样使用 `Absolute` 属性。
<!--/alert:error-->

<!--alert:info-->
`ExtensionOnHover` 参数不会改变**MMain**的内容区域。 要使内容区域响应`ExtensionOnHover`，请绑定`OnMiniVariantUpdate` 到数据。
<!--/alert:info-->

## 示例

### 属性

#### 底部抽屉

使用 `Bottom` 属性我们能够在移动设备上重新定位抽屉，让其从屏幕底部出来。 这是另一种样式，只能遇到 MobileBreakpoint 时激活。

<example file="" />

#### 悬停时扩展

将组件放置在 **MiniVariant** 模式中，并在悬停时扩展开。不更改 **MMain** 的内容区域。 宽度可以使用 `MiniVariantWidth` 属性来控制。

<example file="" />

#### 永久浮动抽屉

默认情况下，导航抽屉有一个 1px 右边框，将其与内容分开。 在这个例子中，我们要把抽屉从左边分离出来，让它自己浮动。 `Floating` 属性可移除右边的边框(如果使用 `Right` 则移除左边框)。

<example file="" />

#### 图像

通过 `Src` 属性将自定义背景应用于抽屉。 如果你需要自定义 **MImage** 的属性，你可以使用 `ImgContent`。

<example file="" />

#### 迷你模式

当使用 `MiniVariant` 属性时，抽屉将会缩小(默认56px)，并隐藏除第一个元素外的 **Mlist** 内的所有内容。

<example file="" />

#### 右侧

使用 `Right` 属性导航抽屉也可以放置在应用程序（或元素）的右侧。这对于创建带有可能没有任何导航链接的辅助信息的边表也很有用。 当使用 **RTL** 时，您必须为抽屉明确定义 **Right**。

<example file="" />

#### 临时的

使用 `Temporary` 属性将临时抽屉位于它的应用程序上方，在移动设备上默认模拟此抽屉行为。

<example file="" />

### 其他

#### 彩色的抽屉

导航抽屉可以自定义，以适合任何应用程序的设计。 我们在这里使用 **AppendContent** 自定义背景颜色和附加的内容区域。

<example file="" />

#### 组合抽屉

在此示例中，我们定义了一个自定义宽度的抽屉来容纳嵌套。 我们使用 `MRow` 确保抽屉和列表在水平方向彼此相邻。

<example file="" />
