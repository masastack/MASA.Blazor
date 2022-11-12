---
title: App bars
desc: "appbar component is pivotal to any graphical user interface (GUI), as it generally is the primary source of site navigation."
related:
  - /components/buttons
  - /components/icons
  - /components/toolbars
---

## Usage

The `MAppBar` component is used for application-wide actions and information.

<appBars-usage></app-bars-usage>

## Anatomy

## API

- [MAppBar](/api/MAppBar)
- [MAppBarNavIcon](/api/MAppBarNavIcon)
- [MAppBarTitle](/api/MAppBarTitle)

## SubComponents

- `MAppBarNavIcon`：A stylized icon button component created specifically for use with [MToolbar](/components/toolbars) and `MAppBar`. The hamburger menu is displayed on the left side of the toolbar, which is usually used to control the status of the navigation drawer. The default slot can be used to customize the icons and functions of this component.
- `MAppBarTitle`：The modified [MToolbarTitle](/components/toolbars) component is used in conjunction with the **shrinkonscroll** attribute. On the small screen, **MToolbarTitle**
Will be truncated, but this component uses absolute positioning when expanding to make its contents visible. We do not recommend that you use the `MAppbarTitle` component without using the **shrinkonscroll** attribute. It's really because the resize event was added to this component and a lot of additional calculations were done.

## Caveats

<!--alert:warning-->
When `MButton` with **icon** attribute is used inside `MToolbar` and `MAppbar`, they will automatically increase their size and apply negative margins to ensure appropriate spacing according to material design specifications.
If you choose to wrap the buttons in any container, such as `div`, you need to apply a negative margin to the container in order to align them correctly.
<!--/alert:warning-->


## Examples

### Props

#### Collapsible bars

With the collapse and collapse-on-scroll props you can easily control the state of toolbar that the user interacts with.

<example file="" />


#### Dense

You can make MAppBar dense. A dense app bar has lower height than regular one.

<example file="" />

#### Elevate on scroll 

When using the **ElevateOnScroll** prop, the `MAppBar` will rest at an elevation of 0dp until the user begins to scroll
down. Once scrolling, the bar raises to 4dp.

<example file="" />

#### Fade image on scroll

The background image of a `MAppBar` can fade on scroll. Use the **FadeImgOnScroll** property for this.

<example file="" />

#### Hidden on scroll

`MAppBar` can be hidden on scroll. Use the **HideOnScroll** property for this.

<example file="" />

#### Images

`MAppBar` can contain background images. You can set source via the **Src** prop. If you need to customize the `MImage`, the app-bar provides you with an **ImgContent** slot.

<example file="" />

#### Inverted scrolling

When using the **InvertedScroll** property, the bar will hide until the user scrolls past the designated threshold. Once
past the threshold, the `MAppBar` will continue to display until the users scrolls up past the threshold. If no **
ScrollThreshold** value is supplied a default value of 0 will be used.

<example file="" />

#### Prominent

An `MAppBar` with the **Prominent** prop can opt to have its height shrunk as the user scrolls down. This provides a
smooth transition to taking up less visual space when the user is scrolling through content. Shrink height has 2
possible options, **Dense** (48px) and **Short** (56px) sizes.

<example file="" />

#### Scroll threshold

`MAppBar` can have scroll threshold. It will start reacting to scroll only after defined via **ScrollThreshold**
property amount of pixels.

<example file="" />


### Misc

#### Menu

You can easily extend the functionality of app bar by adding **MMenu** there. Click on last icon to see it in action.

<example file="" />

#### Toggle navigation drawers

Using the functional component `MAppBarNavIcon` you can toggle the state of other components such as
a [**MNavigationDrawer**](/components/navigation-drawers).

<example file="" />