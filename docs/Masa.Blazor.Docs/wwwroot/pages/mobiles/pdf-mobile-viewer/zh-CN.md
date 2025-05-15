---
title: PDF Mobile Viewer（PDF 移动端查看器）
desc: "专为移动设备设计的 PDF 查看器。"
tag: "JS代理"
---

## 安装 {#installation released-on=v1.9.0}

```shell
dotnet add package Masa.Blazor.JSComponents.PdfJS
```

``` html
<link href="https://cdn.masastack.com/npm/pdfjs-dist/4.5.136/web/pdf_viewer.min.css" rel="stylesheet">
```

## 使用 {#usage}

<masa-example file="Examples.mobiles.pdf_mobile_viewer.Usage" no-actions="true"></masa-example>

```razor
<MPdfMobileViewer Url="_content/Masa.Blazor.Docs/img/compressed.tracemonkey-pldi-09.pdf" /> 
```