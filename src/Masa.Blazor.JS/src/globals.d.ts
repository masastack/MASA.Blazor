export {};

declare global {
  export const Blazor: Blazor;

  interface Blazor {
    registerCustomEventType: (
      eventName: string,
      options: Blazor.EventTypeOptions
    ) => void;
  }

  namespace Blazor {
    interface EventTypeOptions {
      browserEventName?: string;
      createEventArgs?: (event: Event) => unknown;
    }

    // The following interfaces must be kept in sync with the EventArgs C# classes

    interface ChangeEventArgs {
      value: string | boolean | string[];
    }

    interface DragEventArgs {
      detail: number;
      dataTransfer: DataTransferEventArgs | null;
      screenX: number;
      screenY: number;
      clientX: number;
      clientY: number;
      button: number;
      buttons: number;
      ctrlKey: boolean;
      shiftKey: boolean;
      altKey: boolean;
      metaKey: boolean;
    }

    interface DataTransferEventArgs {
      dropEffect: string;
      effectAllowed: string;
      files: readonly string[];
      items: readonly DataTransferItem[];
      types: readonly string[];
    }

    interface DataTransferItem {
      kind: string;
      type: string;
    }

    interface ErrorEventArgs {
      message: string;
      filename: string;
      lineno: number;
      colno: number;
      type: string;

      // omitting 'error' here since we'd have to serialize it, and it's not clear we will want to
      // do that. https://developer.mozilla.org/en-US/docs/Web/API/ErrorEvent
    }

    interface KeyboardEventArgs {
      key: string;
      code: string;
      location: number;
      repeat: boolean;
      ctrlKey: boolean;
      shiftKey: boolean;
      altKey: boolean;
      metaKey: boolean;
      type: string;
    }

    interface MouseEventArgs {
      detail: number;
      screenX: number;
      screenY: number;
      clientX: number;
      clientY: number;
      offsetX: number;
      offsetY: number;
      pageX: number;
      pageY: number;
      movementX: number;
      movementY: number;
      button: number;
      buttons: number;
      ctrlKey: boolean;
      shiftKey: boolean;
      altKey: boolean;
      metaKey: boolean;
      type: string;
    }

    interface PointerEventArgs extends MouseEventArgs {
      pointerId: number;
      width: number;
      height: number;
      pressure: number;
      tiltX: number;
      tiltY: number;
      pointerType: string;
      isPrimary: boolean;
    }

    interface ProgressEventArgs {
      lengthComputable: boolean;
      loaded: number;
      total: number;
      type: string;
    }

    interface TouchEventArgs {
      detail: number;
      touches: TouchPoint[];
      targetTouches: TouchPoint[];
      changedTouches: TouchPoint[];
      ctrlKey: boolean;
      shiftKey: boolean;
      altKey: boolean;
      metaKey: boolean;
      type: string;
    }

    interface TouchPoint {
      identifier: number;
      screenX: number;
      screenY: number;
      clientX: number;
      clientY: number;
      pageX: number;
      pageY: number;
    }

    interface WheelEventArgs extends MouseEventArgs {
      deltaX: number;
      deltaY: number;
      deltaZ: number;
      deltaMode: number;
    }

    interface FocusEventArgs {
      type: string;
    }

    interface ClipboardEventArgs {
      type: string;
    }
  }

  interface ChangeEvent extends InputEvent {
    target: HTMLInputElement;
  }

  interface Element {
    getElementsByClassName(classNames: string): NodeListOf<HTMLElement>;
    querySelectorAll(selector: string): NodeListOf<HTMLElement>;
  }

  interface Document {
    querySelectorAll(selector: string): NodeListOf<HTMLElement>;
  }

  interface HTMLElement {
    _swiper: {
      instance: any;
      handle: DotNet.DotNetObject;
    };
    _resizeObserver: {
      observer: ResizeObserver;
      handle: DotNet.DotNetObject;
    };
    _ripple: {
      enabled?: boolean;
      centered?: boolean;
      class?: string;
      circle?: boolean;
      touched?: boolean;
      isTouch?: boolean;
      showTimer?: number;
      showTimerCommit?: (() => void) | null;
    };
  }

  interface TransitionEvent {
    target: HTMLElement;
  }

  interface MouseEvent {
    target: HTMLElement;
  }

  interface MbEventTarget {
    elementReferenceId?: string;
    selector?: string;
    class?: string;
  }

  namespace DotNet {
    interface DotNetObject {
      dispose(): void;
    }
  }
}
