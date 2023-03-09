---
title: Monaco Editor
desc: "基于[monaco-editor](https://microsoft.github.io/monaco-editor/)封装"
tag: "JS代理"
---

## 使用

<masa-example file="Examples.components.monaco_editor.Usage"></masa-example>



在使用脚本之前，请参考:

```html
<script>
    var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };
</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

完整选项列表请移步 [monaco-editor](https://microsoft.github.io/monaco-editor/docs.html)。

## 示例

### 扩展颜色

<masa-example file="Examples.components.monaco_editor.ExposedColors"></masa-example>

### Hard Wrapping

<masa-example file="Examples.components.monaco_editor.HardWrapping"></masa-example>

### 编辑器基本选项

<masa-example file="Examples.components.monaco_editor.EditorBasicOptions"></masa-example>
