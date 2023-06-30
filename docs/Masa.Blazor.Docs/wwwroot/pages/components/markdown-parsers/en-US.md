---
title: Markdown parsers
desc: "Packaging based on [markdown-it(v13.0.1)](https://github.com/markdown-it/markdown-it)."
tag: JS Proxy
related:
  - /blazor/components/syntax-highlights
  - /blazor/components/markdowns
  - /blazor/components/editors
---

This component built-in the following third-party markdown-it plugins:
- [markdown-it-anchor](https://github.com/valeriangalliat/markdown-it-anchor): Through the `AnchorOptions` property, you can configure the style of the anchor.
- [markdown-it-attrs](https://github.com/arve0/markdown-it-attrs)：Add classes, identifiers and attributes to markdown using `{.class #identifier attr=value attr2="spaced value"}`.
- [markdown-it-container](https://github.com/markdown-it/markdown-it-container)：Create block-level custom containers.
- [markdown-it-front-matter](https://github.com/ParkSB/markdown-it-front-matter)：Resolve front matter as string.
- [markdown-it-header-sections](https://github.com/arve0/markdown-it-header-sections)：Group headers into sections. The `HeaderSections` property needs to be enabled.
- [markdown-it-todo](https://github.com/dexfire/markdown-it-todo): Render `[ ]` and `[x]` as checkboxes.

> About how to use other plugins, please refer to the example of [Use third-party plugins](#use-third-party-plugins).

> About how to customize rules and custom containers, please refer to the [source code]((https://github.com/masastack/MASA.Blazor/blob/main/docs/Masa.Docs.Shared/wwwroot/js/markdown-parser.js)) of this document.

> **prism** and **highlightjs** highlighting schemes are supported, see the [MSyntaxHighlight](/blazor/components/syntax-highlights) component for details.

## Usage

<masa-example file="Examples.components.markdown_parsers.Usage"></masa-example>

## Examples

### Props

#### Options

All options list please see [markdown-it](https://github.com/markdown-it/markdown-it#init-with-presets-and-options)。

<masa-example file="Examples.components.markdown_parsers.Options"></masa-example>

### Events

#### OnFrontMatterParsed

Front Matter is a format for adding metadata to Markdown files. It is an extension of the YAML format. Use the `OnFrontMatterParsed` event to get the content of Front Matter.

<masa-example file="Examples.components.markdown_parsers.OnFrontMatterParsed"></masa-example>

#### OnTocParsed

Through the `OnTocParsed` event, you can get the directory structure of the Markdown file.

<masa-example file="Examples.components.markdown_parsers.OnTocParsed"></masa-example>

### Misc

#### Use third-party plugins

For example, how to use the [markdown-it-emoji](https://github.com/markdown-it/markdown-it-emoji) plugin. Use the `window.MasaBlazor.extendMarkdownIt` method to extend the markdown-it instance.

``` html
<script src="https://cdnjs.cloudflare.com/ajax/libs/markdown-it-emoji/2.0.2/markdown-it-emoji.min.js"></script>
<script>
  window.MasaBlazor.extendMarkdownIt = function (parser) { 
    if (window.markdownitEmoji) {
      md.use(window.markdownitEmoji);
    }
  }
</script>
```

<masa-example file="Examples.components.markdown_parsers.Emoji"></masa-example>
