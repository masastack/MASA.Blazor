---
category: Components
type: Cards
title: Cards
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/rrwbSt3FQ/Cards.svg
related:
  - /components/buttons
  - /components/images
  - /stylesandanimations/text-and-typography
---

The MCard component is a versatile component that can be used for anything from a panel to a static image. The card component has numerous helper components to make markup as easy as possible. Components that have no listed options use MASA Blazor functional component option for faster rendering and serve as markup sugar to make building easier. 

## API

- [MCard](/api/MCard)
- [MCardActions](/api/MCardActions)
- [MCardSubtitle](/api/MCardSubtitle)
- [MCardText](/api/MCardText)
- [MCardTitle](/api/MCardTitle)


## 功能组件

- `MCardActions`:A container used to place actions for cards, such as [MButton] (/components/buttons) or [MMenu] (/components/menu). Use a special margin on the button at the same time
So that they match other card content areas.
- `MCardSubtitle`:Provide default **font size** and **fill** for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).
- `MCardText`:It is mainly used for **text content** in cards. Apply padding to the text and reduce its font size to. 875rem.
- `MCardTitle`:Provide default font size and padding for card subtitles. Font size can be overwritten with [typesetting] (/stylesandimages/text-and-typography).