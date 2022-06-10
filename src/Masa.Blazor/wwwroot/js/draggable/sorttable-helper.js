export function init(dotNetHelper, containerId, option) {
    //log("init", option);
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
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnDropEnd`, convertDropEndEvent(evt)).then((data) => {

            });
        },
        onAdd: function (evt) {
            let param = convertDropEndEvent(evt);
            let cloneId = false;
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnAdd`, param).then((id) => {
                debugger
                cloneId = id;
                //if (param.parentId != param.newParentId) {
                //    evt.from = evt.to;
                //    evt.oldIndex = evt.newIndex
                //}
                //debugger

                //if (param.parentId != param.newParentId) {
                //setTimeout(() => {
                //    debugger
                //    let ccc = evt.to.children;

                //   // evt.from.appendChild(evt.item);
                //   // evt.oldIndex = evt.from.children.length;
                //}, 500);
                //}
            });
            //debugger
            if (param.isClone) {
                let index = 1;
                while (!cloneId) {
                    setTimeout(() => { }, 10);
                    index++;
                    if (index - 20 > 0)
                        break;
                }

                //evt.clone.remove();
                //evt.clone = document.getElementById(cloneId);
                //evt.from = evt.to;
                //let temp = evt.clone;
                //evt.clone = evt.item;
                //evt.item = temp;
            }
            //console.log(`onadd id: ${containerId} sort:${this.toArray().join(',')}`)
        },
        onUpdate: function (evt) {
            let param = convertDropEndEvent(evt);
            let table = this;
            let ids;
            //evt.item.remove();
            //evt.item
            let t = 0;

            dotNetHelper.invokeMethodAsync(`${nameSpace}OnUpdate`, param).then(() => {
                //let id = evt.item.id
                //evt.item = document.getElementById(id);
                // ids = table.toArray();

                // console.log(`onupdateasync id: ${containerId} sort:${ids.join(',')}`)
                //table.sort(ids);
                //evt.oldIndex = evt.newIndex;
                //evt.to = null;
                //evt.from = null;
                //evt.item = null;
                //evt.dragged = null;
                //evt.clone = null;
                //evt.item = 
                //debugger
                //let item= document.getElementById(id)

                t = 1;
            });

            //let index = 1;
            //while (!t) {
            //    setTimeout(() => { }, 10);
            //    index++;
            //    if (index - 20 > 0) break;
            //}
            //debugger
            /*Sortable.utils.off(evt.item, 'dragend');*/
            /*off(dragEl, 'dragend', this)*/
            //table.sort(ids);
            //evt = null;
            //console.log(`onupdate id: ${containerId} sort:${this.toArray().join(',')}`)
        },
        onSort: function (evt) {
            //let table = this;
            //let ids = (table.toArray() || []).join(',');
            //evt.item.remove();
            let t = 0;
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnSort`, convertDropEndEvent(evt)).then(() => {
                //setTimeout(() => {
                //    var str = (data || []).join(',');
                //   // debugger
                //    if (ids != str)
                //        table.sort(data);
                //}, 50)

                //if (param.parentId != param.newParentId) {
                //evt.from.appendChild(evt.item);
                //evt.oldIndex = evt.from.children.length;
                //}
                t = 1;
            });

            //let index = 1;
            //while (!t) {
            //    setTimeout(() => { }, 10);
            //    index++;
            //    if (index - 20 > 0) break;
            //}
            /*Sortable.utils.off(evt.item, 'dragend');*/
            //debugger

            //evt.from.childNodes
            //evt.item

            //evt.from = evt.to;
            //evt.oldIndex = evt.newIndex
            //if (param.parentId != param.newParentId) {
            //       evt.from = evt.to;
            //       evt.oldIndex = evt.newIndex
            //   }
            // dotNetHelper.invokeMethodAsync(`${nameSpace}OnSort`, convertDropEndEvent(evt));
        },
        onRemove: function (evt) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnRemove`, convertDropEndEvent(evt));
            //console.log(`onremove id: ${containerId} sort:${this.toArray().join(',')}`)
        },
        onMove: function (evt, originalEvent) {
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnMove`, convertDropMoveEvent(evt, originalEvent));
        },
        onClone: function (evt) {
            let param = convertDropEndEvent(evt);
            //let cloneId = false;
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnClone`, param).then((id) => {
                //cloneId = id;
            });

            //if (param.isClone) {
            //    let i = 0;
            //    while (!cloneId) {
            //        setTimeout(() => {}, 10);
            //        i++;
            //        if (i - 20 >= 0)
            //            break;
            //    }

            //    if (cloneId)
            //        evt.clone = document.getElementById(cloneId);
            //}
        },
        onChange: function (evt) {
            //console.log("onChange")
            //console.log(evt);
            dotNetHelper.invokeMethodAsync(`${nameSpace}OnChange`, convertDropEndEvent(evt));
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
        console.log(event.type, event.oldIndex, event.newIndex, event.item, event.from, event.to, event);
        //log(event.name, event)
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
            //if (!event.clone.id) {
            //    let itemId = event.item.id;
            //    event.item.id = "di_" + new Date().getTime().toString();
            //    event.clone.id = event.item.id;
            //}
            //result.cloneId = event.item.id;
            //result.itemId = event.clone.id;
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
    //return;



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
        table.sort(data);
    }
}