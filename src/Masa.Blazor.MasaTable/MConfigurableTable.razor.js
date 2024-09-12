function resizableDataTable(dataTable) {
    const table = dataTable.querySelector("table");
    const header = table.querySelector("thead").getElementsByTagName("tr")[0];
    const cols = header ? header.children : [];
    if (!cols)
        return;
    for (var i = 0; i < cols.length; i++) {
        const col = cols[i];
        const colResizeDiv = col.querySelector(".masa-table-viewer__header-column-resize");
        if (!colResizeDiv)
            continue;
        setListeners(colResizeDiv);
    }
    function setListeners(div) {
        let pageX;
        let curCol;
        let nxtCol;
        let curColWidth;
        let tableWidth;
        div.addEventListener("click", (e) => e.stopPropagation());
        div.addEventListener("mousedown", function (e) {
            curCol = e.target.parentElement;
            nxtCol = curCol.nextElementSibling;
            pageX = e.pageX;
            tableWidth = table.offsetWidth;
            var padding = paddingDiff(curCol);
            curColWidth = curCol.offsetWidth - padding;
            if (nxtCol)
                nxtCol.offsetWidth - padding;
        });
        document.addEventListener("mousemove", function (e) {
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
                curCol.style.width = newCurColWidth + "px";
                table.style.width = tableWidth + diffX + "px";
            }
        });
        document.addEventListener("mouseup", function (e) {
            if (curCol) {
                for (let i = 0; i < cols.length; i++) {
                    const col = cols[i];
                    col.style.width = col["offsetWidth"] + "px";
                }
            }
            curCol = undefined;
            nxtCol = undefined;
            pageX = undefined;
            curColWidth = undefined;
            tableWidth = undefined;
        });
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
}

export { resizableDataTable };
//# sourceMappingURL=MConfigurableTable.razor.js.map
