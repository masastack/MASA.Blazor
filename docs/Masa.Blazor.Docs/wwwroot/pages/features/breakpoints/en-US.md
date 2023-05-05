# Display Breakpoints

With MASA Blazor, you can control all aspects of the application based on the window size. This function works with the [Grid System]((/blazor/components/grids)) and other responsive helper classes(e.g. [display](/blazor/components/grids))

<breakpoint-table></breakpoint-table>

## Breakpoint service

The **breakpoint service** is a programmatic way of accessing viewport information within components. It exposes a number of properties on the `MasaBlazor` object that can be used to control aspects of your application based upon the viewport size. The `Name` property correlates to the currently active breakpoint; e.g. _xs_, _sm_, _md_, _lg_, _xl_.

In the following example, we use a switch statement and the current breakpoint name to modify the `Height` property of the **MCard** component:

<masa-example file="Examples.features.breakpoints.Name"></masa-example>

## Usage

Let’s try a real world example with a `MDialg` component that you want to convert to a fullscreen dialog on mobile devices. To track this we would need to determine the size of the screen relative to the value we are comparing to. We need inject the `IJSRuntime` service into our component and use the `InvokeVoidAsync` method to add a listener to the `window` object and the `resize` event. This is so complex.

Instead, let's access the `Mobile` property of the `MasaBlazor.Breakpoint` object. This will return boolean value of `true` or `false` depending upon if the current viewport is larger or smaller than the `MobileBreakpoint` option.

<masa-example file="Examples.features.breakpoints.Dialog"></masa-example>

### Breakpoint service object

The following properties are available on the `MasaBlazor.Breakpoint` object:

```csharp
public class Breakpoint 
{
    // Breakpoints
    public bool Xs { get; }
    public bool Sm { get; }
    public bool Md { get; }
    public bool Lg { get; }
    public bool Xl { get; }

    // Conditionals
    public bool XsOnly { get; }
    public bool SmOnly { get; }
    public bool SmAndDown { get; }
    public bool SmAndUp { get; }
    public bool MdOnly { get; }
    public bool MdAndDown { get; }
    public bool MdAndUp { get; }
    public bool LgOnly { get; }
    public bool LgAndDown { get; }
    public bool LgAndUp { get; }
    public bool XlOnly { get; }

    // Current breakpoint name (e.g. 'md') 
    public Breakpoints Name { get; }

    // Dimensions
    public double Height { get; }
    public double Width { get; }

    // true if screen with < mobileBreakpoint
    public bool Mobile { get; }
    public OneOf<Breakpoints, double> MobileBreakpoint { get; set; }

    // Thresholds
    // Configurable through options
    public BreakpointThresholds Thresholds { get; set; }

    // Scrollbar
    public double ScrollBarWidth { get; set; }

    // Call if resize
    public event Func<Task> OnUpdate;
}
```

### Breakpoint conditionals

The breakpoint and conditional values return a `boolean` that is derived from the current viewport size. Additionally, the breakpoint service mimics the [Grids](/blazor/components/grids) naming conventions and has access to properties such as `XlOnly`, `XsOnly`, `MdAndDown`, and others. In the following example we change the minimum height of **MSheet** to 300 when on the extra small breakpoint and only show rounded corners on extra small screens:

```razor
@inject MasaBlazor MasaBlazor

<MSheet MinHeight="@(MasaBlazor.Breakpoint.Xs ? 300 : "20vh")"
        Rounded="@(MasaBlazor.Breakpoint.XsOnly)">
    ...
</MSheet>
```

### Mobile breakpoints

The `MobileBreakpoint` option accepts breakpoint names (_xs_, _sm_, _md_, _lg_, _xl_) as a valid configuration option. Once set, the provided value is propagated to supporting components such as [MNavigationDrawer](/blazor/components/navigation-drawers).

```csharp Program.cs
builder.services.AddMasaBlazor(options =>
{
    options.ConfigureBreakpoint(breakpoint =>
    {
        breakpoint.MobileBreakpoint = Breakpoints.Sm; // This is equivalent to a vlaue of 960
    });
});
```

### Thresholds

The `Thresholds` option modifies the values used for viewport calculations. The following snippet overrides xs through lg breakpoints and increases `ScrollBarWidth` to 24.

```csharp Program.cs
builder.services.AddMasaBlazor(options =>
{
    options.ConfigureBreakpoint(breakpoint =>
    {
        breakpoint.Thresholds = new BreakpointThresholds
        {
            Xs = 340,
            Sm = 540,
            Md = 800,
            Lg = 1280,
        };
        breakpoint.ScrollBarWidth = 24;
    });
});
```

You may notice that there is no `Xl` property on the breakpoint service, this is intentional. Viewport calculations always start at 0 and work their way up. A value of 340 for the `Xs` threshold means that a window size of 0 to 340 is considered to be an _extra small_ screen.

[//]: # (TODO: how to update css helper classes?)
