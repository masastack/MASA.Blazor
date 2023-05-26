---
title: ECharts
desc: "A proxy component for [ECharts](https://echarts.apache.org/examples/en/index.html)"
tag: "JS Proxy"
---

## Usage

You need to reference the package of ECharts before using it:

```html 
<script src="https://cdnjs.cloudflare.com/ajax/libs/echarts/5.1.1/echarts.min.js"></script>
```

<masa-example file="Examples.components.echarts.Usage"></masa-example>

## Examples

### Props

#### Dark

Use `Dark` prop to switch to the dark theme.

<masa-example file="Examples.components.echarts.Dark"></masa-example>

#### HeightAndWidth

`Height` and `Width` property set chart height,width

<masa-example file="Examples.components.echarts.HeightAndWidth"></masa-example>

#### Use function in option

Using the function in `Option`, you need to enable the `IncludeFunctionsInOption` property. In the following example, the name of tooltip is set using **lambda**, and the label of the XAxis is set using **function**.

<masa-example file="Examples.components.echarts.IncludeFunctionsInOption"></masa-example>

#### Locale

Specify the locale. For more information, please refer to ECharts official documentation.

<masa-example file="Examples.components.echarts.Locale"></masa-example>

#### Theme

Specify the theme. Light and dark themes are supported by default. You can use [custom themes](https://echarts.apache.org/handbook/en/concepts/style/#theme). In this example, the **vintage** theme can be set after importing the vintage.js file in HTML.

<masa-example file="Examples.components.echarts.Theme"></masa-example>

### Misc

#### Live update

<masa-example file="Examples.components.echarts.LiveUpdate"></masa-example>
