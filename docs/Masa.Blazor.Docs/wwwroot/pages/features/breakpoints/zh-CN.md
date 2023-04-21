# 响应式显示

使用 MASA Blazor，您可以根据窗口大小控制应用程序的各个方面。 此功能与[网格系统](/blazor/components/grids)和其它辅助类 (如[显示辅助](/blazor/styles-and-animations/display-helpers)) 一起生效。

<breakpoint-table></breakpoint-table>

## 断点服务

断点服务是在组件中访问视口信息的编程方式。 它在 `MasaBlazor` 对象上公开了一些属性，这些属性可用于根据视口大小控制应用程序的各个方面。 `Name`属性与当前活动断点相关; 例如，_xs_，_sm_，_md_，_lg_，_xl_。

在下面的示例中，我们使用 switch 语句和当前断点名称来修改 **MCard** 组件的 `Height` 属性：

<masa-example file="Examples.features.breakpoints.Name"></masa-example>

## 使用

让我们尝试一个真实的例子，使用 `MDialg` 组件，您希望在移动设备上将其转换为全屏对话框。要跟踪这一点，我们需要确定屏幕的大小相对于我们要比较的值。 我们需要将 `IJSRuntime` 服务注入到我们的组件中，并使用 `InvokeVoidAsync` 方法将侦听器添加到 `window` 对象和 `resize` 事件。这太复杂了。

相反，我们访问 `MasaBlazor.Breakpoint` 对象的 `Mobile` 属性。这将根据当前视口是否大于或小于 `MobileBreakpoint` 选项返回 `true` 或 `false` 值。

<masa-example file="Examples.features.breakpoints.Dialog"></masa-example>

### 断点服务对象

`MasaBlazor.Breakpoint` 对象上可用的属性如下：

```csharp
public class Breakpoint 
{
    // 断点
    public bool Xs { get; }
    public bool Sm { get; }
    public bool Md { get; }
    public bool Lg { get; }
    public bool Xl { get; }

    // 条件
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

    // 当前断点(例如'Md') 
    public Breakpoints Name { get; }

    // 尺寸
    public double Height { get; }
    public double Width { get; }

    // 如果屏幕宽度 < MobileBreakpoint 时为 true
    public bool Mobile { get; }
    public OneOf<Breakpoints, double> MobileBreakpoint { get; set; }

    // 阈值
    // 可通过选项配置
    public BreakpointThresholds Thresholds { get; set; }

    // 滚动条宽度
    public double ScrollBarWidth { get; set; }

    // 大小发生变化时触发的事件
    public event Func<Task> OnUpdate;
}
```

### 断点条件

断点和条件值返回一个 `boolean`，该值是从当前视口大小获取的。此外，断点服务模仿了 [Grids](/blazor/components/grids) 命名约定，并且可以访问诸如 `XlOnly`，`XsOnly`，`MdAndDown` 等属性。 在以下示例中，我们在额外小的断点上将 **MSheet** 的最小高度更改为 300，并且仅在额外小的屏幕上显示圆角：

```razor
@inject MasaBlazor MasaBlazor

<MSheet MinHeight="@(MasaBlazor.Breakpoint.Xs ? 300 : "20vh")"
        Rounded="@(MasaBlazor.Breakpoint.XsOnly)">
    ...
</MSheet>
```

### Mobile断点

`MobileBreakpoint` 选项接受断点名称 (_xs_, _sm_, _md_, _lg_, _xl_) 作为有效的配置选项。 一旦设置，提供的值将传播到支持的组件，例如 [MNavigationDrawer](/blazor/components/navigation-drawers)。

```csharp Program.cs
builder.services.AddMasaBlazor(options =>
{
    options.ConfigureBreakpoint(breakpoint =>
    {
        breakpoint.MobileBreakpoint = Breakpoints.Sm; // This is equivalent to a vlaue of 960
    });
});
```

### 阈值

`Thresholds` 选项修改用于视口计算的值。 以下代码段覆盖了从 xs 到 lg 的断点，并将 `ScrollBarWidth` 增加到 24。

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

你可能会注意到断点服务上没有 `Xl` 属性，这是有意为之的。 视口计算始终从 0 开始，并逐渐向上。 `Xs` 阈值的值为 340 意味着窗口大小为 0 到 340 的视口被视为 _额外小_ 屏幕。

[//]: # (TODO: how to update css helper classes?)
