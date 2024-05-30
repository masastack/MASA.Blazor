declare const Prism: Prism;
declare const hljs: hljs;

function getHighlighter() {
  try {
    if (Prism) {
      return {
        getLanguage(lang: string) {
          return Prism.languages[lang];
        },
        highlight(code: string, lang: string) {
          return Prism.highlight(code, Prism.languages[lang], lang);
        },
      };
    }
  } catch (error) {}

  try {
    if (hljs) {
      return {
        getLanguage(lang: string) {
          return hljs.getLanguage(lang);
        },
        highlight(code: string, lang: string) {
          return hljs.highlight(code, { language: lang }).value;
        },
      };
    }
  } catch (error) {}

  return undefined;
}

export function highlight(code: string, lang: string) {
  const highlighter = getHighlighter();

  if (highlighter) {
    if (lang && lang.trim()) {
      lang = getLangCodeFromExtension(lang.toLowerCase());

      if (highlighter.getLanguage(lang)) {
        try {
          return highlighter.highlight(code, lang);
        } catch (error) {
        console.error(
          `Syntax highlight for language ${lang} failed.`
        );
        }
      } else {
        console.warn(
          `[markdown-it-proxy] Syntax highlight for language "${lang}" is not supported.`
        );
      }
    }
  } else {
    console.warn(
      `Highlighter(Prismjs or Highlight.js) is required!`
    );
  }

  return encodeHTML(code);
}

export function highlightToStream(str: string, lang: string): ArrayBuffer {
  var htmlCode = highlight(str, lang);
  return new TextEncoder().encode(htmlCode);
}

function getLangCodeFromExtension(extension) {
  const extensionMap = {
    cs: "csharp",
    md: "markdown",
    ts: "typescript",
    py: "python",
    razor: "cshtml",
    sh: "bash",
    yml: "yaml",
  };

  return extensionMap[extension] || extension;
}

function encodeHTML(rawStr: string) {
  return rawStr.replace(/[\u00A0-\u9999<>\&]/g, function(i) {
    return '&#'+i.charCodeAt(0)+';';
  });
}
