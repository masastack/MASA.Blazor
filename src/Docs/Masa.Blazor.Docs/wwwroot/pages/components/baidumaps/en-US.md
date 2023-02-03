---
title: BaiduMaps
desc: "[BaiduMaps JavaScript API GL](https://lbsyun.baidu.com/index.php?title=jspopularGL)"
tag: "JsProxy"
---

## Usage

Before using the **MBaiduMap** component, go to [BaiduMap Open Platform](https://lbs.baidu.com/index.php?title=jspopularGL/guide/getkey) to register a 
developer account and apply for an AK.
To use base map services, register an AK whose **Application Type** is **Browser**.
 
Then refer the package of BaiduMaps JS API GL: 

```html
<script src="https://api.map.baidu.com/api?v=1.0&&type=webgl&ak={your-AK}"></script>
```

<app-alert type="info" content='Replace **{your-AK}** with your AK. '></app-alert>

<masa-example file="Examples.components.baidumaps.Usage"></masa-example>

Now, you can roam the map with mouse dragging; Set `EnableScrollWheelZoom` to determine whether you could zoom the map with mouse wheel. 


## Examples

### Props

#### Dark

To use dark theme, go to [My Maps](https://lbsyun.baidu.com/apiconsole/custommap) to create and publish a map theme and get the Id. 
 
Use `DarkThemeId` to indicate the dark theme Id, and use `Dark` property to switch to dark theme. 

<masa-example file="Examples.components.baidumaps.Dark"></masa-example>

<app-alert type="info" content='The account that created and published the map theme must be the same as that applied for the AK. '></app-alert>

#### Height and width

`Height` and `Width` property set map height, width

<masa-example file="Examples.components.baidumaps.HeightAndWidth"></masa-example>

#### Move and zoom

Use `Zoom` to get or set the map zoom. 
 
Use `Center` to get or set the map center. If target point is visible in sight, map center will smoothly move to that point. 

<masa-example file="Examples.components.baidumaps.ZoomAndMove"></masa-example>

