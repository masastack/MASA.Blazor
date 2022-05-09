---
order: 0
title:
  zh-CN: 是否显示告警
  en-US: ShowAlert
---

## zh-CN

 `true`显示错误信息，并保留当前已填写的表单内容，`false`不显示错误，将异常向上传递到ErrorBoundry处理错误；
 如果在生命周期加载过程发生了错误，当前razor是否被包含在上级的Error handler中：
 1. 包含，将先显示异常，并将当前页面内容呈现为默认的ErrorBoundry错误处理处理内容；
 2. 不包含，当前页面直接内容呈现为默认的ErrorBoundry错误处理内容；

## en-US

`true` displays the error message and retains the content of the form currently filled in, `false` does not display the error, and passes the exception up to ErrorBoundry to handle the error;
  If an error occurs during the life cycle loading process, whether the current razor is included in the upper-level Error handler:
  1. If included, the exception will be displayed first, and the current page content will be rendered as the default ErrorBoundry error handling content;
  2. Not included, the direct content of the current page is rendered as the default ErrorBoundry error handling content;
