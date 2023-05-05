# 常见问题

在特定问题上被卡住？ 在创建工单之前先回顾下这些常见的问题和答案。如果你仍然找不到你要找的东西，你可以在GitHub上提交[问题](https://github.com/masastack/MASA.Blazor/issues)，或者添加右侧的微信询问我们。

## 目录列表

- [如何垂直居中内容？](#vertical-center-content)
- [如何自动高亮对应路由的导航？](#highlight-navigation)
- [P开头的组件为什么无法使用？](#p-starting-components)
- [无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”](#cannot-convert-from-method-group-to-eventcallback)

## 问题专区

- **如何垂直居中内容？** { #vertical-center-content }

  将 `fill-height` css 应用于 **MContainer**。 这个辅助类通过只增加 **height: 100%**, 但是对于容器, 它还会添加应用所需的类将内容垂直居中。

- **如何自动高亮对应路由的导航？** { #highlight-navigation }

  开启 `Routable` 参数，此参数会自动高亮对应路由的导航。支持此特性的组件包括：**MList**, **MBreadcrumbs**, **MTabs** 和 **MBottomNavigation**。

- **P开头的组件为什么无法使用？** { #p-starting-components }

  P开头的组件是预置组件，预置组件都在命名空间 **MASA.Blazor.Presets** 下。你只需写明命名空间即可使用，或者在 `_Imports.razor` 中添加全局的命名空间引用。

- **无法从“方法组”转换为“Microsoft.AspNetCore.Components.EventCallback”** { #cannot-convert-from-method-group-to-eventcallback }

  如果方法里存在泛型参数，那你需要指明泛型类型。例如在 **MSelect** 组件使用 `OnSelectedItemUpdate` 事件时，你需要指明泛型类型，如下所示：

  ``` razor l:1
  <MSelect TItem="string"
           OnSelectedItemUpdate="OnUpdate">
  </MSelect>
  ```
