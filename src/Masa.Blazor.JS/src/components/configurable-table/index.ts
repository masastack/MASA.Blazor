export function resizableDataTable(dataTable: HTMLElement, dotNETHelper: DotNet.DotNetObject) {
  const table = dataTable.querySelector("table");
  const header = table.querySelector("thead").getElementsByTagName("tr")[0];

  let pageX: number;
  let curCol: HTMLElement;
  let nxtCol: HTMLElement;
  let curColWidth: number;
  let nxtColWidth: number;
  let tableWidth: number;
  let documentEventRegistered = false;

  var observer = new MutationObserver(function (mutations) {
    mutations.forEach(function (mutation) {
      if (mutation.addedNodes.length) {
        (Array.from(mutation.addedNodes) as HTMLElement[]).forEach((el) => {
          const resizeActivator: HTMLDivElement = el.querySelector(
            ".masa-table-viewer__header-column-resize"
          );
          if (resizeActivator) {
            setListeners(resizeActivator);
          }
        });
      }

      if (mutation.removedNodes.length) {
        (Array.from(mutation.removedNodes) as HTMLElement[]).forEach((el) => {
          const resizeActivator: HTMLDivElement = el.querySelector(
            ".masa-table-viewer__header-column-resize"
          );
          if (resizeActivator) {
            resizeActivator.removeEventListener("mousedown", mousedown);
          }
        });
      }
    });
  });

  header && observer.observe(header, { childList: true });
  // todo: remove observer on dispose

  const cols = header ? header.children : [];
  if (!cols) return;

  for (var i = 0; i < cols.length; i++) {
    const col: any = cols[i];
    const colResizeDiv: HTMLDivElement = col.querySelector(
      ".masa-table-viewer__header-column-resize"
    );
    if (!colResizeDiv) continue;

    setListeners(colResizeDiv);
  }

  function setListeners(div: HTMLDivElement) {
    div.addEventListener("click", (e) => e.stopPropagation());
    div.addEventListener("mousedown", mousedown);

    if (documentEventRegistered === false) {
      document.addEventListener("mousemove", mousemove);
      document.addEventListener("mouseup", mouseup);
    }
  }

  function paddingDiff(col) {
    if (getStyleVal(col, "box-sizing") == "border-box") {
      return 0;
    }

    var padLeft = getStyleVal(col, "padding-left");
    var padRight = getStyleVal(col, "padding-right");
    return parseInt(padLeft) + parseInt(padRight);
  }

  function getStyleVal(elm, css) {
    return window.getComputedStyle(elm, null).getPropertyValue(css);
  }

  function mousedown (e: MouseEvent) {
    curCol = (e.target as HTMLElement).parentElement;
    nxtCol = curCol.nextElementSibling as HTMLElement;
    pageX = e.pageX;

    tableWidth = table.offsetWidth;

    var padding = paddingDiff(curCol);

    curColWidth = curCol.offsetWidth - padding;
    if (nxtCol) nxtColWidth = nxtCol.offsetWidth - padding;
  };

  function mousemove(e: MouseEvent) {
    if (curCol) {
      let diffX = e.pageX - pageX;

      const isRtl = dataTable.classList.contains("m-data-table--rtl");
      if (isRtl) {
        diffX = 0 - diffX;
      }

      let newCurColWidth = curColWidth + diffX;

      const minWidth = getComputedStyle(curCol).minWidth;
      if (minWidth && newCurColWidth < parseInt(minWidth)) {
        newCurColWidth = parseInt(minWidth);
        diffX = newCurColWidth - curColWidth;
      }

      const maxWidth = 300;
      if (newCurColWidth > maxWidth) {
        newCurColWidth = maxWidth;
        diffX = newCurColWidth - curColWidth;
      }

      const columnId = curCol.getAttribute('data-column-id');
      dotNETHelper.invokeMethodAsync("OnColumnWidthResize", columnId, newCurColWidth);

      curCol.style.width = newCurColWidth + "px";
      table.style.width = tableWidth + diffX + "px";
    }
  };

  function mouseup(e: MouseEvent) {
    if (curCol) {
      for (let i = 0; i < cols.length; i++) {
        const col: any = cols[i];
        col.style.width = col["offsetWidth"] + "px";
      }
    }
    curCol = undefined;
    nxtCol = undefined;
    pageX = undefined;
    nxtColWidth = undefined;
    curColWidth = undefined;
    tableWidth = undefined;
  };
}
