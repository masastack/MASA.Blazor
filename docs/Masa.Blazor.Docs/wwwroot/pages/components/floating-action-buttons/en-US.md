---

title: Floating Action Buttons
desc: "The **MButton** component can be used as a floating action button. This provides an application with a main point of action. Combined with the `MSpeedDial` component, you can create a diverse set of functions available for your users."
related:
  - /blazor/components/buttons
  - /blazor/components/icons
  - /blazor/styles-and-animations/transitions
---

## Usage

Floating action buttons can be attached to material to signify a promoted action in your application. The default size will be used in most cases, whereas the `Small` variant can be used to maintain continuity with similar sized elements.

<floating-action-buttons-usage></floating-action-buttons-usage>

## Examples

### Misc

#### Display animation

When displaying for the first time, a floating action button should animate onto the screen. Here we use the **FabTransition** . You can also use any custom transition provided by Masa Blazor or your own.

<masa-example file="Examples.components.floating_action_buttons.Animation"></masa-example>

#### Lateral screens

When changing the default action of your button, it is recommended that you display a transition to signify a change. We do this by binding the `Key` prop to a piece of data that can properly signal a change in action to the Masa Blazor transition system. While you can use a custom transition for this, ensure that you set the `Mode` prop to `OutIn`.

<masa-example file="Examples.components.floating_action_buttons.LateralScreens"></masa-example>

#### Small variant

For better visual appeal, we use a small button to match our list avatars.

<masa-example file="Examples.components.floating_action_buttons.SmallVariant"></masa-example>

#### Speed dial

**MSpeedDial** component has a very robust api for customizing your FAB experience exactly how you want.

<masa-example file="Examples.components.floating_action_buttons.SpeedDial"></masa-example>




