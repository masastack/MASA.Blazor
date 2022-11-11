---
title: Error handler（异常处理）
desc: "`MErrorHandler`组件用来处理`blazor`组件数据加载和render时的异常信息。"
cols: 1
---

## 使用

### 异常统一处理：

1. `balzor`生命周期内的异常，无法处理，直接传递到`ErrorBoundry`处理；
2. 非MASA Blazor组件产生的异常，无法处理，直接传递到`ErrorBoundry`处理；
3. MASA Blazor组件非生命周期方法产生的异常，都可以处理，默认展示`Exception.Message`，也可以配置其它选项显示异常堆栈或自定义处理异常

<error-handler-usage></error-handler-usage>

## 示例

### 属性

#### 自定义异常处理

自定义异常处理 `Func<Exception,Task>`，点击按钮触发异常后，按钮背景为红色，文字颜色为白色。

<example file="" />

#### 是否显示告警

 `true`显示错误信息，并保留当前已填写的表单内容，`false`不显示错误，将异常向上传递到ErrorBoundry处理错误；
 如果在生命周期加载过程发生了错误，当前razor是否被包含在上级的Error handler中：
 1. 包含，将先显示异常，并将当前页面内容呈现为默认的ErrorBoundry错误处理处理内容；
 2. 不包含，当前页面直接内容呈现为默认的ErrorBoundry错误处理内容；

<example file="" />

#### 是否显示异常详细信息

`true` 显示，`false` 不显示， 默认不显示

<example file="" />




