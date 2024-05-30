class BaiduMapProxy {
  instance;
  dotNetHelper;

  constructor(containerId, initArgs) {
    this.instance = new BMapGL.Map(containerId);

    if (initArgs.enableScrollWheelZoom)
      this.instance.enableScrollWheelZoom();

    this.instance.setMaxZoom(initArgs.maxZoom);
    this.instance.setMinZoom(initArgs.minZoom);
    this.instance.centerAndZoom(initArgs.center, initArgs.zoom);

    this.instance.setMapType(initArgs.mapTypeString);

    if (initArgs.trafficOn)
      this.instance.setTrafficOn();

    if (initArgs.dark)
      this.instance.setMapStyleV2({
        styleId: initArgs.darkThemeId
      });
  }

  setDotNetObjectReference(dotNetHelper, events) {
    this.dotNetHelper = dotNetHelper;

    events.forEach((event_name) => {
      this.instance.addEventListener(event_name, async function (e) {
        if (event_name == "dragstart" ||
          event_name == "dragging" ||
          event_name == "dragend" ||
          event_name == "dblclick") {
          await dotNetHelper.invokeMethodAsync("OnEvent", event_name, {
            latlng: e.point,
            pixel: e.pixel,
          });
        }
        else if (event_name == "click" ||
          event_name == "rightclick" ||
          event_name == "rightdblclick" ||
          event_name == "mousemove") {
          await dotNetHelper.invokeMethodAsync("OnEvent", event_name, {
            latlng: e.latlng,
            pixel: e.pixel,
          });
        }
        else {
          await dotNetHelper.invokeMethodAsync("OnEvent", event_name, null);
        }
      });
    });
  }

  getOriginInstance = () => this.instance;

  getCenter = () => this.instance.getCenter();

  setZoom = (zoom) => this.instance.setZoom(zoom);

  getZoom = () => this.instance.getZoom();

  setMaxZoom = (maxZoom) => this.instance.setMaxZoom(maxZoom);

  setMinZoom = (minZoom) => this.instance.setMinZoom(minZoom);

  enableScrollWheelZoom = () => this.instance.enableScrollWheelZoom();

  disableScrollWheelZoom = () => this.instance.disableScrollWheelZoom();

  setMapType = (mapType) => this.instance.setMapType(mapType);

  getMapType = () => this.instance.getMapType();

  setTrafficOn = () => this.instance.setTrafficOn();

  setTrafficOff = () => this.instance.setTrafficOff();

  setMapStyleV2 = (options) => this.instance.setMapStyleV2(options);

  panTo = (point) => this.instance.panTo(point);

  addOverlay = (overlay) => this.instance.addOverlay(overlay);

  removeOverlay = (overlay) => this.instance.removeOverlay(overlay);

  clearOverlays = () => this.instance.clearOverlays();

  addCircle(circle) {
    var c = new BMapGL.Circle(circle.center, circle.radius, {
      strokeColor: circle.strokeColor,
      strokeWeight: circle.strokeWeight,
      strokeOpacity: circle.strokeOpacity,
      strokeStyle: circle.strokeStyle == 0 ? "solid" : "dashed",
      fillColor: circle.fillColor,
      fillOpacity: circle.fillOpacity
    });

    this.instance.addOverlay(c);

    return c;
  }

  addMarker(marker) {
    var m = new BMapGL.Marker(marker.point, {
      offset: marker.offset,
      rotation: marker.rotation,
      title: marker.title
    });

    this.instance.addOverlay(m);

    return m;
  }

  addLabel(label) {
    var l = new BMapGL.Label(label.content, {
      offset: label.offset,
      position: label.position
    });

    this.instance.addOverlay(l);

    return l;
  }

  addPolyline(polyline) {
    if (polyline.points == null)
      return null;

    var pl = new BMapGL.Polyline(polyline.points, {
      strokeColor: polyline.strokeColor,
      strokeWeight: polyline.strokeWeight,
      strokeOpacity: polyline.strokeOpacity,
      strokeStyle: polyline.strokeStyle == 0 ? "solid" : "dashed",
      geodesic: polyline.geodesic,
      clip: polyline.clip
    });

    this.instance.addOverlay(pl);

    return pl;
  }

  addPolygon(polygon) {
    if (polygon.points == null)
      return null;

    var bmapPoints = [];
    polygon.points.forEach(element => {
      bmapPoints.push(toBMapGLPoint(element));
    });

    var pg = new BMapGL.Polygon(bmapPoints, {
      strokeColor: polygon.strokeColor,
      strokeWeight: polygon.strokeWeight,
      strokeOpacity: polygon.strokeOpacity,
      strokeStyle: polygon.strokeStyle == 0 ? "solid" : "dashed",
      fillColor: polygon.fillColor,
      fillOpacity: polygon.fillOpacity
    });

    this.instance.addOverlay(pg);

    return pg;
  }

  contains(overlay) {
    var os = this.instance.getOverlays();
    for (let index = 0; index < os.length; index++) {
      if (os[index] === overlay)
        return true;
    }
    return false;
  }

  destroyMap() {
    if (this.instance != null)
      delete this.instance;
  }
}

BMapGL.Polygon.prototype.setPathWithGeoPoint = (points, polygon) => {
  if (points == null)
    return;

  var bmapPoints = [];
  points.forEach(element => {
    bmapPoints.push(toBMapGLPoint(element));
  });

  polygon.setPath(bmapPoints);
}

const toBMapGLPoint = (point) => new BMapGL.Point(point.lng, point.lat);

const init = (containerId, initArgs) => {
  if (containerId && document.getElementById(containerId)) {
    return new BaiduMapProxy(containerId, initArgs);
  }

  return null;
}

export { init }