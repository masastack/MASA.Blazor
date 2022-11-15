---
title: Alerts
desc: "The component is used to convey important information to the user through the use contextual types icons and color.These default types come in in 4 variations: **Success**,**Info**,**Warning**, and **Error**. Default icons are assigned which help represent different actions each type portrays. Many parts of an alert such as **Border**, **Icon**, and **Color** can also be customized to fit almost any situation."
related:
  - /components/buttons
  - /components/icons
  - /components/dialogs
---

## Usage

Alerts in their simplest form are flat sheets of paper that display a message.

<alerts-usage></alerts-usage>

## Anatomy

## Examples

### Props

#### Border

The **Border** prop adds a simple border to one of the 4 sides of the alert. This can be combined with props like *
*Color**, **Dark**, and **Type** to provide unique accents to the alert.

<masa-example file="Examples.alerts.Border"></masa-example>

#### Colored border

The **ColoredBorder** prop removes the alert background in order to accent the **Border** prop. If a **Type** is set, it
will use the type's default color. If no **Color** or **Type** is set, the color will default to the inverted color of
the applied theme (black for light and white/gray for dark).

<masa-example file="Examples.alerts.ColoredBorder"></masa-example>

#### Dense

The **Dense** prop decreases the height of the alert to create a simple and compact style. When combined with the *
*Border** prop, the border thickness will be decreased to stay consistent with the style.

<masa-example file="Examples.alerts.Dense"></masa-example>

#### Dismissible

The **Dismissible** prop adds a close button to the end of the alert component. Clicking this button will set its value
to false and effectively hide the alert. You can restore the alert by binding  **@bind-Value** and setting it to true.
The close icon automatically has an `aria-label` applied that can be changed by modifying the **CloseLabel** prop or
changing **close** value in your locale.

<masa-example file="Examples.alerts.Dismissible"></masa-example>

#### Icon

The **Icon** prop allows you to add an icon to the beginning of the alert component. If a **Type** is provided, this
will override the default type icon. Additionally, setting the **Icon** prop to false will remove the icon altogether.

<masa-example file="Examples.alerts.Icon"></masa-example>

#### Outlined

The **Outlined** prop inverts the style of an alert, inheriting the currently applied **Color**, applying it to the text
and border, and making its background transparent.

<masa-example file="Examples.alerts.Outlined"></masa-example>

#### Prominent

The **Prominent** prop provides a more pronounced alert by increasing the height and applying a halo to the icon. When
applying both **Prominent** and **Dense** together, the alert will take on the appearance of a normal alert but with the
**Prominent** icon effects.

<masa-example file="Examples.alerts.Prominent"></masa-example>

#### Text

The **Text** prop is a simple alert variant that applies a reduced opacity background of the provided **Color**. Similar
to other styled props, **Text** can be combined with other props like **Dense**, **Prominent**, **Outlined**, and *
*Shaped** to create a unique and customized component.

<masa-example file="Examples.alerts.Text"></masa-example>

#### Shaped

The **Shaped** prop will add **border-radius**  at the top-left and bottom-right of the `MAlert`. Similar to other
styled props, **Shaped** can be combined with other props like **Dense**, **Prominent**, **Outlined** and **Text** to
create a unique and customized component

<masa-example file="Examples.alerts.Shaped"></masa-example>

#### Transition

The **Transition** prop allows you to apply a transition to the alert which is viewable when the component hides and
shows. For more information, you can check out any of [prebuilt transitions](/stylesandanimations/transitions).

<masa-example file="Examples.alerts.Transition"></masa-example>

#### Twitter

By combining **Color**, **Dismissible**, **Border**, **Elevation**, **Icon**, and **ColoredBorder** props, you can
create stylish custom alerts such as this Twitter notification.

<masa-example file="Examples.alerts.Twitter"></masa-example>

#### Type

The **Type** prop provides 4 default styles: **Success**, **Info**, **Warning**, and **Error**. Each of these styles
provide a default icon and color.

<masa-example file="Examples.alerts.Type"></masa-example>
