# Frequently asked questions

Stuck on a particular problem? Check some of these common gotchas before creating a ticket. If you still cannot find what you are looking for, you can submit an [issue](https://github.com/masastack/MASA.Blazor/issues) on GitHub or add wechat on the right to ask us.

## Table of contents

- [How do I vertically center content?](#vertical-center-content)
- [How do I automatically highlight the navigation corresponding to the route?](#highlight-navigation)
- [Why can't I use components starting with P?](#p-starting-components)
- [cannot convert from 'method group' to 'EventCallback'](#cannot-convert-from-method-group-to-eventcallback)


## Questions

- **How do I vertically center content?** { #vertical-center-content }

  Apply the `fill-height` css class to the **MContainer**. This helper class adds **height: 100%** but also adds the classes required to vertically center the content for containers.

- **How do I automatically highlight the navigation corresponding to the route?** { #highlight-navigation }

  Turn on the `Routable` parameter, which will automatically highlight the navigation corresponding to the route. The components that support this feature include: **MList**, **MBreadcrumbs**, **MTabs** and **MBottomNavigation**.

- **Why can't I use components starting with P?** { #p-starting-components }

  The components starting with P are preset components, which are all under the namespace **MASA.Blazor.Presets**. You only need to specify the namespace to use, or add a global namespace reference in `_Imports.razor`.

- **cannot convert from 'method group' to 'EventCallback'** { #cannot-convert-from-method-group-to-eventcallback }

  If there are generic parameters in the method, you need to specify the generic type. For example, when using the `OnSelectedItemUpdate` event in the **MSelect** component, you need to specify the generic type as follows:

  ``` razor l:1
  <MSelect TItem="string"
           OnSelectedItemUpdate="OnUpdate">
  </MSelect>
  ```
