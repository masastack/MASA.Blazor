---
title: ECharts（图表）
desc: "[ECharts](https://echarts.apache.org/examples/zh/index.html)"
tag: js-proxy
---

## 使用

图表使用

<echarts-usage></echarts-usage>

<app-alerts type="info" content='使用前需要先引用ECharts的包：`<script src="https://cdn.masastack.com/npm/echarts/5.1.1/echarts.min.js"></script>`。'></app-alerts>

## 示例

### 属性

#### 暗色主题

使用 Dark 属性切换到暗色主题。

<masa-example file="Examples.components.echarts.Dark"></masa-example>

#### 高度和宽度

通过 `Height` , `Width`  属性设置宽高

<masa-example file="Examples.components.echarts.HeightAndWidth"></masa-example>

#### 本地化

指定使用的语言。详细使用请参考ECharts官方文档。

<masa-example file="Examples.components.echarts.Locale"></masa-example>

#### 主题

指定使用的主题，默认支持亮和暗主题。 你可以使用[自定义主题](https://echarts.apache.org/handbook/zh/concepts/style/#%E9%A2%9C%E8%89%B2%E4%B8%BB%E9%A2%98%EF%BC%88theme%EF%BC%89)，例如本示例，在HTML引入vintage.js文件后可以设置**vintage**主题。

<masa-example file="Examples.components.echarts.Locale"></masa-example>


