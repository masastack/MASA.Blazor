import { GridItemHTMLElement, GridStack, GridStackElement, GridStackOptions } from "gridstack";

function init(
  options: GridStackOptions = {},
  elOrString: GridStackElement = ".grid-stack",
  dotNet: DotNet.DotNetObject
) {
  let grid = GridStack.init(options, elOrString);
  grid["dotNet"] = dotNet;
  addEvents();

  function addEvents() {
    grid.on("resizestop", function (event: Event, el: GridItemHTMLElement) {
      dotNet.invokeMethodAsync("OnResize", resize(event, el));
    });
  }

  return {
    setStatic: (value: boolean) => grid.setStatic(value),
    reload: () => {
      const opts = { ...grid.opts };
      const el = grid.el;
      grid.destroy(false);
      grid = GridStack.init(opts, el);
      grid["dotNet"] = dotNet;
      addEvents();
      return grid;
    },
    save: () => {
      const widgets = grid.save();
      if (Array.isArray(widgets)) {
        return widgets.map(({ content, ...rest }) => rest);
      }
      return [];
    },
  };
}

function resize(event: Event, el: GridItemHTMLElement) {
  const id = el.getAttribute("gs-id");
  const { x, y, w, h } = el.gridstackNode;
  const { w: width, h: height } = el.gridstackNode["_rect"];
  return { id, x, y, w, h, width, height };
}

export { init };
