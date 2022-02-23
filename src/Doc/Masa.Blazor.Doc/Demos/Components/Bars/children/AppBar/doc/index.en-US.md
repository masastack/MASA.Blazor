---
category: Components
type: AppBars
title: App bars
cols: 1
related:
  - /components/buttons
  - /components/icons
  - /components/toolbars
---

appbar component is pivotal to any graphical user interface (GUI), as it generally is the primary source of site navigation.

## API

- [MAppBar](/api/MAppBar)
- [MAppBarNavIcon](/api/MAppBarNavIcon)
- [MAppBarTitle](/api/MAppBarTitle)

## 功能组件

- `MAppBarNavIcon`：A stylized icon button component created specifically for use with [MToolbar] (/components/toolbars) and 'MAppBar'. The hamburger menu is displayed on the left side of the toolbar, which is usually used to control the status of the navigation drawer. The default slot can be used to customize the icons and functions of this component.
- `MAppBarTitle`：The modified [MToolbarTitle] (/components/toolbars) component is used in conjunction with the * * shrinkonscroll * * attribute. On the small screen, * * MToolbarTitle**
Will be truncated, but this component uses absolute positioning when expanding to make its contents visible. We do not recommend that you use the 'MAppbarTitle' component without using the * * shrinkonscroll * * attribute. It's really because the resize event was added to this component and a lot of additional calculations were done.

## 注意事项

<!--alert:warning-->
When 'MButton' with **icon** attribute is used inside 'MToolbar' and 'MAppbar', they will automatically increase their size and apply negative margins to ensure appropriate spacing according to material design specifications.
If you choose to wrap the buttons in any container, such as' div ', you need to apply a negative margin to the container in order to align them correctly.
<!--/alert:warning-->