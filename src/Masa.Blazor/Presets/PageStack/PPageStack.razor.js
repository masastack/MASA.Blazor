let dotnet;

export function attachListener(handle) {
  dotnet = handle;
  document.addEventListener('click', onDocumentClick);
}

function onDocumentClick(event) {
  if (dotnet === null) return;

  const anchor = event.target.closest('a');
  if (!anchor) return;

  const href = anchor.getAttribute("href");
  if (!href) return;

  let strategy = anchor.getAttribute("data-page-stack-strategy");
  strategy = strategy === null ? null : strategy.toLowerCase();
  console.log('strategy', strategy)
  if (strategy === "" || strategy === "true" || strategy === "push") {
    dotnet.invokeMethodAsync("Push", href);
  }
}

export function detachListener(handle) {
  document.removeEventListener('click', onDocumentClick);
}
