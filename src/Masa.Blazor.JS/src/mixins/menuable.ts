import { convertToUnit, getActivator } from "utils/helper";

type Options = {
  absolute: boolean;
  auto: boolean;
  offsetY: boolean;
  nudgeWidth: number | string;
  nudgeTop: string;
  nudgeBottom: string;
  offsetOverflow: boolean;
  allowOverflow: boolean;
  minWidth: number | string;
  maxWidth: number | string;
  isRtl: boolean;
  maxHeight: number | string;
  contentStyle: string;
  origin: string;
};

export function useMenuable(
  activatorSelector: string,
  contentElement: HTMLElement,
  hasActivator: boolean,
  options: Options
) {
  console.log("activatorSelector", activatorSelector);
  console.log("contentElement", contentElement);
  console.log("hasActivator", hasActivator);
  console.log("options", options);

  let hasWindow,
    attach,
    activatorFixed,
    pageYOffset,
    pageWidth,
    positionY,
    positionX,
    absoluteY,
    absoluteX,
    relativeYOffset,
    offsetX,
    nudgeLeft,
    nudgeRight,
    right,
    selectedIndex,
    tiles: HTMLElement[] = [];

  const defaultOffset = 8;

  let dimensions = {
    activator: {
      top: 0,
      left: 0,
      bottom: 0,
      right: 0,
      width: 0,
      height: 0,
      offsetTop: 0,
      scrollHeight: 0,
      offsetLeft: 0,
    },
    content: {
      top: 0,
      left: 0,
      bottom: 0,
      right: 0,
      width: 0,
      height: 0,
      offsetTop: 0,
      scrollHeight: 0,
    },
  };

  return {
    updateDimensions,
  };

  function updateDimensions() {
    hasWindow = typeof window !== "undefined";
    checkActivatorFixed();
    checkForPageYOffset();
    pageWidth = document.documentElement.clientWidth;

    const dimensions2: any = {
      activator: { ...dimensions.activator },
      content: { ...dimensions.content },
    };

    // Activator should already be shown
    if (!hasActivator || options.absolute) {
      dimensions2.activator = absolutePosition();
    } else {
      const activator = getActivator(activatorSelector);
      if (!activator) return;

      dimensions2.activator = measure(activator);
      dimensions2.activator.offsetLeft = activator.offsetLeft;
      if (attach !== false) {
        // account for css padding causing things to not line up
        // this is mostly for v-autocomplete, hopefully it won't break anything
        dimensions2.activator.offsetTop = activator.offsetTop;
      } else {
        dimensions2.activator.offsetTop = 0;
      }
    }

    // Display and hide to get dimensions
    sneakPeek(() => {
      console.log("sneakPeek");
      if (contentElement) {
        if (contentElement.offsetParent) {
          const offsetRect = getRoundedBoundedClientRect(
            contentElement.offsetParent
          );

          relativeYOffset = window.pageYOffset + offsetRect.top;
          dimensions2.activator.top -= relativeYOffset;
          dimensions2.activator.left -= window.pageXOffset + offsetRect.left;
        }

        dimensions2.content = measure(contentElement);
      }

      dimensions = dimensions2;
      console.log("sneakPeek updateDimensions", dimensions2);

      setStyles();
    });
  }

  function setStyles() {
    contentElement.style.cssText = options.contentStyle;

    //TODO: 要判断已存在就不更新吗？
    contentElement.style.maxHeight = calculatedMaxHeight();
    contentElement.style.minWidth = calculatedMinWidth();
    contentElement.style.maxWidth = calculatedMaxWidth();
    contentElement.style.top = calculatedTop();
    console.log("top", calculatedTop());
    contentElement.style.left = calculatedLeft();
    console.log("Left", calculatedLeft());
    contentElement.style.transformOrigin = options.origin;

  }

  function calculatedTop() {
    return !options.auto
      ? calcTop()
      : convertToUnit(calcYOverflow(calcTopAuto())); // TODO: 这里与vueitfy不一致
  }

  function calculatedLeft(): string {
    const menuWidth = Math.max(
      dimensions.content.width,
      parseFloat(calculatedMinWidth())
    );

    if (!options.auto) return calcLeft(menuWidth) || "0";

    return convertToUnit(calcXOverflow(calcLeftAuto(), menuWidth)) || "0";
  }

  function calcLeftAuto() {
    return parseInt(dimensions.activator.left - defaultOffset * 2);
  }

  function calculatedMaxHeight(): string {
    const height = options.auto ? "200px" : convertToUnit(options.maxHeight);

    return height || "0";
  }

  function calculatedMinWidth(): string {
    if (options.minWidth) {
      return convertToUnit(options.minWidth) || "0";
    }

    const minWidth = Math.min(
      dimensions.activator.width +
        Number(options.nudgeWidth) +
        (options.auto ? 16 : 0),
      Math.max(pageWidth - 24, 0)
    );

    const calculatedMaxWidth2 = isNaN(parseInt(calculatedMaxWidth()))
      ? minWidth
      : parseInt(calculatedMaxWidth());

    return convertToUnit(Math.min(calculatedMaxWidth2, minWidth)) || "0";
  }

  function calculatedMaxWidth(): string {
    return convertToUnit(options.maxWidth) || "0";
  }

  function calcLeft(menuWidth: number) {
    const left = computedLeft();
    return convertToUnit(
      attach !== false ? left : calcXOverflow(left, menuWidth)
    );
  }
  function calcTop() {
    const top = computedTop();
    return convertToUnit(attach !== false ? top : calcYOverflow(top));
  }

  function calcTopAuto() {
    const activeTile = contentElement.querySelector(
      ".m-list-item--active"
    ) as HTMLElement | null;

    if (!activeTile) {
      selectedIndex = null;
    }

    if (options.offsetY || !activeTile) {
      return computedTop();
    }

    selectedIndex = Array.from(tiles).indexOf(activeTile);

    const tileDistanceFromMenuTop = activeTile.offsetTop - calcScrollPosition();
    const firstTileOffsetTop = (
      contentElement.querySelector(".m-list-item") as HTMLElement
    ).offsetTop;

    return computedTop() - tileDistanceFromMenuTop - firstTileOffsetTop - 1;
  }

  function calcScrollPosition() {
    const $el = contentElement;
    const activeTile = $el.querySelector(".m-list-item--active") as HTMLElement;
    const maxScrollTop = $el.scrollHeight - $el.offsetHeight;

    return activeTile
      ? Math.min(
          maxScrollTop,
          Math.max(
            0,
            activeTile.offsetTop -
              $el.offsetHeight / 2 +
              activeTile.offsetHeight / 2
          )
        )
      : $el.scrollTop;
  }

  function computedLeft() {
    const a = dimensions.activator;
    const c = dimensions.content;
    const activatorLeft = (attach !== false ? a.offsetLeft : a.left) || 0;
    const minWidth = Math.max(a.width, c.width);
    let left = 0;
    left += activatorLeft;
    if (left || (options.isRtl && !right)) left -= minWidth - a.width;
    if (offsetX) {
      const maxWidth = isNaN(Number(options.maxWidth))
        ? a.width
        : Math.min(a.width, Number(options.maxWidth));

      left += left ? -maxWidth : a.width;
    }
    if (nudgeLeft) left -= parseInt(nudgeLeft);
    if (nudgeRight) left += parseInt(nudgeRight);

    return left;
  }

  function computedTop() {
    const a = dimensions.activator;
    const c = dimensions.content;
    let top = 0;

    if (top) top += a.height - c.height;
    if (attach !== false) top += a.offsetTop;
    else top += a.top + pageYOffset;
    if (options.offsetY) top += top ? -a.height : a.height;
    if (options.nudgeTop) top -= parseInt(options.nudgeTop);
    if (options.nudgeBottom) top += parseInt(options.nudgeBottom);

    return top;
  }

  function calcXOverflow(left: number, menuWidth: number) {
    const xOverflow = left + menuWidth - pageWidth + 12;

    if ((!left || right) && xOverflow > 0) {
      left = Math.max(left - xOverflow, 0);
    } else {
      left = Math.max(left, 12);
    }

    return left + getOffsetLeft();
  }

  function getOffsetLeft() {
    if (!hasWindow) return 0;

    return window.pageXOffset || document.documentElement.scrollLeft;
  }

  function calcYOverflow(top: number) {
    const documentHeight = getInnerHeight();
    const toTop = absoluteYOffset() + documentHeight;
    const activator = dimensions.activator;
    const contentHeight = dimensions.content.height;
    const totalHeight = top + contentHeight;
    const isOverflowing = toTop < totalHeight;

    // If overflowing bottom and offset
    // TODO: set 'bottom' position instead of 'top'
    if (
      isOverflowing &&
      options.offsetOverflow &&
      // If we don't have enough room to offset
      // the overflow, don't offset
      activator.top > contentHeight
    ) {
      top = pageYOffset + (activator.top - contentHeight);
      // If overflowing bottom
    } else if (isOverflowing && !options.allowOverflow) {
      top = toTop - contentHeight - 12;
      // If overflowing top
    } else if (top < absoluteYOffset() && !options.allowOverflow) {
      top = absoluteYOffset() + 12;
    }

    return top < 12 ? 12 : top;
  }

  function getInnerHeight() {
    if (!hasWindow) return 0;

    return window.innerHeight || document.documentElement.clientHeight;
  }

  function absoluteYOffset(): number {
    return pageYOffset - relativeYOffset;
  }

  function checkActivatorFixed() {
    if (attach !== false) {
      activatorFixed = false;
      return;
    }
    let el = getActivator(activatorSelector);
    while (el) {
      if (window.getComputedStyle(el).position === "fixed") {
        activatorFixed = true;
        return;
      }
      el = el.offsetParent as HTMLElement;
    }
    activatorFixed = false;
  }

  function checkForPageYOffset() {
    if (hasWindow) {
      pageYOffset = activatorFixed ? 0 : getOffsetTop();
    }
  }

  function getOffsetTop() {
    if (!hasWindow) return 0;

    return window.pageYOffset || document.documentElement.scrollTop;
  }

  function absolutePosition() {
    return {
      offsetTop: positionY || absoluteY,
      offsetLeft: positionX || absoluteX,
      scrollHeight: 0,
      top: positionY || absoluteY,
      bottom: positionY || absoluteY,
      left: positionX || absoluteX,
      right: positionX || absoluteX,
      height: 0,
      width: 0,
    };
  }

  function measure(el: HTMLElement) {
    if (!el || !hasWindow) return null;

    const rect = getRoundedBoundedClientRect(el);

    // Account for activator margin
    if (attach !== false) {
      const style = window.getComputedStyle(el);

      rect.left = parseInt(style.marginLeft!);
      rect.top = parseInt(style.marginTop!);
    }

    return rect;
  }

  function getRoundedBoundedClientRect(el: Element) {
    const rect = el.getBoundingClientRect();
    return {
      top: Math.round(rect.top),
      left: Math.round(rect.left),
      bottom: Math.round(rect.bottom),
      right: Math.round(rect.right),
      width: Math.round(rect.width),
      height: Math.round(rect.height),
    };
  }

  function sneakPeek(cb: () => void) {
    requestAnimationFrame(() => {
      const el = contentElement;

      if (!el || el.style.display !== "none") {
        cb();
        return;
      }

      el.style.display = "inline-block";
      cb();
      el.style.display = "none";
    });
  }
}
