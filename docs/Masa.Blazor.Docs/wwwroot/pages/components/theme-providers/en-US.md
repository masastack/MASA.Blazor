---
title: Theme providers
desc: The theme provider allows you to style a section of your application in a different theme from the default
related:
  - /blazor/features/theme
---

## Examples

### Props

#### Background

By default, **MThemeProvider** is a non-rendering component and allows you to modify the theme of all its child components. 
When using the `WithBackground` property, **MThemeProvider** wraps its child elements in an element and replaces its own background color with the theme's background color.

<masa-example file="Examples.components.theme_providers.Background"></masa-example>