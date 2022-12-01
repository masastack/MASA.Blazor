---
title: monaco-editor编辑器
desc: "对于操作一些代码或者编辑一些文本的时候提供语法高亮和语法提示，支持自定义语法提示，支持语法动态切换，让你的编辑框更高级！"
tag: js-proxy
related:
  - /components/monaco-editor
---

## 使用

多功能编辑器使用

## css文件

```html
<link rel="stylesheet" data-name="vs/editor/editor.main" href="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.css">
```

## js文件 (注意引用顺序)

```html
<script>var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

<masa-example file="Examples.components.monaco_editor.Index"></masa-example>

## 设置语法

<masa-example file="Examples.components.monaco_editor.SwitchLanguage"></masa-example>