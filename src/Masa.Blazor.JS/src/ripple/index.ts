import { removeListeners, RippleOptions, updateRipple } from "./ripple";

export default function registerRippleObserver() {
  const observer = new MutationObserver((mutationsList, observer) => {
    for (const mutation of mutationsList) {
      if (mutation.type === "childList" && mutation.addedNodes.length > 0) {
        for (const node of mutation.addedNodes) {
          if (node instanceof HTMLElement) {
            if (
              node.nodeType === Node.ELEMENT_NODE &&
              node.hasAttribute("ripple") &&
              !node._ripple
            ) {
              updateRipple(node, convertRippleAttributeToOptions(node), false);
            }
          }
        }
      }

      // 处理属性变动
      if (mutation.type === "attributes") {
        const target = mutation.target as HTMLElement;
        if (target.hasAttribute("ripple") && !target._ripple) {
          if (mutation.attributeName === "ripple") {
            updateRipple(
              target,
              convertRippleAttributeToOptions(target),
              false
            );
          } else if (!target.hasAttribute("ripple") && target._ripple) {
            removeListeners(target);
            delete target._ripple;
          }
        }
      }

      if (
        mutation.type === "attributes" &&
        mutation.attributeName === "ripple"
      ) {
        const target = mutation.target as HTMLElement;
        if (target._ripple) {
          updateRipple(
            target,
            convertRippleAttributeToOptions(target),
            target._ripple.enabled
          );
        }
      }

      if (mutation.type === "childList" && mutation.removedNodes.length > 0) {
        for (const node of mutation.removedNodes) {
          if (node instanceof HTMLElement) {
            if (node.nodeType === Node.ELEMENT_NODE && node._ripple) {
              removeListeners(node);
              delete node._ripple;
            }
          }
        }
      }
    }
  });

  // ripple="false",
  // ripple="",
  // ripple="true",
  // ripple="center",
  // ripple="circle",
  // ripple="circle&center"
  // ripple="center&custom-css",
  // ripple="circle&custom-css",
  // ripple="custom-css"
  function convertRippleAttributeToOptions(
    target: HTMLElement
  ): RippleOptions | null {
    const value = target.getAttribute("ripple");
    if ((typeof value !== "string" && !value) || value === "false") {
      return null;
    }

    const options: RippleOptions = {};

    const props = value.split("&");
    props.forEach((prop) => {
      if (prop === "center") {
        options.center = true;
      } else if (prop === "circle") {
        options.circle = true;
      } else {
        options.class = prop.trim();
      }
    });

    return options;
  }

  const initialElements = document.querySelectorAll("[ripple]");

  for (const element of initialElements) {
    updateRipple(element, convertRippleAttributeToOptions(element), false);
  }

  observer.observe(document, {
    childList: true,
    subtree: true,
    attributes: true,
    attributeFilter: ["ripple"],
    attributeOldValue: false,
  });
}
