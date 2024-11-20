import { parseChangeEvent } from "events/EventType";
import { createSharedEventArgs } from "events/extra";
import { checkIfStopPropagationExistsInComposedPath } from "utils/blazor";

class Input {
  input: HTMLInputElement | HTMLTextAreaElement;
  inputSlot: Element;
  dotnetHelper: DotNet.DotNetObject;
  debounce: number;

  constructor(
    dotnetHelper: DotNet.DotNetObject,
    input: HTMLInputElement | HTMLTextAreaElement,
    inputSlot: Element,
    debounce: number
  ) {
    this.input = input;
    this.inputSlot = inputSlot;
    this.dotnetHelper = dotnetHelper;
    this.debounce = debounce;

    this.#registerAllEvents();
  }

  setValue(value: any) {
    this.input.value = value;
  }

  #registerAllEvents() {
    if (!this.input || !this.inputSlot) return;

    this.#registerClickEvent();

    this.#registerMouseUpEvent();

    if (
      !(
        this.input &&
        (this.input instanceof HTMLInputElement ||
          this.input instanceof HTMLTextAreaElement)
      )
    )
      return;

    this.#registerInputEvent();
  }

  #registerInputEvent() {
    let compositionInputting = false;

    let timeout;

    this.input.addEventListener("compositionstart", (_) => {
      compositionInputting = true;
    });

    this.input.addEventListener("compositionend", (event: CompositionEvent) => {
      compositionInputting = false;

      const changeEventArgs = parseChangeEvent(event);
      changeEventArgs.value = this.input.value;

      if (
        this.input.maxLength !== -1 &&
        changeEventArgs.value.length > this.input.maxLength
      ) {
        changeEventArgs.value = changeEventArgs.value.substring(
          0,
          this.input.maxLength
        );
      }

      this.dotnetHelper.invokeMethodAsync("OnInput", changeEventArgs);
    });

    this.input.addEventListener("input", (event: ChangeEvent) => {
      if (compositionInputting) return;

      var changeEventArgs = parseChangeEvent(event);

      clearTimeout(timeout);
      timeout = setTimeout(() => {
        this.dotnetHelper.invokeMethodAsync("OnInput", changeEventArgs);
      }, this.debounce);
    });

    this.input.addEventListener('change', (event: ChangeEvent) => {
      var changeEventArgs = parseChangeEvent(event);
      this.#formatNumberValue(event);

      this.dotnetHelper.invokeMethodAsync("OnChange", changeEventArgs);
    })
  }

  #formatNumberValue(event: ChangeEvent) {
    if (event.target.type === "number") {
      const value = event.target.value;
      const valueAsNumber = event.target.valueAsNumber;
      if (!!value && value !== valueAsNumber.toString()) {
        this.input.value = isNaN(valueAsNumber)
        ? ""
        : valueAsNumber.toString();
      }
    }
  }

  #registerClickEvent() {
    this.inputSlot.addEventListener("click", (e) => {
      if (
        checkIfStopPropagationExistsInComposedPath(e, "click", this.inputSlot)
      ) {
        return;
      }

      var eventArgs = createSharedEventArgs("mouse", e);
      this.dotnetHelper.invokeMethodAsync("OnClick", eventArgs);
    });
  }

  #registerMouseUpEvent() {
    this.inputSlot.addEventListener("mouseup", (e) => {
      if (
        checkIfStopPropagationExistsInComposedPath(e, "mouseup", this.inputSlot)
      ) {
        return;
      }

      var eventArgs = createSharedEventArgs("mouse", e);
      this.dotnetHelper.invokeMethodAsync("OnMouseUp", eventArgs);
    });
  }
}

function init(
  dotnetHelper: DotNet.DotNetObject,
  input: HTMLInputElement | HTMLTextAreaElement,
  inputSlot: Element,
  debounce: number
) {
  return new Input(dotnetHelper, input, inputSlot, debounce);
}

export { init };
