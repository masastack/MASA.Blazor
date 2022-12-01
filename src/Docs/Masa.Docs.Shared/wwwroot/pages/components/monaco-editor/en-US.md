---
title: monaco-editor The editor
desc: "For the operation of some code or edit some text when providing syntax highlighting and syntax prompt, support for custom syntax prompt, support for dynamic grammar switching, let your edit box more advanced!"
tag: js-proxy
related:
  - /components/monaco-editor
---

## Usage

Multi-function editor to use

## css file

```html
<link rel="stylesheet" data-name="vs/editor/editor.main" href="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.css">
```

## js files (note the order of references)

```html
<script>var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

<masa-example file="Examples.components.monaco_editor.Index"></masa-example>

## Setting the Language

<masa-example file="Examples.components.monaco_editor.SwitchLanguage"></masa-example>