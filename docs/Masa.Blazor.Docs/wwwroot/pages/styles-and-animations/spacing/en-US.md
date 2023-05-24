# Spacing

Do not update your layout by creating new classes. Auxiliary auxiliary classes modify the filling and edges of elements.

## Start

**How ​​to run**

The **margin** or **padding** range of the auxiliary class applied to the element is from 0 to 16. Each size increment is consistent with the Material Design margin specification. These classes can be passed `{property}{direction}- {size}` format used.

**property** Application spacing type:

* `m`-apply `margin`
* `p`-apply `padding`

**direction** specifies the side to which this attribute applies:

* `t`-apply the spacing between `margin-top` and `padding-top`
* `b`-Apply the spacing between `margin-bottom` and `padding-bottom`
* `l`-apply the spacing between `margin-left` and `padding-left`
* `r`-apply the spacing between `margin-right` and `padding-right`
* `s`-Apply the spacing between `margin-left/padding-left` (LTR mode) and `margin-right/padding-right (RTL mode)`
* `e`-Apply the spacing between `margin-right/padding-right` (LTR mode) and `margin-left/padding-left (RTL mode)`
* `x`-apply the spacing between `*-left` and `*-right`
* `y`-apply the spacing between `*-top` and `*-bottom`
* `a`-apply the spacing in all directions

**size** Controls the spacing properties in 4px increments:

* `0`-Set to 0 to eliminate all `margin` or `padding`.
* `1`-Set `margin` or `padding` to 4px
* `2`-Set `margin` or `padding` to 8px
* `3`-Set `margin` or `padding` to 12px
* `4`-Set `margin` or `padding` to 16px
* `5`-Set `margin` or `padding` to 20px
* `6`-Set `margin` or `padding` to 24px
* `7`-Set `margin` or `padding` to 28px
* `8`-Set `margin` or `padding` to 32px
* `9`-Set `margin` or `padding` to 36px
* `10`-Set `margin` or `padding` to 40px
* `11`-Set `margin` or `padding` to 44px
* `12`-Set `margin` or `padding` to 48px
* `13`-Set `margin` or `padding` to 52px
* `14`-Set `margin` or `padding` to 56px
* `15`-Set `margin` or `padding` to 60px
* `16`-Set `margin` or `padding` to 64px
* `n1`-set `margin` to -4px
* `n2`-set `margin` to -8px
* `n3`-set `margin` to -12px
* `n4`-set `margin` to -16px
* `n5`-set `margin` to -20px
* `n6`-set `margin` to -24px
* `n7`-set `margin` to -28px
* `n8`-set `margin` to -32px
* `n9`-set `margin` to -36px
* `n10`-set `margin` to -40px
* `n11`-set `margin` to -44px
* `n12`-set `margin` to -48px
* `n13`-set `margin` to -52px
* `n14`-set `margin` to -56px
* `n15`-set `margin` to -60px
* `n16`-set `margin` to -64px
* `auto`-set the spacing to auto

Use the playground to learn what the different helper classes can do. To understand **how they work**, see the section below.

<masa-example file="Examples.styles_and_animations.spacing.Start"></masa-example>

### Misc

#### Breakpoints

MASA Blazor uses Flexbox to construct a 12-point grid system. Spacing is used to create a specific layout of application content. It contains 5 media breakpoint queries to determine the size or orientation of a specific screen: **xs**, **sm* *, **md**, **lg** and **xl**. The default resolution is defined in the view breakpoint table below.

<breakpoint-table></breakpoint-table>

The helper class applies **margin** or **padding** at a given breakpoint. These classes can be used in the format `{property}{direction}-{breakpoint}-{size}`. This does not apply to **xs** because it is inferred; for example: `ma-xs-2` is equal to `ma-2`.

<masa-example file="Examples.styles_and_animations.spacing.Breakpoints"></masa-example>

#### Horizontal

Using the margin helper classes you can easily center content horizontally.

<masa-example file="Examples.styles_and_animations.spacing.Horizontal"></masa-example>

#### Margin

It is also possible to use negative margins ranging from **1** to **16**.

<masa-example file="Examples.styles_and_animations.spacing.Margin"></masa-example>