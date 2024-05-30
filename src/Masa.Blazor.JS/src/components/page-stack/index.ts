const dotnetRefs: { [prop: number]: DotNet.DotNetObject } = {};
const clickHandlers: { [prop: number]: (e: MouseEvent) => void } = {};
let nextId = 0;

export function attachListener(handle: DotNet.DotNetObject) {
  const id = nextId;
  const clickHandler = (event: MouseEvent) => onDocumentClick(id, event);
  clickHandlers[id] = clickHandler;
  document.addEventListener("click", clickHandler);
  dotnetRefs[id] = handle;
  return nextId++;
}

async function onDocumentClick(id: number, event: MouseEvent) {
  const dotnet = dotnetRefs[id];
  if (dotnet === null) return;

  const anchor = event.target.closest("a");
  if (!anchor) return;

  const href = anchor.getAttribute("href");
  if (!href) return;

  let strategy = anchor.getAttribute("data-page-stack-strategy");
  strategy = strategy === null ? null : strategy.toLowerCase();
  if (strategy === "" || strategy === "true" || strategy === "push") {
    await dotnet.invokeMethodAsync("Push", href);
  }
}

export function detachListener(id: number) {
  const clickHandler = clickHandlers[id];
  document.removeEventListener("click", clickHandler);

  dotnetRefs[id] && dotnetRefs[id].dispose();

  delete clickHandlers[id];
  delete dotnetRefs[id];
}
