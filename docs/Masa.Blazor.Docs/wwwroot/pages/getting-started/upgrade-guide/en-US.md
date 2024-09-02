# Upgrade Guides

## Upgrading form v1.6.x to v1.7.0

### Components {#v1-7-0-components}

#### Pagination {#v1-7-0-pagination}

A mini style UI has been added, now when the browser window is less than *600px*, it will automatically use it. If you don't want to use the mini style, you can manually set it through the `MiniVariant` property.

```diff
  <MPagination @bind-Value="page"
               Length="10"
+              MiniVariant="false"
  ></MPagination>
```

#### Form {#v1-7-0-form}

DataAnnotations validation now natively supports complex types, no need to reference additional libraries and code.

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

After enabling the `Selectable` property, you can now select by clicking on the row. To disable this functionality, you need to set `SelectOnRowClick` to `false`.

```diff
  <MTreeview @bind-Value="_selected"
+            SelectOnRowClick="false"
             Selectable="true">
  </MTreeview>
```

## Upgrading form v1.5.x to v1.6.0

### Change the script

```diff
- <script src="_content/BlazorComponent/js/blazor-component.js"></script>
+ <script src="_content/Masa.Blazor/js/masa-blazor.js"></script>
```

### Remove all references to BlazorComponent

```diff _Imports.razor
- @using BlazorComponent
- @using BlazorComponent.I18n
```

```diff _Imports.cs
- global using BlazorComponent;
- global using BlazorComponent.I18n;
```

### Components

#### DragZone

The component was deprecated in v1.4.0 and has now been removed. It is recommended to use the [MSortable](/blazor/labs/sortable) component instead.

#### Data/DataTable

- For server-side pagination and sorting, the `ServerItemsLength` parameter must be provided, which is the total length of the server-side data.
- The `Locale` parameter has never been implemented, and there is no need to implement it, it has now been removed.

## Upgrading form v1.4.x to v1.5.0

### Components

#### PageStack

The component was introduced in v1.4.0 to manage the page stack. In v1.5.0, we refactored it and introduced the `PageStackNavController` service to solve some potential problems and provide more complete functionality.

#### Icon
 
Remove the unnecessary `IsActive` parameter.

#### Border

Refactor using CSS approach.

- The `Rounded` and `WrapperStyle` parameters are deleted.
- The `Color` parameter only supports built-in _primary_, _secondary_, _accent_, _surface_, _success_, _error_, _warning_, _info_ and standard CSS color values.
  ```diff 
  - <MBorder Color="pink"></Border>
  + <MBorder Color="#e91e63"></Border>
  ```

## Upgrading form v1.2.x to v1.4.0

### Components

#### DragZone

This component is deprecated but not deleted, it is recommended to use the [MSortable](/blazor/labs/sortable) component instead.

#### InfiniteScroll

The parameter `Parent` no longer supports **ElementReference** type values, please use CSS selector strings. For the reason of this change,
please refer to [GitHub](https://github.com/masastack/MASA.Blazor/issues/1820#issuecomment-2041300810).

#### PageContainer

No longer use the **Windows** component as the switching container internally, so custom styles that use internal CSS class names may fail.

### CSS

The class name `m-application--wrap` is changed to `m-application__wrap`.

## Upgrading form v1.1.x to v1.2.0

### Components

#### DataTable

- The type of `OnItemSelect` is changed from `Action<TItem, bool>` to `EventCallback<(TItem Item, bool Selected)>`.

- Removed the `FixedRight` property, now you can set the fixed position of the column through the `Fixed` property in the `Headers`.

## Upgrading form v1.0.x to v1.1.0

### Components

#### Cascader

The default value of the `Outlined` property is `false` now, consistent with the default style of other form input components.

#### Form(FluentValidation)

Removed the built-in automatic registration of **FluentValidation** validators, now you need to register manually, please refer to [Automatic registration](https://docs.fluentvalidation.net/en/latest/di.html#automatic-registration) for details.

#### ImageCaptcha

In order to reduce the size of WebAssembly loading, the ImageCaptcha component is moved to a separate nuget package: [Masa.Blazor.SomethingSkia](https://www.nuget.org/packages/Masa.Blazor.SomethingSkia).

## Upgrading from v0.6.x to v1.0.0

v1.0.0 contains non backwards compatible breaking changes, the following changes:

### Features

#### I18n

Removed the way of setting the default locale through `$DefaultCulture`. Instead, supply the `Locale` option when calling `AddMasaBlazor` in _Program.cs_.

```csharp
services.AddMasaBlazor(options =>
{
    options.Locale = new Locale("zh-CN", "en-US");
});
```

#### Bidirectionality (LTR/RTL)

Removed the way of setting the default RTL on some components through parameter. Instead, supply the `RTL` option when calling `AddMasaBlazor` in _Program.cs_.

- MApp (Removed `LeftToRight` parameter)
- MBadge (Removed `Right` parameter)

### Services

#### MasaBlazor

The type of the `MasaBlazor.Breakpoint.OnUpdate` event is changed to `EventHandler<BreakpointChangedEventArgs>`.

```diff
  @inject MasaBlazor MasaBlazor

  MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;

- private void BreakpointOnOnUpdate()
+ private void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
  {
  }
```

### Components

#### MBreadcrumbs

The `Linkage` is renamed to `Routable`.

```diff
- <MBreadcrumbs Linkage></MBreadcrumbs>
+ <MBreadcrumbs Routable></MBreadcrumbs>
```

#### MButton

The parameter `StopPropgation` is renamed to `OnClickStopPropagation`.

```diff
- <MButton StopPropagation></MButton>
+ <MButton OnClickStopPropagation></MButton>
 ```

#### MCheckbox/MSwitch

The `Value` is no longer a `bool`, but generic type `TValue`. For words that do not use `@bind-Value`, you need to specify the generic type explicitly.

```diff
- <MCheckbox Value="" ValueChanged=""></MCheckbox>
+ <MCheckbox Value="" ValueChanged="" TValue="bool"></MCheckbox>
```

#### MDataTable

- The `Align` type of DataTableHeader changes from `string` to enum.

  ```diff
  - Align = "start"
  + Align = DataTableHeaderAlign.Start
  ```

- The type of `ItemClass` was changed from `string` to `Func<TItem, string>`.

#### MErrorHandler

- The `ShowAlert` parameter is removed, uses `PopupType` instead
- The `OnErrorHandleAsync` parameter is removed, use `OnHandle` instead. `OnHandle` would overrides the default error handler. Use `OnAfterHandle` if you only want to do something else after handling an error.

```diff
- <MErrorHandler ShowAlert="false"></MErrorHandler>
+ <MErrorHandler PopupType="ErrorPopupType.None" OnHandle="" OnAfterHandle=""></MErrorHandler>
```

#### MHover

Removed the `Class` and `Style` properties of `Context`.

#### MIcon

Introduces the concept of the default icon set. Now if the icon is not the default icon set, you need to specify the prefix of icon set.

```diff
- <MIcon>home</MIcon>
+ <MIcon>md:home</MIcon>
- <MIcon>fas fa-home</MIcon>
+ <MIcon>fa:fas fa-home</MIcon>
```

#### MInfiniteScroll

Now no longer needs to set the loading state through the additional `HasMore` parameter, but through the `Status` of the `OnLoad` event parameter to control it. Also, the component will automatically trigger the `OnLoad` event when it is first rendered.

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

The `Linkage` is renamed to `Routable`.

```diff
- <MList Linkage></MList>
+ <MList Routable></MList>
```

#### MNavigationDrawer

The type of `Value` is changed to `bool?`, and the type of `ValueChanged` is changed to `EventCallback<bool?>`. If you don't have special requirements, it is recommended to set the `Value` value to `null`. When the `Value` value is `null`, the component will automatically determine whether to display the drawer according to the screen width.

#### MOverlay

A new parameter `Contained` is used to replace the previous parameter `Absolute`.

```diff
- <MOverlay Absolute></MOverlay>
+ <MOverlay Contained></MOverlay>
```

#### MPageTabs

Refactored to the preset component **PPageTabs**. There are many design and API changes, please refer to the [document](/blazor/components/page-tabs) for details.

#### PopupService

Removed `AlertAsync` and `ToastAsync`, use `EnqueueSnackbarAsync` instead.

```diff
- PopupService.AlertAsync()
- PopupService.ToastAsync()
+ PopupService.EnqueueSnackbarAsync()
```

#### PConfirm

The component has been removed, use the `IPopupService` service's `ConfirmAsync` method instead.

#### PToasts

The component has been removed, use the **PEnqueuedSnackbars** component or `IPopupService` service's `EnqueueSnackbarAsync` method instead.

## Upgrading from v0.5.x to v0.6.x

v0.6.x contains non backwards compatible breaking changes, the following changes:

### Themes

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

### Components

- **MForm**: `Validate`, `Reset` and `ResetValidation` are change to synchronous methods. Change the context type of `ChildContent` from `EditContext` to `FormContext`.

## Upgrading from v0.4.x to v0.5.x

v0.5.x contains non backwards compatible breaking changes, the following changes:

### CSS

Use `masa-blazor.min.css` instead of `masa-blazor.css` and `masa-extend-blazor.css`.

### I18n

```diff
- services.AddMasaBlazor();
- services.AddMasaI18nForServer();
- services.AddMasaI18nForWasmAsync(); // in WASM
+ services.AddMasaBlazor().AddI18nForServer();
+ await services.AddMasaBlazor().AddI18nForWasmAsync(); // in WASM
```

And rename the **languageConfig.json** file to **supportedCultures.json** at the same time.
