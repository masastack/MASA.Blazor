export function beforeStart(options, extensions) {
}

export function afterStarted(blazor) {
  const width = getClientWidth();
  const height = getClientHeight();

  DotNet.invokeMethodAsync("Masa.Blazor", "InitWidthAndHeight", width, height)
}

function getClientWidth() {
  if (typeof document === 'undefined') return 0 // SSR
  return Math.max(document.documentElement.clientWidth, window.innerWidth || 0)
}

function getClientHeight() {
  if (typeof document === 'undefined') return 0 // SSR
  return Math.max(document.documentElement.clientHeight, window.innerHeight || 0)
}