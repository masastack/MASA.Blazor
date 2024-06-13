// see https://github.com/dotnet/aspnetcore/blob/main/src/Components/Web.JS/src/Rendering/Events/EventTypes.ts
// updated at 2022/08/31

export const touchEvents = [
  "touchcancel",
  "touchend",
  "touchmove",
  "touchenter",
  "touchleave",
  "touchstart",
];

export function parseMouseEvent(event: MouseEvent): Blazor.MouseEventArgs {
  return {
    detail: event.detail,
    screenX: event.screenX,
    screenY: event.screenY,
    clientX: event.clientX,
    clientY: event.clientY,
    offsetX: event.offsetX,
    offsetY: event.offsetY,
    pageX: event.pageX,
    pageY: event.pageY,
    button: event.button,
    buttons: event.buttons,
    ctrlKey: event.ctrlKey,
    shiftKey: event.shiftKey,
    altKey: event.altKey,
    metaKey: event.metaKey,
    type: event.type,
  } as Blazor.MouseEventArgs;
}

export function parseTouchEvent(event: TouchEvent): Blazor.TouchEventArgs {
  return {
    detail: event.detail,
    touches: parseTouch(event.touches),
    targetTouches: parseTouch(event.targetTouches),
    changedTouches: parseTouch(event.changedTouches),
    ctrlKey: event.ctrlKey,
    shiftKey: event.shiftKey,
    altKey: event.altKey,
    metaKey: event.metaKey,
    type: event.type,
  };
}

function parseTouch(touchList: TouchList): Blazor.TouchPoint[] {
  const touches: Blazor.TouchPoint[] = [];

  for (let i = 0; i < touchList.length; i++) {
    const touch = touchList[i];
    touches.push({
      identifier: touch.identifier,
      clientX: touch.clientX,
      clientY: touch.clientY,
      screenX: touch.screenX,
      screenY: touch.screenY,
      pageX: touch.pageX,
      pageY: touch.pageY,
    });
  }
  return touches;
}

export function parseChangeEvent(event: Event): Blazor.ChangeEventArgs {
  const element = event.target as Element;
  if (isTimeBasedInput(element)) {
    const normalizedValue = normalizeTimeBasedValue(element);
    return { value: normalizedValue };
  } else if (isMultipleSelectInput(element)) {
    const selectElement = element as HTMLSelectElement;
    const selectedValues = Array.from(selectElement.options)
      .filter((option) => option.selected)
      .map((option) => option.value);
    return { value: selectedValues };
  } else {
    const targetIsCheckbox = isCheckbox(element);
    const newValue = targetIsCheckbox ? !!element["checked"] : element["value"];
    return { value: newValue };
  }
}

export function parseDragEvent(event: DragEvent): Blazor.DragEventArgs {
  return {
    ...parseMouseEvent(event),
    dataTransfer: event.dataTransfer ? {
      dropEffect: event.dataTransfer.dropEffect,
      effectAllowed: event.dataTransfer.effectAllowed,
      files: Array.from(event.dataTransfer.files).map(f => f.name),
      items: Array.from(event.dataTransfer.items).map(i => ({ kind: i.kind, type: i.type })),
      types: event.dataTransfer.types
    } : null,
  };
}

function isTimeBasedInput(element: Element): element is HTMLInputElement {
  return timeBasedInputs.indexOf(element.getAttribute("type")!) !== -1;
}

const timeBasedInputs = ["date", "datetime-local", "month", "time", "week"];

function normalizeTimeBasedValue(element: HTMLInputElement): string {
  const value = element.value;
  const type = element.type;
  switch (type) {
    case "date":
    case "month":
      return value;
    case "datetime-local":
      return value.length === 16 ? value + ":00" : value; // Convert yyyy-MM-ddTHH:mm to yyyy-MM-ddTHH:mm:00
    case "time":
      return value.length === 5 ? value + ":00" : value; // Convert hh:mm to hh:mm:00
    case "week":
      // For now we are not going to normalize input type week as it is not trivial
      return value;
  }

  throw new Error(`Invalid element type '${type}'.`);
}

function isMultipleSelectInput(element: Element): element is HTMLSelectElement {
  return (
    element instanceof HTMLSelectElement && element.type === "select-multiple"
  );
}

function isCheckbox(element: Element | null): boolean {
  return (
    !!element &&
    element.tagName === "INPUT" &&
    element.getAttribute("type") === "checkbox"
  );
}
