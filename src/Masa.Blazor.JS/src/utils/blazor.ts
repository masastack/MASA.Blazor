// @event:stopPropagation only works in Blazor,
// so need to capture it manually.
export function checkIfStopPropagationExistsInComposedPath(
  event: Event,
  eventName: string,
  util: EventTarget
) {
  let stopPropagation = false;

  for (const eventTarget of event.composedPath()) {
    if (eventTarget === util) {
      break;
    }

    if (checkIfStopPropagation(eventTarget, eventName)) {
      stopPropagation = true;
      break;
    }
  }

  return stopPropagation;
}

function checkIfStopPropagation(eventTarget: EventTarget, eventName: string) {
  const nestProps = ["_blazorEvents_1", "stopPropagationFlags", eventName];

  let isFlag = eventTarget;
  let i = 0;
  while (isFlag[nestProps[i]]) {
    isFlag = isFlag[nestProps[i]];
    i++;
  }

  return i == nestProps.length && typeof isFlag === "boolean" && isFlag;
}