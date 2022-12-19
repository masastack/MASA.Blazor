---
order: 0
title:
  zh-CN: 自定义异常处理
  en-US: OnErrorHandleAsync
---

## zh-CN

自定义异常处理`Func<Exception,Task<bool>>`，点击按钮触发异常后，按钮背景色变为红色文字颜色改为白色。
返回值为：`True`，则不再进行异常信息展示的 提示，直接进行异常的处理，建议实现自定义异常处理后，自行进行异常信息的暂时，返回值设置为`True`;
返回值为：`False`，会先进行异常信息的提示，再进行异常的处理。

## en-US

Custom exception handling `Func<Exception, Task<bool>>`, after clicking the button triggers the exception, the button background color changes to red and the text color changes to white.
If the return value is `True`, the exception information display prompt will not be displayed, and the exception will be handled directly. It is recommended that after implementing custom exception handling, the exception information will be temporarily reset by itself, and the return value will be set to `True`;
The return value is: `False`, the exception information will be prompted first, and then the exception will be handled.
