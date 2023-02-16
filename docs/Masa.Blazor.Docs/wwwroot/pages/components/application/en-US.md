---
title: Application
desc: "In MASA Blazor, the MApp component and the app prop on components like **MNavigationDrawer**, **MAppBar**, **MFooter** and more, help bootstrap your application with the proper sizing around <MMain> component. This allows you to create truly unique interfaces without the hassle of managing your layout sizing. The **MApp** component is REQUIRED for all applications. This is the mount point for many of MASA Blazor's components and functionality and ensures that it propagates the default application variant (dark/light) to children components and also ensures proper cross-browser support for certain click events in browsers like Safari. **MApp** should only be rendered within your application ONCE."
related:
  - /blazor/features/theme
  - /blazor/components/app-bars
  - /blazor/components/navigation-drawers
---

<app-alert type="error" content="In order for your application to work properly, you must wrap it in a **MApp** component. This component is required for ensuring 
proper cross-browser compatibility. MASA Blazor doesn't support multiple isolated MASA Blazor instances on a page. **MApp** can exist 
anywhere inside the body of your app, however, there should only be one and it must be the parent of ALL MASA Blazor components.">
</app-alert>

<app-alert type="info" content="If you are using multiple layouts in your application you will need to ensure each root layout file that will contain MASA Blazor 
components has a MApp at the root of its template."></app-alert>

## Default application markup

This is an example of the default application markup for MASA Blazor. You can place your layout elements anywhere, 
as long as you apply the app property. The key component to making your page content work together with layout elements 
is **MMain**. The **MMain** component will be dynamically sized depending upon the structure of your designated app components. 
You can use combinations of any or all of the above components including **MBottomNavigation**.

```cshtml
<!-- MainLayout.razor -->
@inherits LayoutComponentBase

<MApp>
  <MNavigationDrawer App>
    <!-- -->
  </MNavigationDrawer>

  <MAppBar App>
    <!-- -->
  </MAppBar>

  <!-- Sizes your content based upon application components -->
  <MMain>
    <!-- Provides the application the proper gutter -->
    <MContainer Fluid>
        @Body
    </MContainer>
  </MMain>

  <MFooter App>
    <!-- -->
  </MFooter>
</MApp>

```

<app-alert type="info" content="Applying the `App` prop automatically applies `position:fixed` to the layout element. If your application calls for an absolute element, 
you can overwrite this functionality by using the `Absolute` prop."></app-alert>

## Application components

Below is a list of all the components that support the `App` prop and can be used as layout elements in your application. 
These can be mixed and matched and only one of each particular component should exist at any time. You can, however, 
swap them out and the layout will accommodate. For some examples displaying how you can build various layouts, checkout the [Pre-made layouts](/blazor/getting-started/wireframes) page.

Each of these application components have a designated location and priority that it affects within the layout system.

- [MAppBar](/blazor/components/app-bars): Is always placed at the top of an application with a lower priority than **MSystemBar**.
- [MBottomNavigation](/blazor/components/bottom-navigation): Is always placed at the bottom of an application with a higher priority than **MFooter**.
- [MFooter](/blazor/components/footers): Is always placed at the bottom of an application with a lower priority than **MBottomNavigation** .
- [MNavigationDrawer](/blazor/components/navigation-drawers): Can be placed on the left or right side of an application and can be configured to sit next to or below **MAppBar** .
- [MSystemBar](/blazor/components/system-bars): Is always placed at the top of an application with higher priority than  **MAppBar** .

![app](http://cdn.masastack.com/stack/doc/blazor/layouts/app.png)

## Application service

The application service is used to configure your MASA Blazor layout. It communicates with the **MMain** component so that it's able to properly size the application content. 
It has a number of properties that can be accessed:

```csharp
double Bar { get; }
double Bottom { get; }
double Footer { get; }
double InsetFooter { get; }
double Left { get; }
double Right { get; }
double Top { get; }
```

These values are automatically updated when you add and remove components with the `App` prop. They are NOT editable and exist in a READONLY state. 
You can access these values by referencing the `Application` property.

```csharp
 [Inject] public MasaBlazor MasaBlazor { get; set; }
 
 Console.WriteLine(MasaBlazor.Application.Footer); // 60
```

<app-alert type="error" content="In order for your application to work properly, you must wrap it in a **MApp** component. "></app-alert>

## Application Theme

The theme of Masa Blazor can be customized, including various default colors...

Modify the code to add Masa.Blazor related services in Program.cs to set a default theme

```csharp
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureTheme(theme =>
    {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Secondary = "#5CBBF6";
        theme.Themes.Light.Accent = "#005CAF";
        theme.Themes.Light.UserDefined["Tertiary"] = "#e57373";
    });
})
```

### Dynamically modify the theme

You can toggle theme by using the **ToggleTheme** method of the **MasaBlazor** service

<masa-example file="Examples.components.application.DynamicallyModifyTheme"></masa-example>
