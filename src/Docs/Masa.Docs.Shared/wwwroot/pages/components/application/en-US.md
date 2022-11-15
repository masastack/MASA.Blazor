---
category: Components
type: Application
title: Application
cols: 1
---

# Application

In MASA Blazor, the MApp component and the app prop on components like **MNavigationDrawer**, **MAppBar**, **MFooter** and more, 
help bootstrap your application with the proper sizing around <MMain> component. This allows you to create truly unique 
interfaces without the hassle of managing your layout sizing. The **MApp** component is REQUIRED for all applications. 
This is the mount point for many of MASA Blazor's components and functionality and ensures that it propagates the default 
application variant (dark/light) to children components and also ensures proper cross-browser support for certain click 
events in browsers like Safari. **MApp** should only be rendered within your application ONCE.

## API

- [MApp](/api/MApp)
- [MMain](/api/MMain)

<!--alert:error-->
In order for your application to work properly, you must wrap it in a **MApp** component. This component is required for ensuring 
proper cross-browser compatibility. MASA Blazor doesn't support multiple isolated MASA Blazor instances on a page. **MApp** can exist 
anywhere inside the body of your app, however, there should only be one and it must be the parent of ALL MASA Blazor components.
<!--/alert:error-->

<!--alert:info-->
If you are using multiple layouts in your application you will need to ensure each root layout file that will contain MASA Blazor 
components has a MApp at the root of its template.
<!--/alert:info-->

## Default application markup

This is an example of the default application markup for MASA Blazor. You can place your layout elements anywhere, 
as long as you apply the app property. The key component to making your page content work together with layout elements 
is **MMain**. The **MMain** component will be dynamically sized depending upon the structure of your designated app components. 
You can use combinations of any or all of the above components including **MBottomNavigation**.

```html
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

<!--alert:info-->
Applying the `App` prop automatically applies `position:fixed` to the layout element. If your application calls for an absolute element, 
you can overwrite this functionality by using the `Absolute` prop.
<!--/alert:info-->

## Application components

Below is a list of all the components that support the `App` prop and can be used as layout elements in your application. 
These can be mixed and matched and only one of each particular component should exist at any time. You can, however, 
swap them out and the layout will accommodate. 

Each of these application components have a designated location and priority that it affects within the layout system.

- [MAppBar](/components/app-bars): Is always placed at the top of an application with a lower priority than **MSystemBar**.
- MBottomNavigation: Is always placed at the bottom of an application with a higher priority than **MFooter**.
- [MFooter](/components/footers): Is always placed at the bottom of an application with a lower priority than **MBottomNavigation** .
- [MNavigationDrawer](/components/navigation-drawers): Can be placed on the left or right side of an application and can be configured to sit next to or below **MAppBar** .
- [MSystemBar](/components/system-bars): Is always placed at the top of an application with higher priority than  **MAppBar** .

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

<!--alert:error-->
In order for your application to work properly, you must wrap it in a **MApp** component. 
<!--/alert:error-->


