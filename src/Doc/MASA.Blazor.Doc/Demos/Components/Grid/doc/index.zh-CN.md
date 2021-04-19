---
category: Components
subtitle: 网格
type: 网格
title: Grid
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/Grid.svg

---

MASA.Blazor 配备了一个使用 flexbox 构建的 12 格网格系统。 网格用于在应用的内容中创建特定的布局。 它包含 5 种类型的媒体断点，用于针对特定的屏幕尺寸或方向，xs、sm、md、lg 和 xl。 这些分辨率在视口断点表中定义如下，可以通过自定义断点进行修改。

## Material Design 断点

| 设备                 | 代码   | 类型                 | 像素范围           |
| -------------------- | ------ | -------------------- | ------------------ |
| Extra small (超小号) | **xs** | 小型号到大型号的手机 | < 600px            |
| Small (小号)         | **sm** | 小型号到中型号的平板 | 600px > < 960px    |
| Medium (中号)        | **md** | 大型号平板到手提电脑 | 960px > < 1264px*  |
| Large (大号)         | **lg** | 桌面端               | 1264px > < 1904px* |
| Extra large (超大号) | **xl** | 4K 和超宽屏幕        | \> 1904px*         |

桌面端上浏览器滚动条的宽度为 * -16px

