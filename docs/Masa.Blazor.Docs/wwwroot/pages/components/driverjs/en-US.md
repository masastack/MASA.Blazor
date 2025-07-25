---
title: Tour
desc: "A tour component based on [driver.js](https://github.com/kamranahmedse/driver.js)."
tag: "JS Wrapper"
release: v1.9.0
---

```shell {#install-cli}
dotnet add package Masa.Blazor.JSComponents.DriverJS
```

```html {#install-style}

<link href="_content/Masa.Blazor.JSComponents.DriverJS/driver.css" rel="stylesheet"/>
```

## Usage

<masa-example file="Examples.components.driverjs.Usage"></masa-example>

## Examples

### Props

#### No Animation

<masa-example file="Examples.components.driverjs.NoAnimation"></masa-example>

#### Style

<masa-example file="Examples.components.driverjs.Style"></masa-example>

#### Highlight

<masa-example file="Examples.components.driverjs.Highlight"></masa-example>

#### No Element

<masa-example file="Examples.components.driverjs.NoElement"></masa-example>

#### Click Overlay {released-on=v1.10.0}

By default, clicking the overlay will close the tour. Setting `OverlayClickBehavior` to `OverlayClickBehavior.NextStep`
will advance to the next step when the overlay is clicked.

<masa-example file="Examples.components.driverjs.ClickOverlay"></masa-example>

