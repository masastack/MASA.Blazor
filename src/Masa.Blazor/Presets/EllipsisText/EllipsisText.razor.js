export function observer(el, invoker) {
  const resizeObserver = new ResizeObserver((entries => {
    invoker.invokeMethodAsync('OnEllipsisChange', el.offsetWidth < el.scrollWidth)
  }))

  resizeObserver.observe(el);

  return resizeObserver;
}