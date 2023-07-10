---
title: Markdown解析器
desc: "基于 [markdown-it(v13.0.1)](https://github.com/markdown-it/markdown-it) 封装。"
tag: JS代理
related:
  - /blazor/components/syntax-highlights
  - /blazor/components/markdowns
  - /blazor/components/editors
---

本组件内置了以下第三方 markdown-it 的插件：
- [markdown-it-anchor](https://github.com/valeriangalliat/markdown-it-anchor)：通过 `AnchorOptions` 属性可以配置锚点的样式。
- [markdown-it-attrs](https://github.com/arve0/markdown-it-attrs)：使用`{.class #identifier attr=value attr2="spaced value"}`将类、标识符和属性添加markdown中。
- [markdown-it-container](https://github.com/markdown-it/markdown-it-container)：创建块级自定义容器。
- [markdown-it-front-matter](https://github.com/ParkSB/markdown-it-front-matter)：解析 Front Matter。
- [markdown-it-header-sections](https://github.com/arve0/markdown-it-header-sections)：将标题分组为节。需要开启 `HeaderSections` 属性。
- [markdown-it-todo](https://github.com/dexfire/markdown-it-todo): 将 `[ ]` 和 `[x]`程序为复选框。


> 有关如何使用其他插件，请参考 [使用第三方插件](#使用第三方插件) 的示例。

> 有关如何自定义规则和自定义容器，请参考本文档的[源码](https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Docs.Shared/wwwroot/js/markdown-parser.js)。

> 支持 **prism** 和 **highlightjs** 两种高亮方案，详情请查看 [MSyntaxHighlight](/blazor/components/syntax-highlights) 组件。

## 使用

<masa-example file="Examples.components.markdown_parsers.Usage"></masa-example>

## 示例

### 属性

#### 选项

完整选项列表请移步 [markdown-it](https://github.com/markdown-it/markdown-it#init-with-presets-and-options)。

<masa-example file="Examples.components.markdown_parsers.Options"></masa-example>

### 事件

#### OnFrontMatterParsed

Front Matter 是一种用于在 Markdown 文件中添加元数据的格式，它是 YAML 格式的一种扩展。使用 `OnFrontMatterParsed` 事件可以获取 Front Matter 的内容。

<masa-example file="Examples.components.markdown_parsers.OnFrontMatterParsed"></masa-example>

#### OnTocParsed

使用 `OnTocParsed` 事件可以获取 Markdown 文件的目录结构。

<masa-example file="Examples.components.markdown_parsers.OnTocParsed"></masa-example>

### 其他

#### 使用第三方插件

举个例子，如何使用 [markdown-it-emoji](https://github.com/markdown-it/markdown-it-emoji) 插件。使用 `window.MasaBlazor.extendMarkdownIt` 方法可以对 `markdown-it` 进行扩展。

``` html
<script src="https://cdnjs.cloudflare.com/ajax/libs/markdown-it-emoji/2.0.2/markdown-it-emoji.min.js"></script>
<script>
  window.MasaBlazor.extendMarkdownIt = function (parser) {
    const { md } = parser;
    if (window.markdownitEmoji) {
      md.use(window.markdownitEmoji);
    }
  }
</script>
```

<masa-example file="Examples.components.markdown_parsers.Emoji"></masa-example>
