---
title: BaiduMaps
desc: "[BaiduMaps JavaScript API GL](https://lbsyun.baidu.com/index.php?title=jspopularGL)"
tag: "JS Proxy"
---

## Usage

Before using the **MBaiduMap** component, go to [BaiduMap Open Platform](https://lbs.baidu.com/index.php?title=jspopularGL/guide/getkey) to register a 
developer account and apply for an AK.
To use base map services, register an AK whose **Application Type** is **Browser**.
 
Then refer the package of BaiduMaps JS API GL: 

```html
<script src="https://api.map.baidu.com/getscript?v=1.0&&type=webgl&ak={your-ak}"></script>
```

<app-alert type="info" content='Replace `{your-ak}` with your AK. '></app-alert>

<masa-example file="Examples.components.baidumaps.Usage"></masa-example>

## Examples

### Props

#### Dark

To use dark theme, go to [My Maps](https://lbsyun.baidu.com/apiconsole/custommap) to create and publish a map theme and get the Id. 
Use `DarkThemeId` to indicate the dark theme Id, and use `Dark` property to switch to dark theme. 

<app-alert type="info" content='The account that created and published the map theme must be the same as that applied for the AK. '></app-alert>

<masa-example file="Examples.components.baidumaps.Dark"></masa-example>

#### Height and width

`Height` and `Width` property set map height, width

<masa-example file="Examples.components.baidumaps.HeightAndWidth"></masa-example>

#### Move and zoom

Use `Zoom` to set the map zoom. Use `MaxZoom` and `MinZoom` to set the max and min zoom level of your map.
Use `Center` to get or set the map center. If target point is visible in sight, map center will smoothly move to that point. 

<masa-example file="Examples.components.baidumaps.ZoomAndMove"></masa-example>

#### Map Layers

Use `TrafficOn` to turn on the real-time traffic layer.
Use `MapType` to set type of the map (Normal map, Earth map or Satellite map).

<masa-example file="Examples.components.baidumaps.MapLayer"></masa-example>

### Overlays

 **MBaiduMap** component supports common map overlays. To add overlays, use overlay components as direct child component of **MBaiduMap** .
Or use related methods in **MBaiduMap** to operate overlays.

<masa-example file="Examples.components.baidumaps.Overlays"></masa-example>