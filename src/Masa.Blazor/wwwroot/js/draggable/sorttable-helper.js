export function init(containerId, option) {
    let _options = {
        group: null,
        disabled: false,
        handle: ".my-handle",
        draggable: ".item",
        onStart: function (evt) {
            log("onStart", evt);
        },
        onEnd: function (evt) {
            log("onEnd", evt);
        },
        onAdd: function (evt) {
            log("onAdd", evt);
        },
        onUpdate: function (evt) {
            log("onUpdate", evt);
        },
        onSort: function (evt) {
            log("onSort", evt);
        },
        onRemove: function (evt) {
            log("onRemove", evt);
        },
        onMove: function (evt) {
            log("onMove", evt);
        },
        onClone: function (evt) {
            log("onClone", evt);
        },
        onChange: function (evt) {
            log("onChange", evt);
        },
    }
    if (Sortable && containerId) {
        new Sortable(document.getElementById(containerId), Object.assign(_options, option))
    }

    function log(name, obj) {
        //debugger
        console.log(`${name} evt:${JSON.stringify(obj)}`);
    }

}