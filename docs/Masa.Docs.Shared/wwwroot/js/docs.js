window.registerWindowScrollEvent = function (dotnet, selector) {
  let _timeout;
  let _offsets = [];
  let _toc = [];
  let _scrollingFromClickEvent;

  const toc = document.querySelector(selector);
  toc.addEventListener('click', async (e) => {
    const el = e.target;
    if (el.tagName !== "A") return

    routeToNamedElement(el, 108, true)

    el.click();

    _scrollingFromClickEvent = true;

    await dotnet.invokeMethodAsync("UpdateHash", el.getAttribute('href'));
  })

  let isEnd
  window.addEventListener("scroll", onScroll);
  window.addEventListener("scrollend", () => {
    clearTimeout(isEnd)
    isEnd = setTimeout(() => {
      _scrollingFromClickEvent = false;
    }, 300)
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

    const hash = _toc[tindex];

    await dotnet.invokeMethodAsync("UpdateHash", hash);
  }

  function onScroll() {
    if (_scrollingFromClickEvent) return;

    clearTimeout(_timeout);

    _timeout = setTimeout(findActiveIndex, 17);
  }
};

window.backTop = function () {
  slideTo(0);
};

window.routeToNamedElement = function (el, offset, once = false) {
  el.addEventListener('click', (e) => {
    const hash = el.getAttribute('href')

    history.pushState({}, "", window.location.pathname + hash)

    const namedElement = document.querySelector(hash);
    const top = namedElement.getBoundingClientRect().top;
    const offsetPosition = top + window.pageYOffset - offset;
    
    window.scrollTo({top: offsetPosition, behavior: "smooth"});
  }, {once})

}

window.scrollToElement = function (selector, offset) {
  var x = window.pageXOffset,
    y = window.pageYOffset;

  window.addEventListener('scroll', () => {
    window.scrollTo(x, y)
  }, {once: true})

  window.location.hash = selector;

  // scrolling = true;
  isHash = true;
  const el = document.querySelector(selector);
  console.log('el', el)
  const top = el.getBoundingClientRect().top;
  const offsetPosition = top + window.pageYOffset - offset;
  console.log('top', offsetPosition)
  window.requestAnimationFrame(() => {
    window.scrollTo({top: offsetPosition, behavior: "smooth"});
  })
};

window.activeNavItemScrollIntoView = function (ancestorSelector) {
  const activeListItem = document.querySelector(
    `${ancestorSelector} .m-list-item--active:not(.m-list-group__header)`
  );
  if (!activeListItem) return;
  activeListItem.scrollIntoView({behavior: "smooth"});
};

function slideTo(targetPageY) {
  var timer = setInterval(function () {
    var currentY =
      document.documentElement.scrollTop || document.body.scrollTop;
    var distance =
      targetPageY > currentY ? targetPageY - currentY : currentY - targetPageY;
    var speed = Math.ceil(distance / 10);
    if (currentY == targetPageY || currentY == 1) {
      document.documentElement.scrollTop = 0;
      clearInterval(timer);
    } else {
      window.scrollTo(
        0,
        targetPageY > currentY ? currentY + speed : currentY - speed
      );
    }
  }, 10);
}

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
    apiKey: "d1fa64adb784057c097feb592d4497d0",
    // hitComponent: ({ hit, children }) => {
    //   return {
    //     type: "a",
    //     ref: undefined,
    //     constructor: undefined,
    //     props: {
    //       href: hit.url,
    //       onClick: (e) => {
    //         let hitUrl;
    //         if (hit.url.startsWith("/")) {
    //           hitUrl = new URL(hit.url, location.origin);
    //         } else {
    //           hitUrl = new URL(hit.url);
    //         }
    //         if (document.location.pathname === hitUrl.pathname) {
    //           if (hitUrl.hash) {
    //             window.requestAnimationFrame(() =>
    //               window.scrollToElement(hitUrl.hash.substring(1), 108)
    //             );
    //           } else {
    //             window.backTop();
    //           }
    //         }
    //       },
    //       children: children,
    //     },
    //     __v: null,
    //   };
    // },
    placeholder,
    searchParameters: {
      facetFilters: ["lang:" + currentLanguage],
    },
  };
  if (currentLanguage == "zh") {
    option.translations = cnTranslation;
  }
  docsearch(option);
};
