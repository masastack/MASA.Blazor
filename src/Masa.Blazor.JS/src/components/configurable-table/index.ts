class ResizableDataTable {
  private dataTable: HTMLElement;
  private dotnetHelper: DotNet.DotNetObject;
  private table: HTMLTableElement;
  private wrapper: HTMLDivElement;
  private header: HTMLTableRowElement;
  private pageX: number;
  private curCol: HTMLElement;
  private nxtCol: HTMLElement;
  private curColWidth: number;
  private nxtColWidth: number;
  private tableWidth: number;
  private documentEventRegistered: boolean;

  constructor(dataTable: HTMLElement, dotNETHelper: DotNet.DotNetObject) {
    this.dataTable = dataTable;
    this.dotnetHelper = dotNETHelper;
    this.wrapper = this.dataTable.querySelector(".m-data-table__wrapper");
    console.log('this.wrapper', this.wrapper)
    this.table = this.dataTable.querySelector("table");
    this.header = this.table.querySelector("thead").getElementsByTagName("tr")[0];
    this.documentEventRegistered = false;

    this.init();
  }

  private init() {
    if (!this.documentEventRegistered) {
      document.addEventListener("mousemove", this.mousemove.bind(this));
      document.addEventListener("mouseup", this.mouseup.bind(this));
      this.documentEventRegistered = true;
    }

    this.header.addEventListener("mousedown", this.wrapperMousedown.bind(this));
    this.wrapper.addEventListener('scroll', this.scroll.bind(this));
  }

  private paddingDiff(col: HTMLElement): number {
    if (this.getStyleVal(col, "box-sizing") == "border-box") {
      return 0;
    }

    const padLeft = this.getStyleVal(col, "padding-left");
    const padRight = this.getStyleVal(col, "padding-right");
    return parseInt(padLeft) + parseInt(padRight);
  }

  private getStyleVal(elm: HTMLElement, css: string): string {
    return window.getComputedStyle(elm, null).getPropertyValue(css);
  }

  private wrapperMousedown(e: MouseEvent) {
    if (e.target instanceof HTMLElement && e.target.classList.contains("masa-table-viewer__header-column-resize")) {
      this.mousedown(e);
    }
  }

  private scroll(e: Event) {
    const scrollWidth = this.wrapper.scrollWidth;
    const clientWidth = this.wrapper.clientWidth;
    const scrollLeft = this.wrapper.scrollLeft;

    const rtl = this.wrapper.parentElement.classList.contains('m-data-table--rtl');

    if (Math.abs(scrollWidth -((rtl ? -scrollLeft : scrollLeft) + clientWidth)) < 1) {
      this.wrapper.classList.remove('scrolling')
      this.wrapper.classList.remove('scrolled-to-left')
      this.wrapper.classList.add('scrolled-to-right');
    } else if (Math.abs(scrollLeft - (rtl ? scrollWidth - clientWidth : 0)) < 1) {
      this.wrapper.classList.remove('scrolling')
      this.wrapper.classList.remove('scrolled-to-right')
      this.wrapper.classList.add('scrolled-to-left');
    } else {
      this.wrapper.classList.remove('scrolled-to-right');
      this.wrapper.classList.remove('scrolled-to-left');
      this.wrapper.classList.add('scrolling');
    }
  }

  private mousedown(e: MouseEvent) {
    this.curCol = (e.target as HTMLElement).parentElement;
    this.nxtCol = this.curCol.nextElementSibling as HTMLElement;
    this.pageX = e.pageX;

    this.tableWidth = this.table.offsetWidth;

    const padding = this.paddingDiff(this.curCol);

    this.curColWidth = this.curCol.offsetWidth - padding;
    if (this.nxtCol) this.nxtColWidth = this.nxtCol.offsetWidth - padding;
  }

  private mousemove(e: MouseEvent) {
    if (this.curCol) {
      let diffX = e.pageX - this.pageX;

      const isRtl = this.dataTable.classList.contains("m-data-table--rtl");
      if (isRtl) {
        diffX = 0 - diffX;
      }

      let newCurColWidth = this.curColWidth + diffX;

      const minWidth = getComputedStyle(this.curCol).minWidth;
      if (minWidth && newCurColWidth < parseInt(minWidth)) {
        newCurColWidth = parseInt(minWidth);
        diffX = newCurColWidth - this.curColWidth;
      }

      const maxWidth = 300;
      if (newCurColWidth > maxWidth) {
        newCurColWidth = maxWidth;
        diffX = newCurColWidth - this.curColWidth;
      }

      const columnId = this.curCol.getAttribute("data-id");
      this.dotnetHelper.invokeMethodAsync("OnColumnWidthResize", columnId, newCurColWidth);

      this.curCol.style.width = newCurColWidth + "px";
      this.table.style.width = this.tableWidth + diffX + "px";
    }
  }

  private mouseup(e: MouseEvent) {
    this.curCol = undefined;
    this.nxtCol = undefined;
    this.pageX = undefined;
    this.nxtColWidth = undefined;
    this.curColWidth = undefined;
    this.tableWidth = undefined;
  }

  public dispose() {
    this.header.removeEventListener("mousedown", this.wrapperMousedown.bind(this));
    this.wrapper.removeEventListener('scroll', this.scroll.bind(this));
    document.removeEventListener("mousemove", this.mousemove.bind(this));
    document.removeEventListener("mouseup", this.mouseup.bind(this));
    this.documentEventRegistered = false;
    this.dotnetHelper.dispose();
  }
}

export function init(  dataTable: HTMLElement,  dotNETHelper: DotNet.DotNetObject) {
  return new ResizableDataTable(dataTable, dotNETHelper);
}
