---
category: Components
subtitle: 卡片
type: 卡片
title: Cards
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/rrwbSt3FQ/Cards.svg
related:
  - /components/buttons
  - /components/images
  - /stylesandanimations/text-and-typography
---

`MCard` 组件是一个可用于从面板到静态图像的多功能组件。 卡 组件有许多帮助程序组件来尽可能简单地进行标记。 没有列出选项的组件使用Blazor的功能组件来更快渲染并充当标记糖以使建筑变得更加容易。

## API

- [MCard](/api/MCard)
- [MCardActions](/api/MCardActions)
- [MCardSubtitle](/api/MCardSubtitle)
- [MCardText](/api/MCardText)
- [MCardTitle](/api/MCardTitle)

## 功能组件

- `MCardActions`：用于为卡片放置 动作 的容器，如 [MButton](/components/buttons) 或 [MMenu](/components/menus)。 同时在按钮上使用 个特殊边距
  ，以便它们与其他卡片内容区域的匹配。
- `MCardSubtitle`：为卡片字幕提供默认的 **字体大小** 和 **填充**。 字体大小可以以 [排版类](/stylesandanimations/text-and-typography) 覆盖。
- `MCardText`：主要用于卡片中的 **文本内容**。 对文本应用填充，将其字体大小减少为 .875rem。
- `MCardTitle`：为卡片字幕提供默认的 字体大小 和 填充。 字体大小可以以 [排版类](/stylesandanimations/text-and-typography) 覆盖。
