# Icon Fonts

MASA Blazor supports booting Material Design icons, Material icons, Font Awesome 4 and Font Awesome 5 By default, the application will use <a href="https://materialdesignicons.com" target="_blank">Material Design Icon</a> by default.

## Usage

You can use `MIcon`, `MAlert`, `MBadge` and other components to display icons. example:

```html
<MIcon>mdi-alarm</MIcon>

<MAlert Color="#2A3B4D" Icon="@("mdi-firework")">
    Hello World!
</MAlert>

<MBadge Color="error" Icon="mdi-lock"></MBadge>
```

If you want to change the font library or custom icons, this feature is not currently supported. Please be patient, we will continue to improve.