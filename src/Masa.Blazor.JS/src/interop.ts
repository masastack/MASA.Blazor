import debounceIt from "just-debounce-it";
import throttle from "just-throttle";

import { parseDragEvent, parseTouchEvent, touchEvents } from "./events/EventType";
import { registerExtraEvents } from "./events/index";
import registerRippleObserver from "./ripple";
import { canUseDom, getBlazorId, getDom, getElementSelector } from "./utils/helper";

window.onload = function () {
  registerExtraEvents();
  registerPasteWithData("pastewithdata")
  registerRippleObserver();
}

export function getZIndex(el?: Element | null): number {
  if (!el || el.nodeType !== Node.ELEMENT_NODE) return 0

  const index = +window.getComputedStyle(el).getPropertyValue('z-index')

  if (!index) return getZIndex(el.parentNode as Element)
  return index
}

export function getDomInfo(element, selector = "body") {
  var result = {};

  var dom = getDom(element);

  if (dom) {
    if (dom.style && dom.style["display"] === "none") {
      // clone and set display not none because
      // element with display:none can not get the dom info
      var cloned = dom.cloneNode(true) as HTMLElement;
      cloned.style["display"] = "inline-block";
      cloned.style["z-index"] = -1000;
      dom.parentElement.appendChild(cloned);

      result = getDomInfoObj(cloned);

      dom.parentElement.removeChild(cloned);
    } else {
      result = getDomInfoObj(dom);
    }
  }

  return result;
}

function getDomInfoObj(dom: HTMLElement) {
  var result = {};
  result["offsetTop"] = dom.offsetTop || 0;
  result["offsetLeft"] = dom.offsetLeft || 0;
  result["scrollHeight"] = dom.scrollHeight || 0;
  result["scrollWidth"] = dom.scrollWidth || 0;
  result["scrollLeft"] = dom.scrollLeft || 0;
  result["scrollTop"] = dom.scrollTop || 0;
  result["clientTop"] = dom.clientTop || 0;
  result["clientLeft"] = dom.clientLeft || 0;
  result["clientHeight"] = dom.clientHeight || 0;
  result["clientWidth"] = dom.clientWidth || 0;
  var position = getElementPos(dom);
  result["offsetWidth"] = Math.round(position.offsetWidth) || 0;
  result["offsetHeight"] = Math.round(position.offsetHeight) || 0;
  result["relativeTop"] = Math.round(position.relativeTop) || 0;
  result["relativeBottom"] = Math.round(position.relativeBottom) || 0;
  result["relativeLeft"] = Math.round(position.relativeLeft) || 0;
  result["relativeRight"] = Math.round(position.relativeRight) || 0;
  result["absoluteLeft"] = Math.round(position.absoluteLeft) || 0;
  result["absoluteTop"] = Math.round(position.absoluteTop) || 0;
  return result;
}

function getElementPos(element) {
  var res: any = new Object();
  res.x = 0;
  res.y = 0;
  if (element !== null) {
    if (element.getBoundingClientRect) {
      var viewportElement = document.documentElement;
      var box = element.getBoundingClientRect();
      var scrollLeft = viewportElement.scrollLeft;
      var scrollTop = viewportElement.scrollTop;

      res.offsetWidth = box.width;
      res.offsetHeight = box.height;
      res.relativeTop = box.top;
      res.relativeBottom = box.bottom;
      res.relativeLeft = box.left;
      res.relativeRight = box.right;
      res.absoluteLeft = box.left + scrollLeft;
      res.absoluteTop = box.top + scrollTop;
    }
  }
  return res;
}

export function getParentClientWidthOrWindowInnerWidth(element: HTMLElement) {
  return element.parentElement ? element.parentElement.clientWidth : window.innerWidth;
}

export function triggerEvent(elOrString, eventType, stopPropagation) {
  var dom = getDom(elOrString);
  var evt = new Event(eventType);
  if (stopPropagation) {
    evt.stopPropagation();
  }

  return dom.dispatchEvent(evt);
}

export function setProperty(elOrString, name, value) {
  var dom = getDom(elOrString);
  dom[name] = value;
}

export function getBoundingClientRect(elOrString, attach = "body") {
  let dom = getDom(elOrString);

  var result = {};

  if (dom && dom.getBoundingClientRect) {
    if (dom.style && dom.style["display"] === "none") {
      var cloned = dom.cloneNode(true) as HTMLElement;
      cloned.style["display"] = "inline-block";
      cloned.style["z-index"] = -1000;
      document.querySelector(attach)?.appendChild(cloned);

      result = cloned.getBoundingClientRect();

      document.querySelector(attach)?.removeChild(cloned);
    } else {
      result = dom.getBoundingClientRect();
    }
  }

  return result;
}

var htmlElementEventListenerConfigs: { [prop: string]: HtmlElementEventListenerConfig[] } = {}

type HtmlElementEventListenerConfig = {
  listener?: (args: any) => void;
  options?: any;
  handle?: DotNet.DotNetObject;
}

export function addHtmlElementEventListener<K extends keyof HTMLElementTagNameMap>(
  selector: "window" | "document" | K,
  type: string,
  invoker: DotNet.DotNetObject,
  options?: boolean | AddEventListenerOptions,
  extras?: Partial<Pick<Event, "stopPropagation" | "preventDefault">> & { relatedTarget?: string, throttle?: number, debounce?: number, key?: string }) {
  let htmlElement: HTMLElement | Window

  if (selector == "window") {
    htmlElement = window;
  } else if (selector == "document") {
    htmlElement = document.documentElement;
  } else {
    htmlElement = document.querySelector(selector);
  }

  if (!htmlElement) {
    // throw new Error("Unable to find the element.");
    return false;
  }

  var key = extras?.key || `${selector}:${type}`;

  //save for remove
  const config: HtmlElementEventListenerConfig = {};

  var listener = (e: any): void => {
    if (extras?.stopPropagation) {
      e.stopPropagation();
    }

    if ((typeof e.cancelable !== "boolean" || e.cancelable) && extras?.preventDefault) {
      e.preventDefault();
    }

    // mouseleave relatedTarget
    if (extras?.relatedTarget && document.querySelector(extras.relatedTarget)?.contains(e.relatedTarget)) {
      return;
    }

    let obj: any = {}

    if (touchEvents.includes(e.type)) {
      obj = parseTouchEvent(e)
    } else {
      for (var k in e) {
        if (typeof e[k] == 'string' || typeof e[k] == 'number') {
          obj[k] = e[k];
        }
      }
    }

    if (e.target && e.target !== window && e.target !== document) {
      obj.target = {}
      const target = e.target as HTMLElement;
      const elementReferenceId = target.getAttributeNames().find(a => a.startsWith('_bl_'));
      if (elementReferenceId) {
        obj.target['elementReferenceId'] = elementReferenceId
        obj.target['selector'] = `[${elementReferenceId}]`
      } else {
        obj.target['selector'] = getElementSelector(target)
      }

      obj.target['class'] = target.getAttribute('class')
    }

    invoker.invokeMethodAsync('Invoke', obj);
  };

  if (extras?.debounce && extras.debounce > 0) {
    config.listener = debounceIt(listener, extras.debounce)
  } else if (extras?.throttle && extras.throttle > 0) {
    config.listener = throttle(listener, extras.throttle, { trailing: true })
  } else {
    config.listener = listener;
  }

  config.options = options;
  config.handle = invoker

  if (htmlElementEventListenerConfigs[key]) {
    htmlElementEventListenerConfigs[key].push(config);
  } else {
    htmlElementEventListenerConfigs[key] = [config]
  }

  htmlElement.addEventListener(type, config.listener, config.options);

  return true;
}

export function removeHtmlElementEventListener(selector, type, k?: string) {
  let htmlElement: any

  if (selector == "window") {
    htmlElement = window;
  } else if (selector == "document") {
    htmlElement = document.documentElement;
  } else {
    htmlElement = document.querySelector(selector);
  }

  var k = k || `${selector}:${type}`;

  var configs = htmlElementEventListenerConfigs[k];

  if (configs) {
    configs.forEach(item => {
      item.handle.dispose();
      htmlElement?.removeEventListener(type, item.listener, item.options);
    });

    htmlElementEventListenerConfigs[k] = []
  }
}

export function addMouseleaveEventListener(selector) {
  var htmlElement = document.querySelector(selector);
  if (htmlElement) {
    htmlElement.addEventListener()
  }
}

export function contains(e1, e2) {
  const dom1 = getDom(e1);
  if (dom1 && dom1.contains) {
    return dom1.contains(getDom(e2));
  }
  return false;
}

export function equalsOrContains(e1: any, e2: any) {
  const dom1 = getDom(e1);
  const dom2 = getDom(e2);
  return !!dom1 && dom1.contains && !!dom2 && (dom1 == dom2 || dom1.contains(dom2));
}

function fallbackCopyTextToClipboard(text) {
  var textArea = document.createElement("textarea");
  textArea.value = text;

  // Avoid scrolling to bottom
  textArea.style.top = "0";
  textArea.style.left = "0";
  textArea.style.position = "fixed";

  document.body.appendChild(textArea);
  textArea.focus();
  textArea.select();

  try {
    var successful = document.execCommand('copy');
    var msg = successful ? 'successful' : 'unsuccessful';
    console.log('Fallback: Copying text command was ' + msg);
  } catch (err) {
    console.error('Fallback: Oops, unable to copy', err);
  }

  document.body.removeChild(textArea);
}

export function copy(text) {
  if (!navigator.clipboard) {
    fallbackCopyTextToClipboard(text);
    return;
  }
  navigator.clipboard.writeText(text).then(function () {
    console.log('Async: Copying to clipboard was successful!');
  }, function (err) {
    console.error('Async: Could not copy text: ', err);
  });
}

export function focus(selector, noScroll: boolean = false) {
  let dom = getDom(selector);
  if (!(dom instanceof HTMLElement))
    throw new Error("Unable to focus an invalid element.");
  dom.focus({
    preventScroll: noScroll
  })
}

export function select(elOrString) {
  let dom = getDom(elOrString);
  if (!(dom instanceof HTMLInputElement || dom instanceof HTMLTextAreaElement))
    throw new Error("Unable to select an invalid element")
  dom.select()
}

export function hasFocus(selector) {
  let dom = getDom(selector);
  return (document.activeElement === dom);
}

export function blur(selector) {
  let dom = getDom(selector);
  dom.blur();
}

export function log(text) {
  console.log(text);
}

export function scrollIntoView(target, arg?: boolean | ScrollIntoViewOptions) {
  let dom = getDom(target);
  if (dom instanceof HTMLElement) {
    if (arg === null || arg == undefined) {
      dom.scrollIntoView();
    } else if (typeof arg === 'boolean') {
      dom.scrollIntoView(arg);
    } else {
      dom.scrollIntoView({
        block: arg.block == null ? undefined : arg.block,
        inline: arg.inline == null ? undefined : arg.inline,
        behavior: arg.behavior
      })
    }
  }
}

export function scrollIntoParentView(
  target,
  inline = false,
  start = false,
  level = 1,
  behavior: ScrollBehavior = "smooth",
) {
  const dom = getDom(target);
  if (dom instanceof HTMLElement) {
    let parent: HTMLElement = dom;
    while (level > 0) {
      parent = parent.parentElement;
      level--;
      if (!parent) {
        return;
      }
    }

    const options: ScrollToOptions = {
      behavior,
    };

    if (inline) {
      if (start) {
        options.left = dom.offsetLeft
      } else {
        const to = dom.offsetLeft - parent.offsetLeft;
        if (to - parent.scrollLeft < 0) {
        options.left = to;
      } else if (
        to + dom.offsetWidth - parent.scrollLeft >
        parent.offsetWidth
        ) {
          options.left = to + dom.offsetWidth - parent.offsetWidth;
        }
      }
    } else {
      if (start) {
        options.top = dom.offsetTop;
      } else {
        const to = dom.offsetTop - parent.offsetTop;
        if (to - parent.scrollTop < 0) {
          options.top = to;
        } else if (
          to + dom.offsetHeight - parent.scrollTop >
          parent.offsetHeight
          ) {
            options.top = to + dom.offsetHeight - parent.offsetHeight;
          }
      }
    }

    if (options.left || options.top) {
      parent.scrollTo(options);
    }
  }
}

export function scrollTo(target, options: ScrollToOptions) {
  let dom = getDom(target);
  if (dom instanceof HTMLElement) {
    const o = {
      left: options.left === null ? undefined : options.left,
      top: options.top === null ? undefined : options.top,
      behavior: options.behavior
    }
    dom.scrollTo(o)
  }
}

export function scrollToTarget(
  target: string,
  container: string = null,
  offset: number = 0
) {
  const targetEl: HTMLElement = document.querySelector(target);
  if (targetEl) {
    let top ;
    if (container) {
      top = targetEl.offsetTop;
    } else {
      top = targetEl.getBoundingClientRect().top + window.scrollY;
    }
    const containerEl = container
      ? document.querySelector(container)
      : document.documentElement
    containerEl.scrollTo({
      top: top - offset,
      behavior: "smooth",
    });
  }
}

export function scrollToElement(target, offset: number, behavior?: ScrollBehavior) {
  const dom = getDom(target)
  if (!dom) return;
  const domPosition = dom.getBoundingClientRect().top;
  const offsetPosition = domPosition + window.pageYOffset - offset;
  window.scrollTo({
    top: offsetPosition,
    behavior: behavior
  })
}

export function scrollToActiveElement(
  container,
  element = ".active",
  position: "center" | number = "center"
) {
  let containerEl: HTMLElement = getDom(container);

  let activeEl: HTMLElement
  if (typeof element === 'string') {
    activeEl =  container.querySelector(element)
  }

  if (!containerEl || !activeEl) {
    return;
  }

  if (position === 'center') {
    containerEl.scrollTop = activeEl.offsetTop - containerEl.offsetHeight / 2 + activeEl.offsetHeight / 2;
  }
  else {
    containerEl.scrollTop = activeEl.offsetTop - position
  }
}

export function addClsToFirstChild(element, className) {
  var dom = getDom(element);
  if (dom.firstElementChild) {
    dom.firstElementChild.classList.add(className);
  }
}

export function removeClsFromFirstChild(element, className) {
  var dom = getDom(element);
  if (dom.firstElementChild) {
    dom.firstElementChild.classList.remove(className);
  }
}

export function getAbsoluteTop(e) {
  var offset = e.offsetTop;
  if (e.offsetParent != null) {
    offset += getAbsoluteTop(e.offsetParent);
  }
  return offset;
}

export function getAbsoluteLeft(e) {
  var offset = e.offsetLeft;
  if (e.offsetParent != null) {
    offset += getAbsoluteLeft(e.offsetParent);
  }
  return offset;
}

export function addElementToBody(element) {
  document.body.appendChild(element);
}

export function delElementFromBody(element) {
  document.body.removeChild(element);
}

export function addElementTo(addElement, elementSelector) {
  let parent = getDom(elementSelector);
  if (parent && addElement) {
    parent.appendChild(addElement);
  }
}

export function delElementFrom(delElement, elementSelector) {
  let parent = getDom(elementSelector);
  if (parent && delElement) {
    parent.removeChild(delElement);
  }
}

export function getActiveElement() {
  let element = document.activeElement;
  let id = element.getAttribute("id") || "";
  return id;
}

export function focusDialog(selector: string, count: number = 0) {
  let ele = <HTMLElement>document.querySelector(selector);
  if (ele && !ele.hasAttribute("disabled")) {
    setTimeout(() => {
      ele.focus();
      let curId = "#" + getActiveElement();
      if (curId !== selector) {
        if (count < 10) {
          focusDialog(selector, count + 1);
        }
      }
    }, 10);
  }
}

export function getWindow() {
  return {
    innerWidth: window.innerWidth,
    innerHeight: window.innerHeight,
    pageXOffset: window.pageXOffset,
    pageYOffset: window.pageYOffset,
    isTop: window.scrollY == 0,
    isBottom: (window.scrollY + window.innerHeight) == document.body.clientHeight
  };
}

export function getWindowAndDocumentProps(windowProps: string[] = [], documentProps: string[] = []) {
  const obj = {}

  if (windowProps) {
    windowProps.forEach(prop => obj[prop] = window[prop]);
    obj['pageYOffset'] = getPageYOffset();
  }

  if (documentProps) {
    documentProps.forEach(prop => obj[prop] = document.documentElement[prop]);
  }

  return obj
}

function debounce(func, wait, immediate) {
  var timeout;
  return () => {
    const context = this, args = arguments;
    const later = () => {
      timeout = null;
      if (!immediate) func.apply(this, args);
    };
    const callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
    if (callNow) func.apply(context, args);
  };
};

export function css(element: any, name: string | object, value: string | null = null) {
  var dom = getDom(element);
  if (typeof name === 'string') {
    dom.style[name] = value;
  } else {
    for (let key in name) {
      if (name.hasOwnProperty(key)) {
        dom.style[key] = name[key];
      }
    }
  }
}

export function addCls(selector: Element | string, clsName: string | Array<string>) {
  let element = getDom(selector);

  if (typeof clsName === "string") {
    element.classList.add(clsName);
  } else {
    element.classList.add(...clsName);
  }
}

export function removeCls(selector: Element | string, clsName: string | Array<string>) {
  let element = getDom(selector);

  if (typeof clsName === "string") {
    element.classList.remove(clsName);
  } else {
    element.classList.remove(...clsName);
  }
}

export function elementScrollIntoView(selector: Element | string) {
  let element = getDom(selector);

  if (!element)
    return;

  element.scrollIntoView({ behavior: 'smooth', block: 'nearest', inline: 'start' });
}

const hasScrollbar = () => {
  let overflow = document.body.style.overflow;
  if (overflow && overflow === "hidden") return false;
  return document.body.scrollHeight > (window.innerHeight || document.documentElement.clientHeight);
}

export function getScroll() {
  return { x: window.pageXOffset, y: window.pageYOffset };
}

function isElement(node: Element) {
  const ELEMENT_NODE_TYPE = 1;
  return (
    node.tagName !== "HTML" &&
    node.tagName !== "BODY" &&
    node.nodeType == ELEMENT_NODE_TYPE
  )
}

export function getScrollParent(el: Element | undefined, root: HTMLElement | Window | undefined = undefined) {
  root ??= canUseDom ? window : undefined;

  let node = el;
  while (node && node !== root && isElement(node)) {
    const { overflowY } = window.getComputedStyle(node);
    if (/scroll|auto|overlay/i.test(overflowY)) {
      return node;
    }

    node = node.parentNode as Element;
  }

  return root;
}

export function getScrollTop(el: HTMLElement | Window): number {
  const top = 'scrollTop' in el ? el.scrollTop : el.pageYOffset;

  // iOS scroll bounce cause minus scrollTop
  return Math.max(top, 0);
}

export function getInnerText(element) {
  let dom = getDom(element);
  return dom.innerText;
}

export function getMenuOrDialogMaxZIndex(exclude: Element[] = [], element: Element) {
  const base = getDom(element);
  // Start with lowest allowed z-index or z-index of
  // base component's element, whichever is greater
  const zis = [getZIndex(base)]

  const activeElements = [
    ...document.getElementsByClassName('m-menu__content--active'),
    ...document.getElementsByClassName('m-dialog__content--active'),
  ]

  // Get z-index for all active dialogs
  for (let index = 0; index < activeElements.length; index++) {
    if (!exclude.includes(activeElements[index])) {
      zis.push(getZIndex(activeElements[index]))
    }
  }

  return Math.max(...zis)
}

export function getMaxZIndex() {
  return [...document.all].reduce((r, e) => Math.max(r, +window.getComputedStyle(e).zIndex || 0), 0)
}

export function getStyle(element, styleProp) {
  element = getDom(element);

  if (element.currentStyle) {
    return element.currentStyle[styleProp];
  } else if (window.getComputedStyle) {
    return document.defaultView.getComputedStyle(element, null).getPropertyValue(styleProp);
  }
}

export function getTextAreaInfo(element) {
  var result = {};
  var dom = getDom(element);
  result["scrollHeight"] = dom.scrollHeight || 0;

  if (element.currentStyle) {
    result["lineHeight"] = parseFloat(element.currentStyle["line-height"]);
    result["paddingTop"] = parseFloat(element.currentStyle["padding-top"]);
    result["paddingBottom"] = parseFloat(element.currentStyle["padding-bottom"]);
    result["borderBottom"] = parseFloat(element.currentStyle["border-bottom"]);
    result["borderTop"] = parseFloat(element.currentStyle["border-top"]);
  } else if (window.getComputedStyle) {
    result["lineHeight"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("line-height"));
    result["paddingTop"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("padding-top"));
    result["paddingBottom"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("padding-bottom"));
    result["borderBottom"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("border-bottom"));
    result["borderTop"] = parseFloat(document.defaultView.getComputedStyle(element, null).getPropertyValue("border-top"));
  }
  //Firefox can return this as NaN, so it has to be handled here like that.
  if (Object.is(NaN, result["borderTop"]))
    result["borderTop"] = 1;
  if (Object.is(NaN, result["borderBottom"]))
    result["borderBottom"] = 1;
  return result;
}

const objReferenceDict = {};

export function disposeObj(objReferenceName) {
  delete objReferenceDict[objReferenceName];
}

export function upsertThemeStyle(id: string, style: string) {
  const d = document.getElementById(id);
  if (d) {
    document.head.removeChild(d);
  }

  const d_style = document.createElement('style')
  d_style.id = id;
  d_style.type = "text/css";
  d_style.innerHTML = style;

  document.head.insertAdjacentElement('beforeend', d_style)
}

export function getImageDimensions(src: string) {
  return new Promise(function (resolve, reject) {
    var img = new Image()
    img.src = src
    img.onload = function () {
      resolve({
        width: img.width,
        height: img.height,
        hasError: false
      })
    }
    img.onerror = function () {
      resolve({
        width: 0,
        height: 0,
        hasError: true
      })
    }
  })
}

export function enablePreventDefaultForEvent(element: any, event: string, condition?: any) {
  const dom = getDom(element);
  if (!dom) return;
  if (event === 'keydown') {
    dom.addEventListener(event, (e: KeyboardEvent) => {
      if (Array.isArray(condition)) {
        var codes = condition as string[];
        if (codes.includes(e.code)) {
          e.preventDefault();
        }
      } else {
        e.preventDefault();
      }
    })
  } else {
    dom.addEventListener(event, e => {
      if (e.preventDefault) {
        e.preventDefault();
      }
    })
  }
}

export function getBoundingClientRects(selector) {
  var elements = document.querySelectorAll(selector);

  var result = [];

  for (var i = 0; i < elements.length; i++) {
    var e: Element = elements[i];
    var dom = {
      id: e.id,
      rect: e.getBoundingClientRect()
    };
    result.push(dom);
  }

  return result;
}

export function getSize(selectors, sizeProp) {
  var el = getDom(selectors);

  var display = el.style.display;
  var overflow = el.style.overflow;

  el.style.display = "";
  el.style.overflow = "hidden";

  var size = el["offset" + sizeProp.charAt(0).toUpperCase() + sizeProp.slice(1)] || 0;

  el.style.display = display;
  el.style.overflow = overflow;

  return size;
}

export function getProp(elOrString, name) {
  if (elOrString === 'window') {
    return window[name];
  }

  var el = getDom(elOrString);
  if (!el) {
    return null;
  }

  return el[name];
}

export function updateWindowTransition(selectors, isActive, item) {
  var el: HTMLElement = getDom(selectors);
  var container: HTMLElement = el.querySelector('.m-window__container');

  if (item) {
    var itemEl: HTMLElement = getDom(item);
    container.style.height = itemEl.clientHeight + 'px';
    return;
  }

  if (isActive) {
    container.classList.add('m-window__container--is-active');
    container.style.height = el.clientHeight + 'px';
  } else {
    container.style.height = '';
    container.classList.remove('m-window__container--is-active');
  }
}

export function getScrollHeightWithoutHeight(elOrString) {
  var el: HTMLElement = getDom(elOrString);
  if (!el) {
    return 0;
  }

  var height = el.style.height;
  el.style.height = '0'
  var scrollHeight = el.scrollHeight;
  el.style.height = height;

  return scrollHeight;
}

function registerPasteWithData(customEventName) {
  if (Blazor) {
    Blazor.registerCustomEventType(customEventName, {
      browserEventName: 'paste',
      createEventArgs: (event: ClipboardEvent) => {
        return {
          type: event.type,
          pastedData: event.clipboardData.getData('text')
        };
      }
    });
  }
}

export function registerTextFieldOnMouseDown(element, inputElement, callback) {
  if (!element || !inputElement) return

  const listener = (e: MouseEvent) => {
    const target = e.target;
    const inputDom = getDom(inputElement);
    if (target !== inputDom) {
      e.preventDefault();
      e.stopPropagation();
    }

    if (callback) {
      const mouseEventArgs = {
        Detail: e.detail,
        ScreenX: e.screenX,
        ScreenY: e.screenY,
        ClientX: e.clientX,
        ClientY: e.clientY,
        OffsetX: e.offsetX,
        OffsetY: e.offsetY,
        PageX: e.pageX,
        PageY: e.pageY,
        Button: e.button,
        Buttons: e.buttons,
        CtrlKey: e.ctrlKey,
        ShiftKey: e.shiftKey,
        AltKey: e.altKey,
        MetaKey: e.metaKey,
        Type: e.type
      }

      callback.invokeMethodAsync('Invoke', mouseEventArgs);
    }
  };

  element.addEventListener('mousedown', listener)

  const config: HtmlElementEventListenerConfig = {
    listener,
    handle: callback
  };

  const key =`registerTextFieldOnMouseDown_${getBlazorId(element)}`;
  htmlElementEventListenerConfigs[key] = [config]
}

export function unregisterTextFieldOnMouseDown(element: HTMLElement) {
  const key =`registerTextFieldOnMouseDown_${getBlazorId(element)}`;
  const configs = htmlElementEventListenerConfigs[key]
  if (configs && configs.length) {
    configs.forEach(item => {
      item.handle.dispose();
      if (element) {
        element.removeEventListener("mousedown", item.listener);
      }
    })
  }
}

export function containsActiveElement(selector) {
  var el = getDom(selector);
  if (el && el.contains) {
    return el.contains(document.activeElement);
  }

  return null;
}

export function copyChild(el) {
  if (typeof el === 'string') {
    el = document.querySelector(el);
  }

  if (!el) return;

  el.setAttribute('contenteditable', 'true');
  el.focus();
  document.execCommand('selectAll', false, null);
  document.execCommand('copy');
  document.execCommand('unselect');
  el.blur();
  el.removeAttribute('contenteditable');
}

export function copyText(text) {
  if (!navigator.clipboard) {
    var textArea = document.createElement("textarea");
    textArea.value = text;
    textArea.readOnly = true;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
      var successful = document.execCommand('copy');
      var msg = successful ? 'successful' : 'unsuccessful';
      console.log('Fallback: Copying text command was ' + msg);
    } catch (err) {
      console.error('Fallback: Oops, unable to copy', err);
    }

    document.body.removeChild(textArea);
    return;
  }

  navigator.clipboard.writeText(text).then(function () {
    console.log('Async: Copying to clipboard was successful!');
  }, function (err) {
    console.error('Async: Could not copy text: ', err);
  });
}

export function getMenuableDimensions(hasActivator, activatorSelector, isDefaultAttach, contentElement, attached, attachSelector) {
  if (!attached) {
    var container = document.querySelector(attachSelector);
    if (contentElement.nodeType) {
      container.appendChild(contentElement);
    }
  }

  var dimensions = {
    activator: {} as any,
    content: {},
    relativeYOffset: 0,
    offsetParentLeft: 0
  };

  if (hasActivator) {
    var activator = document.querySelector(activatorSelector);
    dimensions.activator = measure(activator, isDefaultAttach)
    dimensions.activator.offsetLeft = activator.offsetLeft
    if (!isDefaultAttach) {
      // account for css padding causing things to not line up
      // this is mostly for v-autocomplete, hopefully it won't break anything
      dimensions.activator.offsetTop = activator.offsetTop
    } else {
      dimensions.activator.offsetTop = 0
    }
  }

  sneakPeek(() => {
    if (contentElement) {
      if (contentElement.offsetParent) {
        const offsetRect = getRoundedBoundedClientRect(contentElement.offsetParent)
        dimensions.relativeYOffset = getPageYOffset() + offsetRect.top

        if (hasActivator) {
          dimensions.activator.top -= dimensions.relativeYOffset
          dimensions.activator.left -= window.pageXOffset + offsetRect.left
        } else {
          dimensions.offsetParentLeft = offsetRect.left
        }
      }

      dimensions.content = measure(contentElement, isDefaultAttach)
    }
  }, contentElement);

  return dimensions;
}

function getPageYOffset() {
  let pageYOffset = window.pageYOffset
  const blockedScrollY = parseInt(document.documentElement.style.getPropertyValue('--m-body-scroll-y'))
  if (blockedScrollY) {
    pageYOffset += Math.abs(blockedScrollY);
  }
  return pageYOffset
}

function measure(el: HTMLElement, isDefaultAttach) {
  if (!el) return {}

  const rect = getRoundedBoundedClientRect(el)

  // Account for activator margin
  if (!isDefaultAttach) {
    const style = window.getComputedStyle(el)

    rect.left = parseInt(style.marginLeft!)
    rect.top = parseInt(style.marginTop!)
  }

  return rect
}

function getRoundedBoundedClientRect(el: Element) {
  if (!el || !el.nodeType) {
    return null
  }

  const rect = el.getBoundingClientRect()
  return {
    top: Math.round(rect.top),
    left: Math.round(rect.left),
    bottom: Math.round(rect.bottom),
    right: Math.round(rect.right),
    width: Math.round(rect.width),
    height: Math.round(rect.height),
  }
}

function sneakPeek(cb: () => void, el) {
  if (!el || !el.style || el.style.display !== 'none') {
    cb()
    return
  }

  el.style.display = 'inline-block'
  cb()
  el.style.display = 'none'
}

export function invokeMultipleMethod(windowProps, documentProps, hasActivator, activatorSelector, attach, contentElement, attached, attachSelector, element) {
  var multipleResult = {
    windowAndDocument: null,
    dimensions: null,
    zIndex: 0
  };

  multipleResult.windowAndDocument = getWindowAndDocumentProps(windowProps, documentProps);
  multipleResult.dimensions = getMenuableDimensions(hasActivator, activatorSelector, attach, contentElement, attached, attachSelector);
  multipleResult.zIndex = getMenuOrDialogMaxZIndex([contentElement], element);

  return multipleResult;
}

export function registerOTPInputOnInputEvent(elementList, callback) {
  for (let i = 0; i < elementList.length; i++) {
    const inputListener = (e: Event) => otpInputOnInputEvent(e, i, elementList, callback);
    const focusListener = (e: Event) => otpInputFocusEvent(e, i, elementList);
    const keyupListener =(e: KeyboardEvent) => otpInputKeyupEvent(e, i, elementList, callback);

    elementList[i].addEventListener('input', inputListener);
    elementList[i].addEventListener('focus', focusListener);
    elementList[i].addEventListener('keyup', keyupListener);

    elementList[i]._optInput = {
      inputListener,
      focusListener,
      keyupListener
    }
  }
}

function otpInputKeyupEvent(e: KeyboardEvent, otpIdx: number, elementList, callback) {
  e.preventDefault();
  const eventKey = e.key;
  if (eventKey === 'ArrowLeft' || eventKey === 'Backspace') {
    if (eventKey === 'Backspace') {
      const obj = {
        type: eventKey,
        index: otpIdx,
        value: ''
      }
      if (callback) {
        callback.invokeMethodAsync('Invoke', obj);
      }
    }
    otpInputFocus(otpIdx - 1, elementList);
  }
  else if (eventKey === 'ArrowRight') {
    otpInputFocus(otpIdx + 1, elementList);
  }
}

function otpInputFocus(focusIndex: number, elementList) {
  if (focusIndex < 0) {
    otpInputFocus(0, elementList);
  }
  else if (focusIndex >= elementList.length) {
    otpInputFocus(elementList.length - 1, elementList);
  }
  else {
    if (document.activeElement !== elementList[focusIndex]) {
      const element = getDom(elementList[focusIndex])
      element.focus();
    }
  }
}

function otpInputFocusEvent(e: Event, otpIdx: number, elementList) {
  const element = getDom(elementList[otpIdx]) as HTMLInputElement;
  if (element && document.activeElement === element) {
    element.select();
  }
}

function otpInputOnInputEvent(e: Event, otpIdx: number, elementList, callback) {
  const target = e.target as HTMLInputElement;
  const value = target.value;

  if (value && value !== '') {
    otpInputFocus(otpIdx + 1, elementList);

    if (callback) {
      const obj = {
        type: 'Input',
        index: otpIdx,
        value: value
      }
      callback.invokeMethodAsync('Invoke', obj);
    }
  }
}

export function unregisterOTPInputOnInputEvent(elementList) {
  for (let i = 0; i < elementList.length; i++) {
    const el = elementList[i]
    if(el && el._optInput) {
      el.removeEventListener('input', el._optInput.inputListener)
      el.removeEventListener('focus', el._optInput.focusListener)
      el.removeEventListener('keyup', el._optInput.keyupListener)
    }
  }
}

export function getListIndexWhereAttributeExists(selector: string, attribute:string, value: string) {
  const tiles = document.querySelectorAll(selector);
  if (!tiles) {
    return -1;
  }

  let index = -1;
  for (let i = 0; i < tiles.length; i++) {
    if (tiles[i].getAttribute(attribute) === value) {
      index = i;
      break;
    }
  }

  return index;
}

export function scrollToTile(contentSelector: string, tilesSelector: string, index: number, keyCode: string) {
  var tiles = document.querySelectorAll(tilesSelector)
  if (!tiles) return;

  let tile = tiles[index] as HTMLElement;

  if (!tile) return;

  const content = document.querySelector(contentSelector);
  if (!content) return;

  const scrollTop = content.scrollTop;
  const contentHeight = content.clientHeight;

  if (scrollTop > tile.offsetTop - 8) {
    content.scrollTo({ top: tile.offsetTop - tile.clientHeight, behavior: "smooth" })
  } else if (scrollTop + contentHeight < tile.offsetTop + tile.clientHeight + 8) {
    content.scrollTo({ top: tile.offsetTop - contentHeight + tile.clientHeight * 2, behavior: "smooth" })
  }
}

export function getElementTranslateY(element) {
  const style = window.getComputedStyle(element);
  const transform = style.transform || style.webkitTransform;
  const translateY = transform.slice(7, transform.length - 1).split(', ')[5];

  return Number(translateY);
}

function isWindow(element: any | Window): element is Window {
  return element === window
}

export function checkIfThresholdIsExceededWhenScrolling(el: Element, parent: any, threshold: number) {
  if (!el || !parent) return

  let parentElement: HTMLElement | Window

  if (parent == "window") {
    parentElement = window;
  } else if (parent == "document") {
    parentElement = document.documentElement;
  } else {
    parentElement = document.querySelector(parent);
  }

  const rect = el.getBoundingClientRect();
  const elementTop = rect.top;
  const current = isWindow(parentElement)
    ? window.innerHeight
    : parentElement.getBoundingClientRect().bottom

  return (current >= elementTop - threshold)
}

export function get_top_domain() {
  var i, h,
    weird_cookie = 'weird_get_top_level_domain=cookie',
    hostname = document.location.hostname.split('.');
  for (i = hostname.length - 1; i >= 0; i--) {
    h = hostname.slice(i).join('.');
    document.cookie = weird_cookie + ';domain=.' + h + ';';
    if (document.cookie.indexOf(weird_cookie) > -1) {
        // We were able to store a cookie! This must be it
        document.cookie = weird_cookie.split('=')[0] + '=;domain=.' + h + ';expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        return h;
    }
  }
}

export function setCookie(name, value) {
  if (value === null || value === undefined) {
    return;
  }

  var domain = get_top_domain();
  if (!domain) {
    domain = '';
  }else if (isNaN(domain[0])) {
    domain = `.${domain}`;
  }
  var Days = 30;
  var exp = new Date();
  exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
  document.cookie = `${name}=${escape(value?.toString())};path=/;expires=${exp.toUTCString()};domain=${domain}`;
}

export function getCookie(name) {
  const reg = new RegExp(`(^| )${name}=([^;]*)(;|$)`);
  const arr = document.cookie.match(reg);
  if (arr) {
    return unescape(arr[2]);
  }
  return null;
}

export function registerDragEvent(el: HTMLElement, dataKey?: string) {
  if (el) {
    const blazorId = getBlazorId(el);
    const listener = (e: DragEvent) => {
      if (dataKey) {
        const dataValue = (e.target as HTMLElement).getAttribute(dataKey);
        e.dataTransfer.setData(dataKey, dataValue);
        e.dataTransfer.setData('offsetX', e.offsetX.toString())
        e.dataTransfer.setData('offsetY', e.offsetY.toString())
      }
    };
    const key = `${blazorId}:dragstart`;
    htmlElementEventListenerConfigs[key] = [{
      listener
    }];
    el.addEventListener("dragstart", listener);
  }
}

export function unregisterDragEvent(el: HTMLElement) {
  const blazorId = getBlazorId(el);
  if (blazorId) {
    const key = `${blazorId}:dragstart`;
    if (htmlElementEventListenerConfigs[key]) {
      htmlElementEventListenerConfigs[key].forEach((config) => {
        el.removeEventListener("dragstart", config.listener);
      });
    }
  }
}

export function resizableDataTable(dataTable: HTMLElement) {
  const table = dataTable.querySelector('table')
  const row = table.querySelector('.m-data-table-header').getElementsByTagName('tr')[0];
  const cols = row ? row.children : [];
  if (!cols) return;

  table.style.overflow = 'hidden';

  const tableHeight = table.offsetHeight;

  for (var i = 0; i < cols.length; i++) {
    const col: any = cols[i];
    const colResizeDiv: HTMLDivElement = col.querySelector(".m-data-table-header__col-resize");
    if (!colResizeDiv) continue
    colResizeDiv.style.height = tableHeight + "px"

    let minWidth = (col.firstElementChild as HTMLElement).offsetWidth; // width of span
    minWidth = minWidth + 32 + 18 + 1 + 1; // 32:padding 18:sort
    if(!col.style.minWidth){
      col.minWidth = minWidth;
      col.style.minWidth = minWidth + "px";
    }

    setListeners(colResizeDiv);
  }

  function setListeners(div: HTMLDivElement) {
    let pageX:number
    let curCol: HTMLElement;
    let nxtCol: HTMLElement;
    let curColWidth: number;
    let nxtColWidth: number;
    let tableWidth: number;

    div.addEventListener('click', e => e.stopPropagation());

    div.addEventListener('mousedown', function (e) {
      curCol = (e.target as HTMLElement).parentElement;
      nxtCol = curCol.nextElementSibling as HTMLElement;
      pageX = e.pageX;

      tableWidth = table.offsetWidth;

      var padding = paddingDiff(curCol);

      curColWidth = curCol.offsetWidth - padding;
      if (nxtCol)
        nxtColWidth = nxtCol.offsetWidth - padding;
    });

    document.addEventListener("mousemove", function (e) {
      if (curCol) {
        let diffX = e.pageX - pageX;

        const isRtl = dataTable.classList.contains("m-data-table--rtl")
        if (isRtl) {
          diffX = 0 - diffX;
        }

        let newCurColWidth = curColWidth + diffX;

        curCol.style.width = newCurColWidth + "px";

        const isOverflow = dataTable.classList.contains(
          "m-data-table--resizable-overflow"
        );
        if (isOverflow) {
          table.style.width = tableWidth + diffX + "px";
          return;
        }

        const isIndependent = dataTable.classList.contains(
          "m-data-table--resizable-independent"
        );
        if (isIndependent) {
          let newNextColWidth = nxtColWidth - diffX;
          const twoColWidth = curColWidth + nxtColWidth;

          if (diffX > 0) {
            if (nxtCol) {
              if (newNextColWidth < nxtCol["minWidth"]) {
                newNextColWidth = nxtCol["minWidth"];
                newCurColWidth = twoColWidth - newNextColWidth;
              }
            }
          } else {
            if (newCurColWidth < curCol["minWidth"]) {
              newCurColWidth = curCol["minWidth"];
              newNextColWidth = twoColWidth - newCurColWidth;
            }
          }

          curCol.style.width = newCurColWidth + "px";

          if (nxtCol) {
            nxtCol.style.width = newNextColWidth + "px";
          }
        }
      }
    });

    document.addEventListener('mouseup', function (e) {
      if (curCol) {
        for (let i = 0; i < cols.length; i++) {
          const col:any = cols[i];
          col.style.width = col['offsetWidth'] + "px"
        }
      }
      curCol = undefined;
      nxtCol = undefined;
      pageX = undefined;
      nxtColWidth = undefined;
      curColWidth = undefined;
      tableWidth = undefined;
    });
  }

  function paddingDiff(col) {
    if (getStyleVal(col, 'box-sizing') == 'border-box') {
      return 0;
    }

    var padLeft = getStyleVal(col, 'padding-left');
    var padRight = getStyleVal(col, 'padding-right');
    return (parseInt(padLeft) + parseInt(padRight));
  }

  function getStyleVal(elm, css) {
    return (window.getComputedStyle(elm, null).getPropertyValue(css))
  }
}

export function updateDataTableResizeHeight(dataTable: HTMLElement) {
  const table = dataTable.querySelector('table')
  const row = table.querySelector('.m-data-table-header').getElementsByTagName('tr')[0];
  const cols = row ? row.children : [];
  if (!cols) return;

  const tableHeight = table.offsetHeight;

  for (var i = 0; i < cols.length; i++) {
    const col: any = cols[i];
    const colResizeDiv: HTMLDivElement = col.querySelector(".m-data-table-header__col-resize");
    colResizeDiv.style.height = tableHeight + "px"
  }
}

function stopPropagation(e) {
  e.stopPropagation();
}

export function addStopPropagationEvent(el: any, type: keyof HTMLElementEventMap) {
  const dom = getDom(el);
  dom.addEventListener(type, stopPropagation);
}

export function removeStopPropagationEvent(el: any, type: keyof HTMLElementEventMap) {
  const dom = getDom(el);
  dom.removeEventListener(type, stopPropagation);
}

export function historyBack() {
  history.back();
}

export function historyGo(delta: number) {
  history.go(delta);
}

export function historyReplace(href) {
  history.replaceState(null, /*ignore title*/ '', href);
}