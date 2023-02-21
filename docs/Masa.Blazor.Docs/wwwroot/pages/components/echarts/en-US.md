---
title: ECharts
desc: "[ECharts](https://echarts.apache.org/examples/en/index.html)"
tag: "JsProxy"
---

## Usage

<masa-example file="Examples.components.echarts.Usage"></masa-example>

<app-alert type="info" content='You need to reference the package of ECharts before using it: `<script src="https://cdn.masastack.com/npm/echarts/5.1.1/echarts.min.js"></script>`.'></app-alert>

## Examples

### Props

#### Dark

Use `Dark` prop to switch to the dark theme.

<masa-example file="Examples.components.echarts.Dark"></masa-example>

#### HeightAndWidth

`Height` and `Width` property set chart height,width

<masa-example file="Examples.components.echarts.HeightAndWidth"></masa-example>

#### Locale

Specify the locale. For more information, please refer to ECharts official documentation.

<masa-example file="Examples.components.echarts.Locale"></masa-example>

#### Theme

Specify the theme. Light and dark themes are supported by default. You can use [custom themes](https://echarts.apache.org/handbook/en/concepts/style/#theme). In this example, the **vintage** theme can be set after importing the vintage.js file in HTML.

<masa-example file="Examples.components.echarts.Theme"></masa-example>

### Misc

#### Live update

<masa-example file="Examples.components.echarts.LiveUpdate"></masa-example>
