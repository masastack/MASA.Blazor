---
title: Syntax highlights
desc: "[prism](https://prismjs.com/) and [highlightjs](https://highlightjs.org/) are supported. You need to introduce the js and css files one of the them in `_Host.cshtml` or `index.html`."
tag: JS Proxy
related:
  - /blazor/components/markdown-parsers
---

## Usage

- Using prism with CDN, such as [jsdelivr](https://www.jsdelivr.com/)

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css">
<!-- use other theme, such as dracula -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/prism-themes@1.9.0/themes/prism-material-dark.min.css">

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/prism.min.js"></script>
<!-- and it's easy to individually load additional languages -->
<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-csharp.min.js"></script>
```

- Or using highlightjs with CDN, such as [jsdelivr](https://www.jsdelivr.com/)

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/highlight.js@11.7.0/styles/default.min.css">
<!-- use other theme, such as github -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/highlight.js@11.7.0/styles/github.min.css">

<script src="https://cdn.jsdelivr.net/gh/highlightjs/cdn-release@11.7.0/build/highlight.min.js"></script>
<!-- and it's easy to individually load additional languages -->
<script src="https://cdn.jsdelivr.net/gh/highlightjs/cdn-release@11.7.0/build/languages/csharp.min.js"></script>
```

<masa-example file="Examples.components.syntax_highlights.Usage"></masa-example>
