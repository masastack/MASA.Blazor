---
title: PDF Mobile Viewer
desc: "A PDF viewer designed for mobile."
tag: "JS Proxy"
---

```shell
dotnet add package Masa.Blazor.JSComponents.PdfJS
```

``` html
<link href="https://cdn.jsdelivr.net/npm/pdfjs-dist@4.5.136/web/pdf_viewer.min.css" rel="stylesheet">
```

## Usage

<masa-example file="Examples.labs.pdf_mobile_viewer.Usage" no-actions="true"></masa-example>

```razor
<MPdfMobileViewer Url="_content/Masa.Blazor.Docs/img/compressed.tracemonkey-pldi-09.pdf" /> 
```