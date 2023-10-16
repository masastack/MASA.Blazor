let scrollingFromClickEvent;

window.scrollToElement = function (hash, offset, preventAutoHighlightToc = false) {
  scrollingFromClickEvent = preventAutoHighlightToc;

  const el = document.querySelector(hash);
  if (!el) return;

  const top = el.getBoundingClientRect().top;
  const offsetPosition = top + window.pageYOffset - offset;

  window.scrollTo({top: offsetPosition, behavior: "smooth"});
};

/*
 * NavigationManager.NavigateTo always scrolls page to the top.
 * The following `window.scrollTo` would be invoked.
 * When NavigationManager.NavigateTo invoked, the x and y is zero.
 */
const origScrollTo = window.scrollTo;
window.scrollTo = function (x, y) {
  if (x === 0 && y === 0) {
    return;
  }
  return origScrollTo.apply(this, arguments);
};

window.registerWindowScrollEvent = function (dotnet, selector) {
  let _timeout;
  let _scrolling;
  let _offsets = [];
  let _toc = [];
  let _registered;
  let scrollend;

  window.addEventListener("scroll", onScroll);
  window.addEventListener("scrollend", () => {
    clearTimeout(scrollend)
    setTimeout(() => {
      scrollingFromClickEvent = false;
    }, 100)
  })

  function setOffsets() {
    const offsets = [];
    var queryFilter = selector;
    var firstNode = document.querySelector(queryFilter);
    if (
      !firstNode ||
      (firstNode && !firstNode.attributes.getNamedItem("href"))
    ) {
      queryFilter = `${selector} a`;
    }

    _toc = Array.from(document.querySelectorAll(queryFilter)).map(
      ({attributes}) => {
        let href = attributes.getNamedItem("href").value;
        const index = href.indexOf("#");
        return href.slice(index);
      }
    );

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
      updateHash("")
      await dotnet.invokeMethodAsync("UpdateHash", "");
      return;
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

    const hash = _toc[tindex] ?? "";

    updateHash(hash)
    if (hash) {
      await dotnet.invokeMethodAsync("UpdateHash", hash);
    }
  }

  function onScroll() {
    if (scrollingFromClickEvent) return

    clearTimeout(_timeout);

    _timeout = setTimeout(findActiveIndex, 17);
  }

  function updateHash(h) {
    history.replaceState({}, "", window.location.pathname + h)
  }
};

window.backTop = function () {
  window.scrollTo({top: 0, behavior: "smooth"})
};

window.activeNavItemScrollIntoView = function (ancestorSelector) {
  setTimeout(() => {
    const activeListItem = document.querySelector(
      `${ancestorSelector} .m-list-item--active:not(.m-list-group__header)`
    );

    if (!activeListItem) return;
    activeListItem.scrollIntoView({behavior: "smooth"});
  }, 500)
};

window.addDocSearch = function (index, currentLanguage, placeholder) {
  let cnTranslation = {
    button: {
      buttonText: "搜索",
      buttonAriaLabel: "搜索",
    },
    modal: {
      searchBox: {
        resetButtonTitle: "清除搜索",
        resetButtonAriaLabel: "清除搜索",
        cancelButtonText: "取消",
        cancelButtonAriaLabel: "取消",
      },
      startScreen: {
        recentSearchesTitle: "最近",
        noRecentSearchesText: "没有搜索历史",
        saveRecentSearchButtonTitle: "保存搜索",
        removeRecentSearchButtonTitle: "从历史移除记录",
        favoriteSearchesTitle: "收藏",
        removeFavoriteSearchButtonTitle: "从收藏移除记录",
      },
      errorScreen: {
        titleText: "不能获取结果",
        helpText: "可能由于网络链接中断",
      },
      footer: {
        selectText: "选中",
        selectKeyAriaLabel: "回车键",
        navigateText: "导航",
        navigateUpKeyAriaLabel: "向下键",
        navigateDownKeyAriaLabel: "向上键",
        closeText: "关闭",
        closeKeyAriaLabel: "ESC键",
        searchByText: "搜索提供：",
      },
      noResultsScreen: {
        noResultsText: "没有找到记录:",
        suggestedQueryText: "尝试搜索中:",
        reportMissingResultsText: "确认这个关键词应该返回记录？",
        reportMissingResultsLinkText: "告诉我们。",
      },
    },
  };

  const container = `#docsearch`;
  let option = {
    container,
    appId: "TSB4MACWRC",
    indexName: "blazor-masastack_" + index,
    apiKey: "12f9496bb2f06001f90cf49738e1a227",
    hitComponent: ({hit, children}) => {
      return {
        type: "a",
        ref: undefined,
        constructor: undefined,
        props: {
          href: hit.url,
          onClick: (e) => {
            let hitUrl;
            if (hit.url.startsWith("/")) {
              hitUrl = new URL(hit.url, location.origin);
            } else {
              hitUrl = new URL(hit.url);
            }
            if (document.location.pathname === hitUrl.pathname) {
              if (hitUrl.hash) {
                window.requestAnimationFrame(() =>
                  window.scrollToElement(hitUrl.hash, 108)
                );
              } else {
                window.scrollTo({top: 0, behavior: "smooth"})
              }
            }
          },
          children: children,
        },
        __v: null,
      };
    },
    placeholder,
    searchParameters: {
      facetFilters: ["lang:" + currentLanguage],
    },
  };
  if (currentLanguage === "zh") {
    option.translations = cnTranslation;
  }
  docsearch(option);
};

window.switchTheme = function (dotNetHelper,dark,x,y) {
  document.documentElement.style.setProperty('--x', x + 'px')
  document.documentElement.style.setProperty('--y', y + 'px')
  document.startViewTransition(() => {
    dotNetHelper.invokeMethodAsync('ToggleTheme',dark);
  });
}

window.isDarkPreferColor = function() {
  return window.matchMedia('(prefers-color-scheme: dark)').matches
}
