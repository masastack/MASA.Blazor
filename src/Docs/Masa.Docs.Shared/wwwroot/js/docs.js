/*
 * NavigationMananger.NavigateTo always scrolls page to the top.
 * The following `window.scrollTo` would be invoked.
 * When NavigationManager.NavigateTo invoked, the x and y is zero.
 */
const origScrollTo = window.scrollTo;
window.scrollTo = function (x, y) {
  if (x === 0 && y === 0) return;
  return origScrollTo.apply(this, arguments);
};

window.setCookie = function (name, value) {
  document.cookie = `$ {
                name
            } = $ {
                escape(value.toString())
            };
            path = /;}`;
};

window.getCookie = function (name) {
  const reg = new RegExp(`(^| )${name}=([^;]*)(;|$)`);
  const arr = document.cookie.match(reg);
  if (arr) {
    return unescape(arr[2]);
  }
  return null;
};

window.getCurrentDocSearchLanguage = function () {
  const language = window.getCookie("Masa_I18nConfig_Language");
  if (!language || language === "zh-CN") {
    return "zh";
  }
  return "en";
};

window.addDoSearch = function (isMobile) {
  const container = `#docsearch${isMobile ? "-mobile" : ""}`;
  docsearch({
    container,
    appId: "TSB4MACWRC",
    indexName: "blazor-masastack",
    apiKey: "a38a8d4b58c5648825ba3fafce8b6ffa",
    debug: false,
    //searchParameters: {
    //    facetFilters: ['language:'+ getCurrentDocSearchLanguage()]
    //}
  });
};

window.getTimeOffset = function () {
  return new Date().getTimezoneOffset();
};

/*
 * markdown-it-proxy would invoke this function to set rules.
 */
window.BlazorComponent.markdownItRules = function (key, markdownIt) {
  if (key !== "document") return;

  addHeadingRules(markdownIt);
  addLinkRules(markdownIt);

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

  function addLinkRules(md) {
    md.renderer.rules.link_open = (tokens, idx, options, env, self) => {
      const next = tokens[idx + 1];
      const content = next.content;

      tokens[idx].tag = "app-link";
      tokens[idx].attrSet("content", content);

      return self.renderToken(tokens, idx, options);
    };
    md.renderer.rules.link_close = (tokens, idx, options, env, self) => {
      tokens[idx].tag = "app-link";

      return self.renderToken(tokens, idx, options);
    };
  }
};

window.registerWindowScrollEventForToc = function (dotnet, tocId) {
  let _timeout;
  let _scrolling;
  let _offsets = [];
  let _toc = [];
  let _registered;

  window.addEventListener("scroll", onScroll);

  function registerClickEvents() {
    if (_registered) return
    const elements = document.querySelectorAll(`#${tocId} li`);
    if (elements && elements.length > 0) {
      _registered = true;
      for (const e of elements) {
        e.addEventListener('click', async () => {
          _scrolling = true;
          await new Promise(resolve => setTimeout(resolve, 600))
          _scrolling = false;
        })
      }
    }
  }

  function setOffsets() {
    const offsets = [];
    _toc = Array.from(document.querySelectorAll(`#${tocId} li>a`)).map(({href}) => {
      const index = href.indexOf("#");
      return href.slice(index);
    })

    const toc = _toc.slice().reverse();

    for (const item of toc) {
      const section = document.getElementById(item.slice(1));
      if (!section) continue;
      offsets.push(section.offsetTop - 48);
    }
    _offsets = offsets;
  }

  async function findActiveIndex() {
    const currentOffset =
      window.pageYOffset || document.documentElement.offsetTop || 0;

    if (currentOffset === 0) {
      await dotnet.invokeMethodAsync("UpdateHash", "")
      return
    }

    setOffsets();

    const index = _offsets.findIndex((offset) => {
      return offset < currentOffset;
    });

    let tindex = index > -1 ? _offsets.length - 1 - index : 0;

    if (
      currentOffset + window.innerHeight ===
      document.documentElement.offsetHeight
    ) {
      tindex = _toc.length - 1;
    }

    const hash = _toc[tindex];

    _scrolling = true;

    await dotnet.invokeMethodAsync("UpdateHash", hash);

    _scrolling = false;
  }

  function onScroll() {
    clearTimeout(_timeout);

    registerClickEvents();

    if (_scrolling) {
      return;
    }

    _timeout = setTimeout(findActiveIndex, 17);
  }
};
