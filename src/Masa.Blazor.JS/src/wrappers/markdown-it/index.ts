import MarkdownIt from "markdown-it";
import markdownItAnchor from "markdown-it-anchor";
import markdownItAttrs from "markdown-it-attrs";
import markdownItContainer from "markdown-it-container";
import markdownItFrontMatter from "markdown-it-front-matter";
import markdownItHeaderSections from "markdown-it-header-sections";
import markdownItTodo from "markdown-it-todo";

import { highlight, highlightToStream } from "./highlighter";

export type MarkdownParser = {
  md: MarkdownIt;
  scope?: string;
  useContainer: (name: string) => void;
  defaultSlugify: (s: string) => string;
  afterRenderCallbacks: (() => void)[];
  frontMatter: {
    meta?: string;
    cb: (s: string) => void;
  };
  toc: {
    contents: any;
    cb: (tocMarkdown, tocArray, tocHtml) => void;
  };
};

function create(
  options: MarkdownIt.Options = {},
  enableHeaderSections: boolean = false,
  anchorOptions = null,
  scope: string = null
) {
  options = { ...options, highlight };

  const parser = {
    scope,
    defaultSlugify: hashString,
    afterRenderCallbacks: [],
    frontMatter: {},
    toc: {},
  } as MarkdownParser;

  parser.frontMatter.meta = undefined;
  parser.frontMatter.cb = (s) => {
    parser.frontMatter.meta = s;
  };

  parser.toc.contents = [];
  parser.toc.cb = (_, array) => {
    parser.toc.contents = array;
  };

  const md: MarkdownIt = new MarkdownIt(options)
    .use(markdownItAttrs)
    .use(markdownItTodo)
    .use(markdownItFrontMatter, parser.frontMatter.cb);

  parser.md = md;
  parser.useContainer = (name) => md.use(markdownItContainer, name);

  // anchor and toc
  {
    let slugify = parser.defaultSlugify;

    md.use(markdownItAnchor, {
      level: anchorOptions?.level ?? 1,
      permalink: anchorOptions?.permalink,
      permalinkSymbol: anchorOptions?.permalinkSymbol,
      permalinkClass: anchorOptions?.permalinkClass,
      slugify,
      callback: (_token, info) => {
        parser.toc.contents.push({
          content: info.title,
          anchor: info.slug,
          level: _token.markup.length,
          attrs: Object.fromEntries(_token.attrs)
        });
      },
    });
  }

  if (enableHeaderSections) {
    md.use(markdownItHeaderSections);
  }

  if (window.MasaBlazor && window.MasaBlazor.extendMarkdownIt) {
    window.MasaBlazor.extendMarkdownIt(parser);
  }

  return parser;
}

function parse(parser: MarkdownParser, src: string) {
  const { markup } = parseAll(parser, src);
  return markup;
}

function parseAll(parser: MarkdownParser, src: string) {
  if (parser) {
    parser.frontMatter.meta = undefined;
    parser.toc.contents = [];

    try {
      const markup = parser.md.render(src);

      return {
        frontMatter: parser.frontMatter.meta,
        markup: markup,
        toc: parser.toc.contents,
      };
    } catch (error) {
      console.log("markdown-it-proxy error:", error);
    }
  }

  return {};
}

function afterRender(parser: MarkdownParser) {
  if (parser.afterRenderCallbacks) {
    while (parser.afterRenderCallbacks.length > 0) {
      const cb = parser.afterRenderCallbacks.shift();
      cb && cb();
    }
  }
}

function hashString(str: string) {
  let slug = String(str)
    .trim()
    .toLowerCase()
    .replace(/[\s,.[\]{}()/]+/g, "-")
    .replace(/[^a-z0-9 -]/g, (c) => c.charCodeAt(0).toString(16))
    .replace(/-{2,}/g, "-")
    .replace(/^-*|-*$/g, "");

  if (slug.charAt(0).match(/[^a-z]/g)) {
    slug = "section-" + slug;
  }

  return encodeURIComponent(slug);
}

export { create, parse, parseAll, afterRender, highlight, highlightToStream };
