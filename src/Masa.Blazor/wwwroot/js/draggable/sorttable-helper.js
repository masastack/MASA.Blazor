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
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnAdd`, convertDropEndEvent(evt));
            console.log(`onadd id: ${containerId} sort:${this.toArray().join(',')}`)
        },
        onUpdate: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnUpdate`, convertDropEndEvent(evt));
            console.log(`onupdate id: ${containerId} sort:${this.toArray().join(',')}`)
        },
        onSort: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnSort`, convertDropEndEvent(evt));
        },
        onRemove: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnRemove`, convertDropEndEvent(evt));
            console.log(`onremove id: ${containerId} sort:${this.toArray().join(',')}`)
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

        if (event.pullMode == "clone") {
            result.isClone = true;
            if (!event.clone.id) {
                let itemId = event.item.id;
                event.item.id = "di_" + new Date().getTime().toString();
                event.clone.id = event.item.id;
            }
            result.cloneId = event.item.id;
            result.itemId = event.clone.id;
        }

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

export function getSort(containerId) {
    if (Sortable && containerId) {
        let table = Sortable.get(document.getElementById(containerId));
        return table.toArray();
    }
    return [];
}

export function sort(containerId, data) {
    return;


    if (Sortable && containerId) {
        let table = Sortable.get(document.getElementById(containerId));
        //let options = table.options
        //console.log(options);
        //if (table) {
        //    let options = table.options
        //    table.destroy();
        //    table = new Sortable(document.getElementById(containerId), options)
        //}

        //table = new Sortable(document.getElementById(containerId), options);
        console.log(`id: ${containerId} old:${table.toArray().join(',')},new:${data.join(",")}`)
        // table.sort(data);
    }
}