---
title: App bars
desc: "**MAppBar** component is pivotal to any graphical user interface (GUI), as it generally is the primary source of site navigation."
related:
  - /blazor/components/buttons
  - /blazor/components/icons
  - /blazor/components/toolbars
---

## Usage

The **MAppBar** component is used for application-wide actions and information.

<app-bars-usage></app-bars-usage>

## Anatomy

The recommended placement of elements inside of `MAppBar` is:

* Place `MAppBarNavIcon` or other navigation items on the far left
* Place `MAppBarTitle` to the right of navigation
* Place contextual actions to the right of navigation
* Place overflow actions to the far right

![App Bar Anatomy](https://cdn.masastack.com/stack/doc/masablazor/anatomy/app-bar-anatomy.png)

| Element / Area | Description |
| - | - |
| 1. Container | The App Bar container holds all `MAppBar` components |
| 2. App Bar Icon (optional) | A styled icon button component created that is often used to control the state of a `MNavigation Drawer` |
| 3. Title (optional) | A heading with increased **font-size** |
| 4. Action items (optional) | Used to highlight certain actions not in the overflow menu |
| 5. Overflow menu (optional) | Place less often used action items into a hidden menu |

## SubComponents

- **MAppBarNavIcon** ：A stylized icon button component created specifically for use with [MToolbar](/blazor/components/toolbars) and **AppBar**. The hamburger menu is displayed on the left side of the toolbar, which is usually used to control the status of the navigation drawer. The default slot can be used to customize the icons and functions of this component.
- **MAppBarTitle** ：The modified [MToolbarTitle](/blazor/components/toolbars) component is used in conjunction with the `shrinkonscroll` attribute. On the small screen, **MToolbarTitle**
Will be truncated, but this component uses absolute positioning when expanding to make its contents visible. We do not recommend that you use the **MAppbarTitle** component without using the `shrinkonscroll` attribute. It's really because the resize event was added to this component and a lot of additional calculations were done.

## Caveats

<app-alert type="warning" content="When **MButton** with **Icon** attribute is used inside **MToolbar** and **MAppbar**, they will automatically increase their size and apply negative margins to ensure appropriate spacing according to material design specifications.
If you choose to wrap the buttons in any container, such as `div`, you need to apply a negative margin to the container in order to align them correctly."></app-alert>

## Examples

### Props

#### Collapsible bars

With the `collapse` and `collapse-on-scroll` props you can easily control the state of toolbar that the user interacts with.

<masa-example file="Examples.components.app_bars.CollapsibleBars"></masa-example>

#### Dense

You can make MAppBar dense. A dense app bar has lower height than regular one.

<masa-example file="Examples.components.app_bars.Dense"></masa-example>

#### Elevate on scroll 

When using the **ElevateOnScroll** prop, the **MAppBar** will rest at an elevation of 0dp until the user begins to scroll
down. Once scrolling, the bar raises to 4dp.

<masa-example file="Examples.components.app_bars.ElevateOnScroll"></masa-example>

#### Fade image on scroll

The background image of a **MAppBar** can fade on scroll. Use the **FadeImgOnScroll** property for this.

<masa-example file="Examples.components.app_bars.FadeImageOnScroll"></masa-example>

#### Hidden on scroll

**MAppBar** can be hidden on scroll. Use the `HideOnScroll` property for this.

<masa-example file="Examples.components.app_bars.HiddenOnScroll"></masa-example>

#### Images

**MAppBar** can contain background images. You can set source via the `Src` prop. If you need to customize the **MImage**, the app-bar provides you with an **ImgContent** slot.

<masa-example file="Examples.components.app_bars.Images"></masa-example>

#### Inverted scrolling

When using the `InvertedScroll` property, the bar will hide until the user scrolls past the designated threshold. Once
past the threshold, the `MAppBar` will continue to display until the users scrolls up past the threshold. If no **
ScrollThreshold** value is supplied a default value of 0 will be used.

<masa-example file="Examples.components.app_bars.Inverted"></masa-example>

#### Prominent

An **MAppBar** with the `Prominent` prop can opt to have its height shrunk as the user scrolls down. This provides a
smooth transition to taking up less visual space when the user is scrolling through content. Shrink height has 2
possible options, `Dense` (48px) and `Short` (56px) sizes.

<masa-example file="Examples.components.app_bars.Prominent"></masa-example>

#### Scroll threshold

**MAppBar** can have scroll threshold. It will start reacting to scroll only after defined via `ScrollThreshold`
property amount of pixels.

<masa-example file="Examples.components.app_bars.ScrollThreshold"></masa-example>

### Misc

#### Menu

You can easily extend the functionality of app bar by adding **MMenu** there. Click on last icon to see it in action.

<masa-example file="Examples.components.app_bars.Menu"></masa-example>

#### Toggle navigation drawers

Using the functional component **MAppBarNavIcon** you can toggle the state of other components such as
a [MNavigationDrawer](/blazor/components/navigation-drawers).

<masa-example file="Examples.components.app_bars.ToggleNavigationDrawers"></masa-example>