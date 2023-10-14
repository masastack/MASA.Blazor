/*
 * markdown-it-proxy would invoke this function to extend markdown-it.
 */

window.MasaBlazor.extendMarkdownIt = function (parser) {
    const { md, scope } = parser;
    
    if (window.markdownitEmoji) {
        md.use(window.markdownitEmoji);
    }

    if (scope === "document") {
        addHeadingRules(md);
        addCodeRules(md);
        addImageRules(md);
        addBlockquoteRules(md);
        addTableRules(md);
        addHtmlInlineRules(md)

        parser.useContainer("code-group");
        parser.useContainer("code-group-item");
        addCodeGroupRules(parser);
    }

    addLinkRules(parser);
    
    function addHtmlInlineRules(md) {
        md.renderer.rules.html_inline = (tokens, idx, options, env, self) => {
            const content = tokens[idx].content;
            if(content.startsWith("</")) {
                return content;
            }

            let tag = content.split(' ')[0].substring(1);
            if (tag.endsWith('>')) {
                tag = tag.replace('>', '');
            }

            if (customElements.get(tag)){
                return content.replace(">", ` masa-blazor-custom-element>`)
            }

            return content;
        }
    }

    function addHeadingRules(md) {
        md.renderer.rules.heading_open = (tokens, idx, options, env, self) => {
            const level = tokens[idx].markup.length;
            const next = tokens[idx + 1];
            const children = next ? next.children : [];
            const [, href] = children[2].attrs[0];
            const content = children[0].content;

            tokens[idx].tag = "app-heading";
            tokens[idx].attrSet("content", content);
            tokens[idx].attrSet("href", href);
            tokens[idx].attrSet("level", level);

            return self.renderToken(tokens, idx, options);
        };
        md.renderer.rules.heading_close = (tokens, idx, options, env, self) => {
            tokens[idx].tag = "app-heading";

            return self.renderToken(tokens, idx, options);
        };
    }

    function addLinkRules({ md, defaultSlugify }) {
        md.renderer.rules.link_open = (tokens, idx, options, env, self) => {
            let i = 1;
            let next = tokens[idx + i];
            let content = next.content;

            while (!content && next.type !== "link_close") {
                i++;
                next = tokens[idx + i];
                content = next.content;
            }

            tokens[idx].tag = "app-link";

            let href = tokens[idx].attrGet("href");
            let isRelativeUrl = true;
            try {
                let url = new URL(href);
                isRelativeUrl = false;
            } catch (TypeError) {}
            if (isRelativeUrl) {
                let decodedHref = decodeURI(href);
                if (decodedHref !== href) {
                    const [path, hash] = decodedHref.split("#");
                    if (hash) {
                        tokens[idx].attrSet("href", path + "#" + defaultSlugify(hash));
                    }
                }
            }

            tokens[idx].attrSet("content", content);

            return self.renderToken(tokens, idx, options);
        };
        md.renderer.rules.link_close = (tokens, idx, options, env, self) => {
            tokens[idx].tag = "app-link";

            return self.renderToken(tokens, idx, options);
        };
    }

    function addCodeRules(md) {
        md.renderer.rules.fence = (tokens, idx, options, env, self) => {
            if (tokens[idx].markup === "```") {
                const content = tokens[idx].content;

                const [lang, fileName, lineHighlights] = resolveCodeInfo(tokens[idx].info)

                return `<default-app-markup code="${content.replaceAll(
                    '"',
                    "&quot;"
                )}" language="${lang}" file-name="${fileName || ""}" line-highlights="${lineHighlights || ""}"></default-app-markup>\n`;
            }
        };
    }

    function addImageRules(md) {
        md.renderer.rules.image = (tokens, idx, options, env, self) => {
            tokens[idx].attrSet("width", "100%");
            return self.renderToken(tokens, idx, options);
        };
    }

    function addBlockquoteRules(md) {
        md.renderer.rules.blockquote_open = (tokens, idx, options, env, self) => {
            tokens[idx].tag = "div";
            tokens[idx].attrSet("class", "m-alert__content");
            return (`<div role="alert" class="m-alert m-alert--doc m-sheet m-alert--border m-alert--border-left m-alert--text info--text"><div class="m-alert__wrapper"><i class="m-icon theme--dark info--text mdi mdi-information m-alert__icon"></i><div class="m-alert__border m-alert__border--left"></div>${self.renderToken(tokens, idx, options)}`);
        };
        md.renderer.rules.blockquote_close = (tokens, idx, options, env, self) => {
            return self.renderToken(tokens, idx, options) + "</div></div></div>";
        };
    }

    function addTableRules(md) {
        md.renderer.rules.table_open = (tokens, idx, options, env, self) => {
            return (
                '<div masa-blazor-html class="m-sheet m-sheet--outlined m-sheet--no-bg rounded theme--light mb-2"><div masa-blazor-html class="m-data-table m-data-table--fixed-height theme--light"><div class="m-data-table__wrapper">' +
                self.renderToken(tokens, idx, options)
            );
        };
        md.renderer.rules.table_close = (tokens, idx, options, env, self) => {
            return self.renderToken(tokens, idx, options) + "</div></div></div>";
        }
    }

    function addCodeGroupRules(parser) {
        parser.md.renderer.rules["container_code-group_open"] = (
            tokens,
            idx,
            options,
            env,
            self
        ) => {
            let nextIndex = idx;
            let nextToken = tokens[idx];

            const dic = {};

            while (nextToken) {
                nextIndex++;
                nextToken = tokens[nextIndex];

                if (nextToken.type === "container_code-group-item_open") {
                    const item = nextToken.info.replace("code-group-item", "").trim();

                    nextIndex++;
                    nextToken = tokens[nextIndex];

                    if (nextToken.type === "fence") {
                        const { content: code, info } = nextToken;
                        const [lang, fileName, lineHighlights]= resolveCodeInfo(info)

                        dic[item] = { code, lang, fileName, lineHighlights };
                    }
                }

                if (nextToken.type === "container_code-group_close") {
                    break;
                }
            }

            const g_attr = `code_group_${idx}`;
            parser.afterRenderCallbacks.push(() => {
                const selector = `[${g_attr}]`;
                const element = document.querySelector(selector);
                if (element) {
                    element.model = dic;
                }
            });

            return `<app-code-group masa-blazor-custom-element ${g_attr}>\n`;
        };
        parser.md.renderer.rules["container_code-group_close"] = (
            tokens,
            idx,
            options,
            env,
            self
        ) => {
            return `</app-code-group>\n`;
        };
    }

    function resolveCodeInfo(info) {
        info = (info || "").trim();
        const [lang, ...res] = info.split(/\s+/);

        let fileName, lineHighlights;

        const f = res.find(u => u.startsWith("f:"))
        const l = res.find(u => u.startsWith("l:"))

        if (res.length > 0 && !f && res[0] !== l) {
            fileName = res[0];
        } else {
            fileName = f && f.substring(2)
        }

        lineHighlights = l && l.substring(2)

        return [lang, fileName, lineHighlights];
    }
};

window.prismHighlightLines = function (pre) {
    if (!pre) return;
    try{
        setTimeout(() => {
            Prism.plugins.lineHighlight.highlightLines(pre)();
        }, 300) // in code-group-item, need to wait for 0.3s transition animation
    } catch (err) {
        console.error(err);
    }
}

window.updateThemeOfElementsFromMarkdown = function (isDark) {
    const customElements = document.querySelectorAll('[masa-blazor-custom-element]');
    
    [...customElements].map(e => {
        e.setAttribute("dark", isDark)
    });

    const elements = document.querySelectorAll('[masa-blazor-html]');
    [...elements].map(e => {
        if (isDark) {
            if (e.className.includes('theme--light')) {
                e.className = e.className.replace('theme--light', 'theme--dark')
            } else {
                e.className += " theme--dark";
            }
        } else {
            if (e.className.includes('theme--dark')) {

                e.className = e.className.replace('theme--dark', 'theme--light')
            } else {
                e.className += " theme--light";
            }
        }
    })
}