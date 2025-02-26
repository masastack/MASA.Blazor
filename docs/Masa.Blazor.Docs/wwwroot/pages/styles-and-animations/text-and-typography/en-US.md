# Text And Typography

Control text size, alignment, wrapping, overflow, transforms and more.

## Examples

### Props

#### Decoration

Remove text decoration with the `.text-decoration-none` class or add an *overline, underline or line-through* by using `.text-decoration-overline`, `.text-decoration-underline`, and `.text-decoration-line-through`.

<masa-example file="Examples.styles_and_animations.text_and_typography.Decoration"></masa-example>

#### FontEmphasis

Material design, by default, supports **100, 300, 400, 500, 700, 900** font weights and italicized text.

<masa-example file="Examples.styles_and_animations.text_and_typography.FontEmphasis"></masa-example>

#### Opacity

Opacity helper classes allow you to easily adjust the emphasis of text. `text--primary` has the same opacity as default text. `text--secondary` is used for hints and helper text. De-emphasize text with `text--disabled`.

<masa-example file="Examples.styles_and_animations.text_and_typography.Opacity"></masa-example>

#### ResponsiveDisplays

There are also available alignment classes that support responsive displays.

<masa-example file="Examples.styles_and_animations.text_and_typography.ResponsiveDisplays"></masa-example>

#### RTL Alignment

When using RTL, you may want to keep the alignment regardless of the **rtl** designation. This can be achieved using text alignment helper classes in the following format: `text-<breakpoint>-<direction>`, where breakpoint can be `sm`, `md`, `lg`, or `xl` and direction can be `left` or `right`. You may also want alignment to respond to rtl which can be done using directions `start` and `end`.

<masa-example file="Examples.styles_and_animations.text_and_typography.RTLAlignment"></masa-example>

#### TextAlignment

Alignment helper classes allow you to easily re-align text.

<masa-example file="Examples.styles_and_animations.text_and_typography.TextAlignment"></masa-example>

#### Transform

Text can be transformed with text capitalization classes.Text breaking and the removal of `text-transform` is also possible. In the first example, the `text-transform: uppercase` custom class is overwritten and allows the text casing to remain. In the second example, we break up a longer word to fit the available space.

<masa-example file="Examples.styles_and_animations.text_and_typography.Transform"></masa-example>

#### Truncated

Longer content can be truncated with a text ellipsis using the `.text-truncate` utility class.

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

#### Typography

Control the size and style of text using the Typography helper classes.

<masa-example file="Examples.styles_and_animations.text_and_typography.Typography"></masa-example>

#### TypographyIllustrate

These classes can be applied to all breakpoints from `xs` to `xl`. When using a base class, `.text-{value}`, it is inferred to be `.text-xs-${value}`.

- `.text-{value}` for `xs`
- `.text-{breakpoint}-{value}` for `sm`, `md`, `lg` and `xl`

The _value_ property is one of:

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

The following example demonstrates how the various sizes would appear at different breakpoints:

<masa-example file="Examples.styles_and_animations.text_and_typography.TypographyIllustrate"></masa-example>

#### WrappingAndOverflow

You can prevent wrapping text with the `.text-no-wrap` utility class.

<masa-example file="Examples.styles_and_animations.text_and_typography.WrappingAndOverflow"></masa-example>