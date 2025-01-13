import { b as __awaiter } from '../../chunks/tslib.es6.js';

var functionThrottle = throttle;

function throttle(fn, interval, options) {
  var timeoutId = null;
  var throttledFn = null;
  var leading = (options && options.leading);
  var trailing = (options && options.trailing);

  if (leading == null) {
    leading = true; // default
  }

  if (trailing == null) {
    trailing = !leading; //default
  }

  if (leading == true) {
    trailing = false; // forced because there should be invocation per call
  }

  var cancel = function() {
    if (timeoutId) {
      clearTimeout(timeoutId);
      timeoutId = null;
    }
  };

  var flush = function() {
    var call = throttledFn;
    cancel();

    if (call) {
      call();
    }
  };

  var throttleWrapper = function() {
    var callNow = leading && !timeoutId;
    var context = this;
    var args = arguments;

    throttledFn = function() {
      return fn.apply(context, args);
    };

    if (!timeoutId) {
      timeoutId = setTimeout(function() {
        timeoutId = null;

        if (trailing) {
          return throttledFn();
        }
      }, interval);
    }

    if (callNow) {
      callNow = false;
      return throttledFn();
    }
  };

  throttleWrapper.cancel = cancel;
  throttleWrapper.flush = flush;

  return throttleWrapper;
}

function observe(el, handle) {
    if (!handle) {
        throw new Error("the handle from .NET cannot be null");
    }
    if (!el) {
        handle.dispose();
        return;
    }
    const throttled = functionThrottle(() => {
        if (!handle)
            return;
        handle.invokeMethodAsync("Invoke");
    }, 300, { trailing: true });
    const observer = new ResizeObserver((entries = []) => __awaiter(this, void 0, void 0, function* () {
        if (!entries.length)
            return;
        throttled();
    }));
    el._resizeObserver = Object(el._resizeObserver);
    el._resizeObserver = { handle, observer };
    observer.observe(el);
}
function unobserve(el) {
    if (!el)
        return;
    if (!el._resizeObserver)
        return;
    el._resizeObserver.observer.unobserve(el);
    el._resizeObserver.handle.dispose();
    delete el._resizeObserver;
}
function observeSelector(selector, handle) {
    if (selector) {
        const el = document.querySelector(selector);
        el && observe(el, handle);
    }
}
function unobserveSelector(selector) {
    if (selector) {
        const el = document.querySelector(selector);
        el && unobserve(el);
    }
}

export { observe, observeSelector, unobserve, unobserveSelector };
//# sourceMappingURL=index.js.map
