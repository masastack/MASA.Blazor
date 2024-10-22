import { getScrollParents, hasScrollbar } from "utils/getScrollParent";
import { convertToUnit } from "utils/helper";

type StrategyProps = {
  strategy: "none" | "block" | "close" | "reposition";
  contained: boolean | undefined;
};

type ScrollStrategyData = {
  root: HTMLElement | undefined;
  contentEl: HTMLElement | undefined;
  targetEl: HTMLElement | undefined;
  invoker?: DotNet.DotNetObject;
};

type ScrollStrategyResult = {
  bind?: () => void;
  unbind: () => void;
};

export function useScrollStrategies(
  props: StrategyProps,
  root: HTMLElement | undefined,
  contentEl: HTMLElement | undefined,
  targetEl: HTMLElement | undefined,
  dotNet?: DotNet.DotNetObject
): ScrollStrategyResult {
  if (props.strategy === "block") {
    return useBlockScrollStrategy(
      {
        root,
        contentEl,
        targetEl,
      },
      props
    );
  } else {
    return useInvokerScrollStrategy(
      {
        root,
        contentEl,
        targetEl,
        invoker: dotNet,
      },
      props
    );
  }
}

function useBlockScrollStrategy(
  data: ScrollStrategyData,
  options: StrategyProps
): ScrollStrategyResult {
  const offsetParent = data.root.offsetParent;
  const scrollElements = [
    ...new Set([
      ...getScrollParents(
        data.contentEl,
        options.contained ? offsetParent : undefined
      ),
    ]),
  ];

  const scrollableParent = ((el) => hasScrollbar(el) && el)(
    offsetParent || document.documentElement
  );

  const bind = () => {
    if (scrollableParent) {
      data.root.classList.add("m-overlay--scroll-blocked");
    }

    const scrollbarWidth =
      window.innerWidth - document.documentElement.offsetWidth;

    scrollElements
      .filter((el) => !el.classList.contains("m-overlay-scroll-blocked"))
      .forEach((el, i) => {
        el.style.setProperty(
          "--m-body-scroll-x",
          convertToUnit(-el.scrollLeft)
        );
        el.style.setProperty("--m-body-scroll-y", convertToUnit(-el.scrollTop));

        if (el !== document.documentElement) {
          el.style.setProperty(
            "--m-scrollbar-offset",
            convertToUnit(scrollbarWidth)
          );
        }

        el.classList.add("m-overlay-scroll-blocked");
      });
  };

  bind();

  return {
    bind,
    unbind: () => {
      scrollElements
        .filter((el) => el.classList.contains("m-overlay-scroll-blocked"))
        .forEach((el, i) => {
          const x = parseFloat(el.style.getPropertyValue("--m-body-scroll-x"));
          const y = parseFloat(el.style.getPropertyValue("--m-body-scroll-y"));

          const scrollBehavior = el.style.scrollBehavior;

          el.style.scrollBehavior = "auto";
          el.style.removeProperty("--m-body-scroll-x");
          el.style.removeProperty("--m-body-scroll-y");
          el.style.removeProperty("--m-scrollbar-offset");
          el.classList.remove("m-overlay-scroll-blocked");

          el.scrollLeft = -x;
          el.scrollTop = -y;

          el.style.scrollBehavior = scrollBehavior;
        });

      if (scrollableParent) {
        data.root.classList.remove("m-overlay--scroll-blocked");
      }
    },
  };
}

function useInvokerScrollStrategy(
  data: ScrollStrategyData,
  options: StrategyProps
) {
  const el = data.targetEl ?? data.contentEl;

  const onScroll = () => {
    data.invoker?.invokeMethodAsync(
      "ScrollStrategy_OnScroll",
      options.strategy
    );
  };

  const scrollElements = [document, ...getScrollParents(el)];
  scrollElements.forEach((el) =>
    el.addEventListener("scroll", onScroll, { passive: true })
  );

  return {
    unbind: () => {
      data.invoker?.dispose();
      scrollElements.forEach((el) =>
        el.removeEventListener("scroll", onScroll)
      );
    },
  };
}
