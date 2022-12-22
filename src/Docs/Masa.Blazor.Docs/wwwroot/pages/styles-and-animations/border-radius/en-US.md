# Border radius

Use the border radius auxiliary style to quickly apply the `border-radius` style to the element

## Usage

Examples of usage are shown below. Note: The infix characters sm, lg and xl are related to the frame radius size and are not affected by the breakpoint.

<masa-example file="Examples.styles_and_animations.border_radius.Basic"></masa-example>

## Examples

### Misc

#### Pill and circle

You can use the `.rounded-pill` class to create pills, and the `.rounded-circle` class to create circles.

<masa-example file="Examples.styles_and_animations.border_radius.Round"></masa-example>

#### Removing Border Radius

Use the `.rounded-0` helper class to remove all radii of elements or the radius selected by corners; for example. `.rounded-l-0` and `.rounded-tr-0`.

<masa-example file="Examples.styles_and_animations.border_radius.Remove"></masa-example>

#### Rounding all corners

The **rounded** auxiliary class allows you to modify the border radius of the element. Use `.rounded-sm`, `.rounded`, `.rounded-lg`, and `.rounded-xl` to add different sizes of border radius.

<masa-example file="Examples.styles_and_animations.border_radius.Set"></masa-example>

#### Rounding by side

The border radius can be configured on each side by using **t**, **r**, **b**, **l** built-in classes; e.g. `.rounded-b-xl` and `. rounded-t`.

<masa-example file="Examples.styles_and_animations.border_radius.Side"></masa-example>

#### Rounding by corner

The border radius can be configured on each corner by using tl, tr, br, bl built-in classes; e.g. rounded-br-xl and . rounded-tr.

<masa-example file="Examples.styles_and_animations.border_radius.Horn"></masa-example>