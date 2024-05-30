declare namespace MarkdownIt {
  type PresetName = "default" | "zero" | "commonmark";

  interface Options {
    /**
     * Set `true` to enable HTML tags in source. Be careful!
     * That's not safe! You may need external sanitizer to protect output from XSS.
     * It's better to extend features via plugins, instead of enabling HTML.
     * @default false
     */
    html?: boolean | undefined;

    /**
     * Set `true` to add '/' when closing single tags
     * (`<br />`). This is needed only for full CommonMark compatibility. In real
     * world you will need HTML output.
     * @default false
     */
    xhtmlOut?: boolean | undefined;

    /**
     * Set `true` to convert `\n` in paragraphs into `<br>`.
     * @default false
     */
    breaks?: boolean | undefined;

    /**
     * CSS language class prefix for fenced blocks.
     * Can be useful for external highlighters.
     * @default 'language-'
     */
    langPrefix?: string | undefined;

    /**
     * Set `true` to autoconvert URL-like text to links.
     * @default false
     */
    linkify?: boolean | undefined;

    /**
     * Set `true` to enable [some language-neutral replacement](https://github.com/markdown-it/markdown-it/blob/master/lib/rules_core/replacements.js) +
     * quotes beautification (smartquotes).
     * @default false
     */
    typographer?: boolean | undefined;

    /**
     * Double + single quotes replacement
     * pairs, when typographer enabled and smartquotes on. For example, you can
     * use `'«»„“'` for Russian, `'„“‚‘'` for German, and
     * `['«\xA0', '\xA0»', '‹\xA0', '\xA0›']` for French (including nbsp).
     * @default '“”‘’'
     */
    quotes?: string | string[];

    /**
     * Highlighter function for fenced code blocks.
     * Highlighter `function (str, lang, attrs)` should return escaped HTML. It can
     * also return empty string if the source was not changed and should be escaped
     * externally. If result starts with <pre... internal wrapper is skipped.
     * @default null
     */
    highlight?:
      | ((str: string, lang: string, attrs: string) => string)
      | null
      | undefined;
  }
}

declare interface MarkdownIt {
  [prop: string]: any;
}

declare module "markdown-it" {
  interface MarkdownItConstructor {
    new (): MarkdownIt;
    new (
      presetName: MarkdownIt.PresetName,
      options?: MarkdownIt.Options
    ): MarkdownIt;
    new (options: MarkdownIt.Options): MarkdownIt;
    (): MarkdownIt;
    (
      presetName: MarkdownIt.PresetName,
      options?: MarkdownIt.Options
    ): MarkdownIt;
    (options: MarkdownIt.Options): MarkdownIt;
  }

  const MarkdownIt: MarkdownItConstructor;

  export default MarkdownIt;
}
