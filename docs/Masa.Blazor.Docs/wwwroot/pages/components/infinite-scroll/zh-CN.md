---
title: Infinite scroll（无限滚动）
desc: "列表滚动到底部自动加载更多数据。"
related:
  - /blazor/components/lists
  - /blazor/components/progress-circular
  - /blazor/components/virtual-scroller
---

## 使用 {#usage}

当组件首次呈现时，或用户页面滚动到底部 `Threshold`（默认为 100px）时，组件将调用的 `OnLoad` 事件。

<masa-example file="Examples.components.infinite_scroll.Usage"></masa-example>

通过 `OnLoad` 事件提供的 `InfiniteScrollLoadEventArgs` 参数的 `Status` 属性设置组件的状态。

| 状态        | 描述                 | 插槽                |
|-----------|:-------------------|-------------------| 
| `Ok`      | 已成功加载内容            | `LoadMoreContent` |
| `Error`   | 加载内容时出错            | `ErrorContent`    |
| `Empty`   | 已没有内容可以加载了         | `EmptyContent`    |
| `Loading` | 内容加载中。此状态应仅由组件内部设置 | `LoadingContent`  |

## 示例 {#examples}

### 属性 {#props}

#### 错误重试 {#error}

`OnLoad` 事件会自动处理异常并设置状态为 `Error`。如果你手动处理了异常，则可以设置 `Error` 属性为 `true`，以显示错误内容。

<masa-example file="Examples.components.infinite_scroll.Error"></masa-example>

#### 手动加载 {#manual}

默认是自动加载，可以设置 `Manual` 属性，改为手动加载模式。

<masa-example file="Examples.components.infinite_scroll.Manual"></masa-example>

### 插槽 {#contents}

#### 自定义指定状态的内容 {#custom-content-for-specified-status}

通过 `ErrorContent`，`LoadingContent`，`LoadMoreContent`，`EmptyContent` 插槽可以自定义指定状态的内容。

<masa-example file="Examples.components.infinite_scroll.CustomContent"></masa-example>

#### 自定义默认内容 {#custom-default-content}

或者通过默认插槽，自定义整个组件的内容。

<masa-example file="Examples.components.infinite_scroll.ChildContent"></masa-example>

### 其他 {#misc}

#### 使用虚拟滚动条 {#use-virtualScroller}

<masa-example file="Examples.components.infinite_scroll.VirtualScroller"></masa-example>
