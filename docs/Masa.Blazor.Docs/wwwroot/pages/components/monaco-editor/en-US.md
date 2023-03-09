---
title: Monaco editor
desc: "The [Monaco editor](https://microsoft.github.io/monaco-editor/) is the code editor that powers [VS Code](https://github.com/microsoft/vscode)."
tag: "JsProxy"
---

## Usage

<masa-example file="Examples.components.monaco_editor.Usage"></masa-example>

Reference the script before you use it:

```html
<script>
    var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };
</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

The full list of options can be found here [monaco-editor](https://microsoft.github.io/monaco-editor/docs.html)。

## Examples

### Exposed Colors

<masa-example file="Examples.components.monaco_editor.ExposedColors"></masa-example>

### Hard Wrapping

<masa-example file="Examples.components.monaco_editor.HardWrapping"></masa-example>

### Editor Basic Options

<masa-example file="Examples.components.monaco_editor.EditorBasicOptions"></masa-example>
