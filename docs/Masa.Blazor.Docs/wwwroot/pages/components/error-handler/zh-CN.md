---
title: Error handler（异常处理）
desc: "`MErrorHandler`组件用来处理`blazor`组件数据加载和渲染时的异常信息。"
cols: 1
---

## 使用

### 异常统一处理：

1. `balzor`生命周期内的异常，无法处理，直接传递到`ErrorBoundry`处理；
2. 非MASA Blazor组件产生的异常，无法处理，直接传递到`ErrorBoundry`处理；
3. MASA Blazor组件非生命周期方法产生的异常，都可以处理，默认展示`Exception.Message`，也可以配置其它选项显示异常堆栈或自定义处理异常

<masa-example file="Examples.components.error_handler.Usage"></masa-example>

## 示例

### 属性

#### 是否显示告警

 自定义异常处理`Func<Exception,Task<bool>>`，点击按钮触发异常后，按钮背景色变为红色文字颜色改为白色。
1. 返回值为：`True`，则不再进行异常信息的提示，直接进行异常的处理，建议实现自定义异常处理后，自行进行异常信息的暂时，返回值设置为`True`;
2. 返回值为：`False`，会先进行异常信息的提示，再进行异常的处理。

<masa-example file="Examples.components.error_handler.ShowAlert"></masa-example>

#### 是否显示异常详细信息

`true` 显示，`false` 不显示， 默认不显示

<masa-example file="Examples.components.error_handler.ShowDetail"></masa-example>


#### 自定义处理

自定义异常处理 `Func<Exception,Task<bool>>`，点击按钮触发异常后，按钮背景为红色，文字颜色为白色。

<masa-example file="Examples.components.error_handler.CustomHandler"></masa-example>
