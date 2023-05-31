# 升级指南

## 从 v0.6.x 升级到 v1.0.x

v1.0.x 包含了不兼容的破坏性更改，包括以下变更：

### 特性

#### I18n

移除了通过`$DefaultCulture`设置默认语言的方式。现在在 _Program.cs_ 中通过 `AddMasaBlazor` 的 `Locale` 选项设置默认语言。

```csharp
services.AddMasaBlazor(options =>
{
    options.Locale = new Locale("zh-CN", "en-US");
});
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
