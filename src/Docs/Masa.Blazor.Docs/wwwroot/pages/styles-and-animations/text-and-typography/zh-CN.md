# Text And Typography（文本和排版）

控制文本大小、对齐、换行、溢出、变形等等。

## 示例

### 属性

#### 装饰线

使用 `.text-decoration-none` 移除文本装饰线或使用 `.text-decoration-overline`, `.text-decoration-underline`, 和 `.text-decoration-line-through` 添加一个 上划线, 下划线或删除线。

<masa-example file="Examples.styles_and_animations.text_and_typography.Decoration"></masa-example>

#### 字体强调

Material 设计, 默认情况下, 支持 **100, 300, 400, 500, 700, 900** 字体宽度和斜体文本.

<masa-example file="Examples.styles_and_animations.text_and_typography.FontEmphasis"></masa-example>

#### 不透明度

不透明度辅助类可以让您轻松调整文本的重点。 `text--primary` 与默认文本具有相同的不透明度。 `text--secondary` 用于提示和辅助文本。 使用 `text--disabled` 去除强调文本。

<masa-example file="Examples.styles_and_animations.text_and_typography.Opacity"></masa-example>

#### 响应式显示

还有可用于支持响应式显示的 alignment 类。

<masa-example file="Examples.styles_and_animations.text_and_typography.ResponsiveDisplays"></masa-example>

#### RTL 对齐

当使用 RTL 时, 无论 **rtl** 指定如何，您可能都希望保持对齐 。 这可以通过以下格式的文本对齐辅助类实现:  `text-<breakpoint>-<direction>`, breakpoint 可以是 `sm`, `md`, `lg`, 或 `xl` , direction 可以是 `left` 或 `right`。 你也可以通过使用 `start` 和 `end` 对齐rtl.

<masa-example file="Examples.styles_and_animations.text_and_typography.RTLAlignment"></masa-example>

#### 文本对齐

Alignment 助手类允许你轻松的创建 re-align 文本。

<masa-example file="Examples.styles_and_animations.text_and_typography.TextAlignment"></masa-example>

#### 变形

文本 capitalization 类可以转换文字的大小写。也可以断开文本或删除 `text-transform` . 在第一个示例中, 自定义类 `text-transform: uppercase` 将被覆盖，并允许保留文本大小写。 在第二个示例中，我们将一个较长的单词拆分以填充可用空间。

<masa-example file="Examples.styles_and_animations.text_and_typography.Transform"></masa-example>

#### 文本截取

可以使用 `.text-truncate` 实用程序类使用文本省略号截断较长的内容。

<div role="alert" class="m-alert m-alert--doc m-sheet theme--dark m-alert--border m-alert--text m-alert--border-left info--text" type="info">
    <div class="m-alert__wrapper"><span aria-hidden="true" class="m-icon notranslate m-alert__icon theme--dark info--text"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" role="img" aria-hidden="true" class="m-icon__svg">
                <path d="M11,9H13V7H11M12,20C7.59,20 4,16.41 4,12C4,7.59 7.59,4 12,4C16.41,4 20,7.59 20,12C20,16.41 16.41,20 12,20M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M11,17H13V11H11V17Z"></path>
            </svg></span>
        <div class="m-alert__content">
            <p><strong>Requires</strong> <code>display: inline-block</code> <strong>or</strong> <code>display: block</code>.</p>
        </div>
        <div class="m-alert__border m-alert__border--left"></div>
    </div>
</div>

<masa-example file="Examples.styles_and_animations.text_and_typography.Truncated"></masa-example>

#### 排版

使用排版辅助类控制文本的大小和样式.

<masa-example file="Examples.styles_and_animations.text_and_typography.Typography"></masa-example>

#### 排版说明

这些类可以应用于从 `xs` 到 `xl`的所有断点。 当使用基础类 `.text-{value}` 时, 它被推断为  `.text-xs-${value}`.

- `.text-{value}` 用于 `xs`
- `.text-{breakpoint}-{value}` 用于 `sm`, `md`, `lg` 和 `xl`

该 _value_ 属性的值是以下之一：

- `h1`
- `h2`
- `h3`
- `h4`
- `h5`
- `h6`
- `subtitle-1`
- `subtitle-2`
- `body-1`
- `body-2`
- `button`
- `caption`
- `overline`

<br>

<div role="alert" class="m-alert m-alert--doc m-sheet theme--dark m-alert--border m-alert--text m-alert--border-left success--text" type="success">
    <div class="m-alert__wrapper"><span aria-hidden="true" class="m-icon notranslate m-alert__icon theme--dark success--text"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" role="img" aria-hidden="true" class="m-icon__svg">
                <path d="M12,2C17.52,2 22,6.48 22,12C22,17.52 17.52,22 12,22C6.48,22 2,17.52 2,12C2,6.48 6.48,2 12,2M11,16.5L18,9.5L16.59,8.09L11,13.67L7.91,10.59L6.5,12L11,16.5Z"></path>
            </svg></span>
        <div class="m-alert__content">
            <p><strong>提示</strong></p>
            <p>在v2.3.0之前的所有版本中，这些类都是以下内容之一：</p> <br>
            <ul>
                <li><code>.display-4</code></li>
                <li><code>.display-3</code></li>
                <li><code>.display-2</code></li>
                <li><code>.display-1</code></li>
                <li><code>.headline</code></li>
                <li><code>.title</code></li>
                <li><code>.subtitle-1</code></li>
                <li><code>.subtitle-2</code></li>
                <li><code>.body-1</code></li>
                <li><code>.body-2</code></li>
                <li><code>.caption</code></li>
                <li><code>.overline</code></li>
            </ul>
        </div>
        <div class="m-alert__border m-alert__border--left"></div>
    </div>
</div>

下面的示例演示如何在不同的断点显示不同的大小：

<masa-example file="Examples.styles_and_animations.text_and_typography.TypographyIllustrate"></masa-example>

#### 文本换行和溢出

您可以使用 `.text-no-wrap` 工具类来防止文本换行。

<masa-example file="Examples.styles_and_animations.text_and_typography.WrappingAndOverflow"></masa-example>