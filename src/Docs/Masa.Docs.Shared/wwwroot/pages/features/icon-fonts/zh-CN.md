# 图标字体

MASA Blazor 支持引导 Material Design 图标, Material 图标, Font Awesome 4 和 Font Awesome 5 默认情况下，应用程序将默认使用 <a href="https://materialdesignicons.com" target="_blank">Material Design 图标</a>。

## 使用

您可以利用 `MIcon`，`MAlert`，`MBadge`等组件展示图标。如下：

```html
<MIcon>mdi-alarm</MIcon>

<MAlert Color="#2A3B4D" Icon="@("mdi-firework")">
    Hello World!
</MAlert>

<MBadge Color="error" Icon="mdi-lock"></MBadge>
```

如果您想要更改字体库或自定义图标，该功能暂时未支持。请耐心等待，我们会不断完善。