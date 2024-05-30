import DrawflowClass, { DrawFlowEditorMode, DrawflowModuleData } from "drawflow";

declare const Drawflow: DrawflowClass;

class DrawflowProxy {
  editor: DrawflowClass;
  dotnetHelper: DotNet.DotNetObject;

  mousedown = new MouseEvent("mousedown", {
    view: window,
    bubbles: true,
    cancelable: false,
  });

  mouseup = new MouseEvent("mouseup", {
    view: window,
    bubbles: true,
    cancelable: false,
  });

  constructor(
    selector: string,
    dotnetHelper: DotNet.DotNetObject,
    mode: DrawFlowEditorMode
  ) {
    var el = document.querySelector<HTMLElement>(selector);
    this.editor = new (Drawflow as any)(el);
    this.editor.start();
    this.editor.editor_mode = mode;
    const that = this;

    this.editor.on("nodeCreated", function (id) {
      dotnetHelper.invokeMethodAsync("OnNodeCreated", id.toString());
    });

    this.editor.on("nodeRemoved", function (id) {
      dotnetHelper.invokeMethodAsync("OnNodeRemoved", id.toString());
    });

    this.editor.on("nodeSelected", function (id) {
      dotnetHelper.invokeMethodAsync("OnNodeSelected", id.toString());
    });

    this.editor.on("nodeUnselected", function (id) {
      dotnetHelper.invokeMethodAsync("OnNodeUnselected", id.toString());
    });

    this.editor.on("nodeDataChanged" as any, function (id) {
      dotnetHelper.invokeMethodAsync("OnNodeDataChanged", id.toString());
    });

    this.editor.on("import", function (e) {
      dotnetHelper.invokeMethodAsync("OnImport");
    });
  }

  setMode(mode: DrawFlowEditorMode) {
    this.editor.editor_mode = mode;
  }

  addNode(
    name: string,
    inputs: number,
    outputs: number,
    clientX: number,
    clientY: number,
    offsetX: number,
    offsetY: number,
    className: string,
    data: object = {},
    html: string
  ) {
    if (this.editor.editor_mode == "fixed") {
      return null;
    }

    const posX =
      clientX *
        (this.editor.precanvas.clientWidth /
          (this.editor.precanvas.clientWidth * this.editor.zoom)) -
      this.editor.precanvas.getBoundingClientRect().x *
        (this.editor.precanvas.clientWidth /
          (this.editor.precanvas.clientWidth * this.editor.zoom)) -
      offsetX;

    const posY =
      clientY *
        (this.editor.precanvas.clientHeight /
          (this.editor.precanvas.clientHeight * this.editor.zoom)) -
      this.editor.precanvas.getBoundingClientRect().y *
        (this.editor.precanvas.clientHeight /
          (this.editor.precanvas.clientHeight * this.editor.zoom)) -
      offsetY;

    var nodeId = this.editor.addNode(
      name,
      inputs,
      outputs,
      posX,
      posY,
      className,
      data,
      html,
      false
    );

    return nodeId.toString();
  }

  removeNodeId(id: string) {
    this.editor.removeNodeId(id);
  }

  getNodeFromId(id: string) {
    const node: any = this.editor.getNodeFromId(id);
    node["id"] = node.id.toString();
    return node;
  }

  updateNodeDataFromId(id: string, data: object) {
    this.editor.updateNodeDataFromId(id, data);
    (this.editor as any).dispatch("nodeDataChanged", id);
  }

  updateNodeHtml(id: string, html: string) {
    this.editor.drawflow.drawflow.Home.data[id].html = html;
  }

  addNodeInput(id: string) {
    this.editor.addNodeInput(id);
  }

  addNodeOutput(id: string) {
    this.editor.addNodeOutput(id);
  }

  removeNodeInput(id: string, inputClass: string) {
    this.editor.removeNodeInput(id, inputClass);
  }

  removeNodeOutput(id: string, outputClass: string) {
    this.editor.removeNodeOutput(id, outputClass);
  }

  updateConnectionNodes(id: string) {
    this.editor.updateConnectionNodes(id);
  }

  removeConnectionNodeId(id: string) {
    this.editor.removeConnectionNodeId(id);
  }

  clear() {
    this.editor.clear();
  }

  export(indented: boolean = false) {
    const res = this.editor.export();
    return JSON.stringify(res, null, indented ? 2 : null);
  }

  import(json: string) {
    const data = JSON.parse(json);
    this.editor.import(data);
  }

  focusNode(id: string) {
    document
      .querySelector(`#node-${id} .drawflow_content_node`)
      .dispatchEvent(this.mousedown);
    document
      .querySelector(`#node-${id} .drawflow_content_node`)
      .dispatchEvent(this.mouseup);
  }

  centerNode(id: string, animate: boolean) {
    const node = document.getElementById(`node-${id}`);
    const args = {
      node_x: this.editor.drawflow.drawflow.Home.data[id].pos_x,
      node_y: this.editor.drawflow.drawflow.Home.data[id].pos_y,
      node_w: node.clientWidth,
      node_h: node.clientHeight,
      canvas_w: this.editor.precanvas.clientWidth,
      canvas_h: this.editor.precanvas.clientHeight,
    };
    const pos_x = -args.node_x + args.canvas_w / 2 - args.node_w / 2;
    const pos_y = -args.node_y + args.canvas_h / 2 - args.node_h / 2;
    const zoom = this.editor.zoom;
    this.setTranslate(pos_x, pos_y, zoom);

    if (animate) {
      const millisecondsStart = 50;
      const millisecondsAnimate = 500;
      node.style.transition = `all ${millisecondsAnimate / 1000}s ease 0s`;
      window.setTimeout(() => {
        node.style.transform = "scale(1.1)";
      }, millisecondsStart);
      window.setTimeout(() => {
        node.style.transform = "scale(1.0)";
      }, millisecondsStart + millisecondsAnimate);
      window.setTimeout(() => {
        node.style.transition = "";
        node.style.transform = "";
      }, millisecondsStart + millisecondsAnimate * 2);
    }

    this.focusNode(id);
  }

  setTranslate(x: number, y: number, zoom: number) {
    this.editor.canvas_x = x;
    this.editor.canvas_y = y;
    let storedZoom = zoom;
    this.editor.zoom = 1;
    this.editor.precanvas.style.transform =
      "translate(" +
      this.editor.canvas_x +
      "px, " +
      this.editor.canvas_y +
      "px) scale(" +
      this.editor.zoom +
      ")";
    this.editor.zoom = storedZoom;
    this.editor.zoom_last_value = 1;
    this.editor.zoom_refresh();
  }
}

function init(
  selector: string,
  dotNetHelper: DotNet.DotNetObject,
  mode: DrawFlowEditorMode = "edit"
) {
  return new DrawflowProxy(selector, dotNetHelper, mode);
}

export { init };
