---
title: Gridstack
desc: "基于 [gridstack(v12.2.2)](https://github.com/gridstack/gridstack.js) 封装。"
tag: 基于JS封装
related:
  - /blazor/components/echarts
  - /blazor/components/sheets
  - /blazor/components/cards
---

## 安装 {#installation released-on=v1.10.0}

```bash
dotnet add package Masa.Blazor.JSComponents.Gridstack
```

```html
<link href="https://cdn.masastack.com/npm/gridstack.js/12.2.1/gridstack.min.css" rel="stylesheet">
```

```html
<script src="https://cdn.masastack.com/npm/gridstack.js/12.2.1/gridstack-all.min.js"></script>
```

## 使用 {#usage}

<masa-example file="Examples.components.gridstack.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### SizeToContent {released-on="v1.10.0"}

<masa-example file="Examples.components.gridstack.SizeToContent"></masa-example>

#### 保存 {#save}

<masa-example file="Examples.components.gridstack.Save"></masa-example>

### 其他 {#misc}

#### ECharts {#echarts}

<masa-example file="Examples.components.gridstack.ECharts"></masa-example>
