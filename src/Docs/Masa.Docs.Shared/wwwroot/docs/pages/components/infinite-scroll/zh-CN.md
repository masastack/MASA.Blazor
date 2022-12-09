---
title: Infinite scroll（无限滚动）
desc: "列表滚动到底部自动加载更多数据。"
related:
  - /components/lists
  - /components/progress-circular
  - /components/virtual-scroller
---

## 使用

当 `HasMore` 属性为 `true` 时，当用户页面滚动到底部 `Threshold`（默认为 250px）时，组件将调用定义的 `OnLoadMore` 函数。支持请求失败时点击重试。 加载更多时抛出异常

<infinite-scroll-usage></infinite-scroll-usage>

## 示例

### 插槽

#### 自定义内容

<example file="" />

### 其他

#### 使用虚拟滚动条

<example file="" />
