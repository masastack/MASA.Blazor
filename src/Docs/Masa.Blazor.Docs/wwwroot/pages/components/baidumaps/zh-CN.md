---
title: Baidu maps（百度地图）
desc: "[BaiduMaps JavaScript API GL](https://lbsyun.baidu.com/index.php?title=jspopularGL)"
tag: "Js代理"
---

## 使用

在使用百度地图服务前，首先需要到[百度地图开放平台](https://lbs.baidu.com/index.php?title=jspopularGL/guide/getkey)注册开发者账号，并申请AK。
要使用基础地图服务，请注册 **应用类型** 为 **浏览器端** 的AK。
 
然后引用BaiduMaps JS API GL的包：
`<script src="https://api.map.baidu.com/api?v=1.0&&type=webgl&ak={your-AK}"></script>`

<app-alert type="info" content='需要将 **{your-AK}** 替换为您申请的AK。'></app-alert>

<masa-example file="Examples.components.baidumaps.Usage"></masa-example>

现在，您可以通过鼠标拖动来漫游地图；通过设置 `EnableScrollWheelZoom` 来决定是否允许通过鼠标滚轮缩放地图。


## 示例

### 属性

#### 暗色主题

要使用暗色主题，请先到[我的地图](https://lbsyun.baidu.com/apiconsole/custommap)中创建并发布暗色地图样式，获取样式Id。
 
通过设置 `DarkThemeId` 指定暗色地图样式Id，并使用 `Dark` 属性切换到暗色主题。

<masa-example file="Examples.components.baidumaps.Dark"></masa-example>

<app-alert type="info" content='创建并发布地图样式的开发者账户必须与申请AK的账户一致。'></app-alert>

#### 高度和宽度

通过 `Height` , `Width`  属性设置宽高。

<masa-example file="Examples.components.baidumaps.HeightAndWidth"></masa-example>

#### 缩放与移动

通过 `Zoom` 设置地图的缩放等级。
 
要移动地图中心，需要使用 `PanTo()` 方法。如果该点在当前的地图视图中已经可见，则会以平滑动画的方式移动到中心点位置。


<masa-example file="Examples.components.baidumaps.ZoomAndMove"></masa-example>

