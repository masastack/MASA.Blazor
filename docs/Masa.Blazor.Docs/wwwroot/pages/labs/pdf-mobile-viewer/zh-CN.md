---
title: PDF Mobile Viewer（PDF 移动端查看器）
desc: "专为移动设备设计的 PDF 查看器。"
tag: "JS代理"
---

在使用之前，需要引入样式表：

``` html
<link href="https://cdn.masastack.com/npm/pdfjs-dist/4.5.136/web/pdf_viewer.min.css" rel="stylesheet">
```

## 使用

<masa-example file="Examples.labs.pdf_mobile_viewer.Usage" no-actions="true"></masa-example>

```razor
<MPdfMobileViewer Url="_content/Masa.Blazor.Docs/img/compressed.tracemonkey-pldi-09.pdf" /> 
```