# Colors（颜色）

通过 **sass** 和 **javascript**，你可以使用 [Material Design](https://material.io/design/color/the-color-system.html#color-theme-creation) 规范中的所有颜色。 这些值可以在样式表、组件文件和实际组件中通过**颜色类**系统使用。

## 示例

### 其他

#### 类

规范中的每种颜色都会被转换为背景和文本变体，这样你就可以在你的应用程序中通过一个类来设置样式，例如 `<div class="red">` 或 `<span class="red--text">` 。

<masa-example file="Examples.styles_and_animations.color.Class"></masa-example>

#### 文本颜色

文字颜色也支持变暗变亮变体，只需使用 `text--{lighten|darken}-{n}`。

<masa-example file="Examples.styles_and_animations.color.Text"></masa-example>

#### Material 色彩表

以下是按原色分组的 **Material Design** 调色板列表。

<masa-example file="Examples.styles_and_animations.color.Material"></masa-example>

