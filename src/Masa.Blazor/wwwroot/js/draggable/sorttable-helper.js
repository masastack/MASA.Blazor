export function init(dotNetHelper, containerId, option) {
    log("init", option);
    let nameSpace = "";
    let _options = {
        onChoose: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnChoose`, null);
        },
        onUnchoose: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnUnchoose`, null);
        },
        onStart: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnStart`, convertDropEndEvent(evt));
        },
        onEnd: function (evt) {           
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnDropEnd`, convertDropEndEvent(evt))
        },
        onAdd: function (evt) {
            debugger
            if (evt.clone) {
                evt.item.remove();
            }
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnAdd`, convertDropEndEvent(evt));
        },
        onUpdate: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnUpdate`, convertDropEndEvent(evt));
        },
        onSort: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnSort`, convertDropEndEvent(evt));
        },
        onRemove: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnRemove`, convertDropEndEvent(evt));
        },
        onMove: function (evt, originalEvent) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnMove`, convertDropMoveEvent(evt, originalEvent));
        },
        onClone: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnClone`, convertDropEndEvent(evt));
        },
        onChange: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnDrop`, convertDropEndEvent(evt));
        },
    }

    if (option.group.pull !== undefined) {
        if (typeof option.group.pull === 'boolean' || option.group.pull == 'clone') {
        } else {
            option.group.pull = function () {
                return dotNetHelper.invokeMethodAsync(`${nameSpace}OnPull`);
            }
        }
    }

    if (option.group.put !== undefined) {
        if (typeof option.group.put === 'boolean' || option.group.put.join) {

        } else {
            option.group.put = function () {
                return dotNetHelper.invokeMethodAsync(`${nameSpace}OnPut`);
            }
        }
    }

    if (Sortable && containerId) {
        var ops = Object.assign(_options, option)
        //log("assign", ops);
        //console.log(`containerId:${containerId},dom:${document.getElementById(containerId)}`)
        new Sortable(document.getElementById(containerId), ops);
    }

    function log(name, obj) {
        obj["__callname"] = name;
        console.log(obj);
    }

    function convertDropEndEvent(event) {
        log(event.name, event)
        if (!event) return null;
        var result = {
            parentId: event.from.id,
            newParentId: event.to.id,
            itemId: event.item.id,
            oldIndex: event.oldIndex,
            newIndex: event.newIndex
        };
        if (result.oldIndex === null) result.oldIndex = -1;
        if (result.newIndex === null) result.newIndex = -1;

        if (event.pullMode == "clone")
            result.isClone = true
        return result;
    }

    function convertDropMoveEvent(event, dragEvent) {
        if (!event) return null;
        var result = {
            parentId: event.from.id,
            newParentId: event.to.id,
            itemId: event.dragged.id,
            rect: event.draggedRect,
            moved: {
                clientX: dragEvent.clientX,
                clientY: dragEvent.clientY,
                screenX: dragEvent.screenX,
                screenY: dragEvent.screenY,
                offsetX: dragEvent.offsetX,
                offsetY: dragEvent.offsetY,
                pageX: dragEvent.pageX,
                pageY: dragEvent.pageY,
                layerX: dragEvent.layerX,
                layerY: dragEvent.layerY,
            }
        };
        return result;
    }
}