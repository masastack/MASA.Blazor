---
order: 0
title:
  zh-CN: 默认应用标记
  en-US: Default application markup
---

## zh-CN

这是一个 MASA Blazor 默认应用标记的例子。 只要设置 App 属性，你可以将布局元素放在任何地方。 使您的页面内容与布局元素一起工作的关键组件是 MMain。 MMain 组件将根据您指定的应用程序组件的结构动态调整大小。
当使用 Router 时，建议将你的视图放在 MMain 内。
下面例子中这些组件可以在你的应用中用作布局元素。 这些组件可以混合和匹配，但是每个特定组件在任何时候都只能存在一个。 
每一个应用组件都有一个指定的位置和优先级，影响布局系统中的位置。
`MAppBar`：放在应用顶部，优先级应低于 `MSystemBar`
`MFooter`：总是放在应用底部。
`MNavigationDrawer`：可以放置在应用的左边或右边，并且可以配置在 `MAppBar` 的旁边或下面。

## en-US

This is an example of MASA Blaz or natural application markup. Reset the App property, you can place the element anywhere. The key component that makes your page content and element elements work together is MMain. The MMain component will proceed according to the application you specify. The structure of program components is dynamically resized.
When using Router, it is recommended to put your view in MMain.
In the following example, these components can be used as layout elements in your application. These components can be mixed and matched, but only one of each specific component can exist at any time.
Each application component has a designated position and priority, which affects its position in the layout system.
`MAppBar`: placed at the top of the application, priority should be lower than `MSystemBar`
`MFooter`: always placed at the bottom of the application.
`MNavigationDrawer`: Can be placed on the left or right side of the application, and can be placed beside or below the `MAppBar`.