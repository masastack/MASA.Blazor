import Sortable, { GroupOptions, SortableEvent, SortableOptions } from "sortablejs";

type Options = Omit<SortableOptions, "store" | "group"> & {
  group: {
    name: string;
    pulls?: string[] | undefined;
    puts?: string[] | undefined;
  };
};

class SortableProxy {
  el: HTMLElement;
  handle: DotNet.DotNetObject;
  sortable: Sortable;

  constructor(
    el: HTMLElement,
    options: Options,
    order: string[],
    handle: DotNet.DotNetObject
  ) {
    this.el = el;
    this.handle = handle;

    const { group, ...rest } = options;
    if (!rest.draggable) {
      delete rest.draggable;
    }

    this.sortable = new Sortable(el, {
      ...rest,
      group: group && {
        name: group.name,
        pull: group.pulls,
        put: (to, from, drag) => {
          const toGroup =
            typeof to.options.group === "string"
              ? to.options.group
              : to.options.group.name;
          const fromGroup =
            typeof from.options.group === "string"
              ? from.options.group
              : from.options.group.name;
          const sameGroup = toGroup && fromGroup && toGroup === fromGroup;

          const value = group.puts;

          if (value == null && sameGroup) {
            return true;
          } else if (value == null) {
            return false;
          } else if (to.toArray().includes(drag.getAttribute("data-id"))) {
            console.warn(
              `[MSortable] Group "${
                group.name
              }" already has an item with the [data-id] "${drag.getAttribute(
                "data-id"
              )}", so it can't be added.`
            );
            return false;
          } else {
            return group.pulls.includes(toGroup);
          }
        },
      },
      store: {
        get: (sortable) => {
          return order;
        },
        set: (sortable) => {
          const order = sortable.toArray();
          this.handle.invokeMethodAsync("UpdateOrder", order);
        },
      },
      onAdd: (e) => this._onAdd(e),
      onRemove: (e) => this._onRemove(e),
    });
  }

  _onAdd(event: SortableEvent) {
    this.handle.invokeMethodAsync(
      "HandleOnAdd",
      event.item.getAttribute("data-id"),
      this.sortable.toArray()
    );
  }

  _onRemove(event: SortableEvent) {
    this.handle.invokeMethodAsync(
      "HandleOnRemove",
      event.item.getAttribute("data-id"),
      this.sortable.toArray()
    );
  }

  invokeVoid(prop: string, ...args: any[]) {
    if (this.sortable[prop] && typeof this.sortable[prop] === "function") {
      this.sortable[prop](...args);
    }
  }

  invoke(prop: string, ...args: any[]) {
    if (this.sortable[prop] && typeof this.sortable[prop] === "function") {
      return this.sortable[prop](...args);
    }
  }
}

function init(
  el: string | HTMLElement,
  options: Options,
  order: string[],
  handle: DotNet.DotNetObject
) {
  if (typeof el === "string") {
    const element: HTMLElement = document.querySelector(el);
    return new SortableProxy(element, options, order, handle);
  }

  return new SortableProxy(el, options, order, handle);
}

export { init };
