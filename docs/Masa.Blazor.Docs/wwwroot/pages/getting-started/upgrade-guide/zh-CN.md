# 升级指南

## 从 v1.9.x 升级到 v1.10.0 {#upgrading-from-v1-9-x-to-v1-10-0}

<app-alert type="warning" content="对于 WebAssembly 项目，包括 Auto 模式的 Web App 项目，更新依赖后可能需要删除 `bin` 和 `obj` 文件夹并重新编译。[Issue #2453](https://github.com/masastack/MASA.Blazor/issues/2453)"></app-alert>

对于没有使用 .NET 9 的[映射静态资产](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/fundamentals/static-files?view=aspnetcore-9.0#static-asset-delivery-in-server-side-blazor-apps) 的项目，
请更新 `_Host.cshtml` 或 `index.html` 文件中的 Masa.Blazor 的 CSS 和 JS 引用。

```diff
- <link href="_content/Masa.Blazor/css/masa-blazor.min.css" rel="stylesheet" />
+ <link href="_content/Masa.Blazor/css/masa-blazor.min.css?v=1.10.0" rel="stylesheet" />
- <script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
+ <script src="_content/Masa.Blazor/js/masa-blazor.js?v=1.10.0"></script>
```

### 特性 {#v1-10-0-features}

#### Theme {#v1-10-0-theme}

- 名为 **CascadingIsDark** 的级联已不再使用，改为使用 `string` 类型的 **MasaBlazorCascadingTheme**。
- 所有关于颜色的CSS变量现在只是rgb颜色值，如果你在使用这些变量，请注意修改。
  ```diff
    .test {
  -   color: var(--m-theme-primary);
  +   color: rgba(var(--m-theme-primary));
    }
  ```
- `Light`和`Dark`属性标记为过时属性，仍然可用，但后续建议使用 `Theme` 属性。
  ```diff
  - <MButton Dark></MButton>
  + <MButton Theme="dark"></MButton>
  ```
- CSS 为了适配 **Material Design 3** 的颜色规则系统进行了微调，可能出现前后版本颜色不一致的情况。
  如有遇到问题请联系我们。

#### Icon fonts {#v1-10-0-icon-fonts}

删除了自定义图标集中标记为过时的 `Custom` 属性，请改为使用 `CssFormatter` 属性。

```diff
  var iconfontAliases = new MaterialDesignIconsAliases()
  {
-     Custom = icon => icon.StartsWith("mdi") ? $"mdi {icon}" : $"iconfont {icon}";
+     CssFormatter = icon => icon.StartsWith("mdi") ? $"mdi {icon}" : $"iconfont {icon}"
  };
```

### 组件 {#v1-10-0-components}

#### 移动端组件 {#mobile-components}

以下组件已移至单独的项目。如果你正在使用这些组件，则需要导入新包。

```cli
dotnet add package Masa.Blazor.MobileComponents
```

- [PMobileCascader](/blazor/mobiles/mobile-cascader)
- [PMobileDatePicker](/blazor/mobiles/mobile-date-pickers)
- [PMobileDateTimePicker](/blazor/mobiles/mobile-date-time-pickers)
- [PMobilePicker](/blazor/mobiles/mobile-pickers)
- [MMobilePickerView](/blazor/mobiles/mobile-picker-views)
- [PMobileTimePicker](/blazor/mobiles/mobile-time-pickers)
- [MPdfMobileViewer](/blazor/mobiles/pdf-mobile-viewer)
- [MPullRefresh](/blazor/mobiles/pull-refresh)
- [PPageStack](/blazor/mobiles/page-stack)
  - 需要在注入 MasaBlazor 服务时添加 `AddMobileComponents` 扩展方法。
    ```diff
      builder.Services
          .AddMasaBlazor()
    +     .AddMobileComponents();
     ```
  - **PStackPageBarInit** 组件已废弃，使用 **PPageStackBar** 代替。不再需要通过设置 `RerenderKey` 强制重新渲染。
  - 最新的推荐用法请参考[示例源码](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Blazor.Docs/Shared/PageStackLayout.razor)。

#### Swiper

此组件已移至单独的项目。如果你正在使用此组件，则需要导入新包。

```cli
dotnet add package Masa.Blazor.JSComponents.Swiper
```

#### Gridstack

此组件已移至单独的项目。如果你正在使用此组件，则需要导入新包。

```cli
dotnet add package Masa.Blazor.JSComponents.Gridstack
```

#### MarkdownIt and SyntaxHighlight

此组件已移至单独的项目。如果你正在使用此组件，则需要导入新包。

```cli
dotnet add package Masa.Blazor.JSComponents.MarkdownIt
```

#### Xgplayer

此组件已移至单独的项目。如果你正在使用此组件，则需要导入新包。

```cli
dotnet add package Masa.Blazor.JSComponents.Xgplayer
```

## 从 v1.8.x 升级到 v1.9.0 {#upgrading-from-v1-8-x-to-v1-9-0}

### 组件 {#v1-9-0-components}

#### Cascader {#v1-9-0-cascader}

添加了新的泛型参数 `TItemValue`。如果你拆分了 `@bind-Value`，你需要传递一个额外的类型为 `TItemValue` 的参数。

```diff
  <MCascader Value="@value"
             ValueChanged="@ValueChanged"
             TItem="AbcItem"
             TValue="string"
+            TItemValue="string"
             ... />
```

#### PdfMobileViewer {#v1-9-0-pdf-mobile-viewer}

此组件已移至单独的项目。 如果你正在使用此组件，则需要导入新包。

```cli
dotnet add package MASA.Blazor.JSComponents.PdfJS
```

#### PageStack {#v1-9-0-page-stack}

删除了 `TabbedPatterns` 和 `SelfPatterns` 属性，使用 `TabRules` 代替。

```diff
  <PPageStack
-     TabbedPatterns="_tabbedPatterns"
-     SelfPatterns="_selfPatterns"
+     TabRules="_tabRules" />

      @code {
-         private string[] _tabbedPatterns =
-         [
-             "/blazor/examples/page-stack/tab1",
-             "/blazor/examples/page-stack/tab2",
-             "/blazor/examples/page-stack/tab3"
-         ];

-         private string[] _selfPatterns =
-         [
-             "/blazor/examples/page-stack/tab2",
-         ];
      
+         private readonly HashSet<TabRule> _tabbedPatterns =
+         [
+             new TabRule("/blazor/examples/page-stack/tab1"),
+             new TabRule("/blazor/examples/page-stack/tab2", Self: true),
+             new TabRule("/blazor/examples/page-stack/tab3"),
+         ];
      }
```

## 从 v1.7.x 升级到 v1.8.0 {#upgrading-from-v1-7-x-to-v1-8-0}

### 组件 {#v1-8-0-components}

#### Pagination {#v1-8-0-pagination}

应用阴影的方式从 `box-shadow`样式已经改为使用 `Elevation` 参数，如果你使用了自定义样式，请注意修改为使用 `elevation` 样式。

```diff
- <MPagination Class="css-to-hide-shadow" />
+ <MPagination Elevation="0" />
```

## 从 v1.6.x 升级到 v1.7.0 {#upgrading-from-v1-6-x-to-v1-7-0}

### 组件 {#v1-7-0-components}

#### Pagination {#v1-7-0-pagination}

新增了一个迷你样式的UI，现在当浏览器窗口小于 *600px* 时，会自动使用。如果不想使用迷你样式，可以通过 `MiniVariant` 属性手动设置。

```diff
  <MPagination @bind-Value="page"
               Length="10"
+              MiniVariant="false"              
  ></MPagination>
```

#### Form {#v1-7-0-form}

DataAnnotations 验证现在内置支持复杂类型，不需要引用额外的类库和代码。

```diff .csproj
- <PackageReference Include="Microsoft.AspNetCore.Components.DataAnnotations.Validation" Version="3.2.0-rc1.20223.4" />
```

```diff .razor
  <MForm>
-     <ObjectGraphDataAnnotationsValidator />
      @foreach (var person in _order.Persons)
      {
          <MTextField @bind-Value="person.Name" Label="Name"></MTextField>
      }
  </MForm>

  @code {
    public class Order
    {
-       [ValidateComplexType]
        public List<Person> Persons { get; set; }
    }

    public class Person
    {
        [Required]
        public string Name { get; set; }
    }

    private Order _order = new() { Persons = [] };
  }
```

#### Treeview {#v1-7-0-treeview}

现在应用 `Selectable` 属性后，可以通过单击行来选择。要禁用此功能，需要将 `SelectOnRowClick` 设置为 `false`。

```diff
  <MTreeview @bind-Value="_selected"
+            SelectOnRowClick="false"
             Selectable="true">
  </MTreeview>
```

## 从 v1.5.x 升级到 v1.6.0

### 更改脚本

```diff
- <script src="_content/BlazorComponent/js/blazor-component.js"></script>
+ <script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```

### 删除所有有关 BlazorComponent 的引用

```diff _Imports.razor
- @using BlazorComponent
- @using BlazorComponent.I18n
```

```diff _Imports.cs
- global using BlazorComponent;
- global using BlazorComponent.I18n;
```

### 组件

#### DragZone

该组件在 v1.4.0 中已弃用，现在已删除。建议使用 [MSortable](/blazor/components/sortable) 组件。

#### Data/DataTable

- 对于服务器端分页和排序必须提供 `ServerItemsLength` 参数，即服务器端数据的总长度。
- `Locale` 参数一直没有实现，而且没有实现的必要，现已删除。

## 从 v1.4.0 升级到 v1.5.0

### 组件

#### PageStack

该组件在 v1.4.0 中引入用于管理页面堆栈。在 v1.5.0 中，我们对其进行了重构，并引入了 `PageStackNavController` 服务来解决一些潜在问题并提供更完整的功能。

#### Icon

删除了不必要的 `IsActive` 参数。

#### Border

使用 CSS 方案重构。

- `Round` and `WrapperStyle` 参数已删除。
- `Color` 参数现在只支持内置的 _primary_, _secondary_, _accent_, _surface_, _success_, _error_, _warning_, _info_ 和标准 CSS 颜色值。
  ```diff 
  - <MBorder Color="pink"></Border>
  + <MBorder Color="#e91e63"></Border>
  ```

## 从 v1.2.x 升级到 v1.4.0

### 组件

#### DragZone

该组件已弃用但未删除，建议使用 [MSortable](/blazor/components/sortable) 组件。

#### InfiniteScroll

`Parent`参数现在不再支持将 **ElementReference** 类型的值，请使用 CSS 选择器字符串。改动原因请移步 [Github](https://github.com/masastack/MASA.Blazor/issues/1820#issuecomment-2041300810)。

#### PageContainer

内部不再使用 **Windows** 组件作为切换的容器，因此有使用内部 CSS 类名的自定义样式可能会失效。

### CSS

`m-application--wrap` 类名改为 `m-application__wrap`。

## 从 v1.1.x 升级到 v1.2.0

### 组件

#### DataTable

- `OnItemSelect` 的类型从 `Action<TItem, bool>` 改为了 `EventCallback<(TItem Item, bool Selected)>`。

- 删除了 `FixedRight` 属性，现在可以通过Headers里的 `Fixed` 属性来设置列的固定位置。

## 从 v1.0.x 升级到 v1.1.0

### 组件

#### Cascader

`Outlined` 属性的默认值改为了 `false`，与其他表单输入组件默认样式一致。

#### Form(FluentValidation)

移除了内置的自动注册 **FluentValidation** 验证器的功能，现在需要手动注册，详情请参考 [Automatic registration](https://docs.fluentvalidation.net/en/latest/di.html#automatic-registration)。

#### ImageCaptcha

为了减少 WebAssembly 加载的体积，将 ImageCaptcha 组件移动到单独的 nuget 包：[Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia)。

## 从 v0.6.x 升级到 v1.0.0

v1.0.0 包含了不兼容的破坏性更改，包括以下变更：

### 特性

#### I18n

移除了通过`$DefaultCulture`设置默认语言的方式。现在在 _Program.cs_ 中通过 `AddMasaBlazor` 的 `Locale` 选项设置默认语言。

```csharp
services.AddMasaBlazor(options =>
{
    options.Locale = new Locale("zh-CN", "en-US");
});
```

#### 双向性(LTR/RTL)

删除了通过参数设置某些组件默认 RTL 的方式。现在在 _Program.cs_ 中通过 `AddMasaBlazor` 的 `RTL` 选项设置默认 RTL。

- MApp (移除了 `LeftToRight` 参数)
- MBadge (移除了 `Right` 参数)

### 服务

#### MasaBlazor

`MasaBlazor.Breakpoint.OnUpdate` 事件的类型改为了 `EventHandler<BreakpointChangedEventArgs>`。

```diff
  @inject MasaBlazor MasaBlazor

  MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;

- private void BreakpointOnOnUpdate()
+ private void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
  {
  }
```

### 组件

#### MBreadcrumbs

`Linkage` 属性重命名为 `Routable`。

```diff
- <MBreadcrumbs Linkage></MBreadcrumbs>
+ <MBreadcrumbs Routable></MBreadcrumbs>
```

#### MButton

`StopPropgation` 属性重命名 `OnClickStopPropagation`。

```diff
- <MButton StopPropagation></MButton>
+ <MButton OnClickStopPropagation></MButton>
```

#### MCheckbox/MSwitch

`Value` 类型不再是 `bool` 类型，而是泛型 `TValue`。对于没有使用 `@bind-Value` 的写法需要显式指定泛型类型。

```diff
- <MCheckbox Value="" ValueChanged=""></MCheckbox>
+ <MCheckbox Value="" ValueChanged="" TValue="bool"></MCheckbox>
```

#### MDataTable

- DataTableHeader 的 `Align` 类型从 `string` 变为枚举。

  ```diff
  - Align = "start"
  + Align = DataTableHeaderAlign.Start
  ```

- `ItemClass`的类型从 `string` 改为了 `Func<TItem, string>`。

#### MErrorHandler

移除了 `ShowAlert` 参数，使用 `PopupType` 代替；移除了 `OnErrorHandleAsync` 参数，请使用 `OnHandle` 代替。`OnHandle` 会覆盖默认的错误处理程序，如果你只想在处理错误之后做其他操作，请使用 `OnAfterHandle`。

```diff
- <MErrorHandler ShowAlert="false" OnErrorHandleAsync=""></MErrorHandler>
+ <MErrorHandler PopupType="ErrorPopupType.None" OnHandle="" OnAfterHandle=""></MErrorHandler>
```

#### MHover

移除了 `Context` 的 `Class` 和 `Style` 属性。

#### MIcon

引入了默认图标集的概念，现在如果不是默认图标集的图标需要指定图标集前缀。

```diff
- <MIcon>home</MIcon>
+ <MIcon>md:home</MIcon>
- <MIcon>fas fa-home</MIcon>
+ <MIcon>fa:fas fa-home</MIcon>
```

#### MInfiniteScroll

现在不需要通过额外的 `HasMore` 参数来设置加载的状态，而是通过 `OnLoad` 事件参数的 `Status` 来控制，一步到位。另外，组件在第一次呈现时会自动触发 `OnLoad` 事件。

```diff
 <MInfiniteScroll
-    HasMore="hasMore"
-    OnLoadMore="OnLoad"
+    OnLoad="OnLoad"
 >
 </MInfiniteScroll>
 @code {
-    private bool hasMore;
-    private async Task OnLoad() {
-        var items = await Request();
-        hasMore = items.Count > 0;
-    }
+    private async Task OnLoad(InfiniteScrollLoadEventArgs args) {
+        var items = await Request();
+        args.Status = items.Count > 0 ? InfiniteScrollStatus.HasMore : InfiniteScrollStatus.NoMore;
+    }
 }
```

#### MList

`Linkage` 属性重命名为 `Routable`。

```diff
- <MList Linkage></MList>
+ <MList Routable></MList>
```

#### MNavigationDrawer

`Value` 的类型改为 `bool?`，`ValueChanged` 的类型改为 `EventCallback<bool?>`。如果你没有特别的需求，建议将 `Value` 值设置 `null`。当 `Value` 值为 `null` 时，组件会根据屏幕宽度自动判断是否显示抽屉。

#### MOverlay

一个新的属性 `Contained` 用来代替之前的 `Absolute`。

```diff
- <MOverlay Absolute></MOverlay>
+ <MOverlay Contained></MOverlay>
```

#### MPageTabs

重构为预置组件**PPageTabs**，设计和API改动较大，具体请参考[文档](/blazor/components/page-tabs)。

#### PopupService

移除了 `AlertAsync` 和 `ToastAsync`，请使用 `EnqueueSnackbarAsync` 代替。

```diff
- PopupService.AlertAsync()
- PopupService.ToastAsync()
+ PopupService.EnqueueSnackbarAsync()
```

#### PConfirm

移除了该组件，请使用 `IPopupService` 服务的 `ConfirmAsync`方法。

#### PToasts

移除了该组件，请使用 **PEnqueuedSnackbars** 组件或 `IPopupService` 服务的 `EnqueueSnackbarAsync` 方法。

## 从 v0.5.x 升级到 v0.6.x

v0.6.x 包含了不兼容的破坏性更改，包括以下变更：

### 主题

```diff
 services.AddMasaBlazor(options =>
 {
-    options.DarkTheme = true;
-    options.UseTheme(theme =>
-    {
-        theme.Primary = "XXX";
-    });
+    options.ConfigureTheme(theme =>
+    {
+        theme.Dark = true;
+        theme.Themes.Light.Primary= "XXX";
+        theme.Themes.Dark.Primary= "XXX"; // support for configure the preset of Dark theme
+    });
 })
```

### 组件

- **Form**：`Validate`，`Reset` 和 `ResetValidation` 更改为同步方法。将 `ChildContent` 的上下文类型从 `EditContext` 改为 `FormContext`。

## 从 v0.4.x 升级到 v0.5.x

v0.5.x 包含了不兼容的破坏性更改，包括以下变更：

### CSS

引入 `masa-blazor.css` 和 `masa-extend-blazor.css` 更改为引入 `masa-blazor.min.css`。

### I18n

```diff
- services.AddMasaBlazor();
- services.AddMasaI18nForServer();
- services.AddMasaI18nForWasmAsync(); // in WASM
+ services.AddMasaBlazor().AddI18nForServer();
+ await services.AddMasaBlazor().AddI18nForWasmAsync(); // in WASM
```

同时把 **languageConfig.json** 文件重命名为 **supportedCultures.json**。
