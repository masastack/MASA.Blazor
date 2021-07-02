---
order: 1
title: 发行说明
---



目前我们是不定期更新，以下是新的功能和修复的bug情况。

## 更新日期：2021-07-01

- 增加了表单验证的[示例](/zh-CN/components/form)，支持手动验证
- [TextField](zh-CN/components/textfield)新增泛型支持，如果使用了@bind-Value则无需改变(有类型推断)，否则需要指定TValue
- 修复了Tooltip导致页面无限撑大的问题，现在Tooltip组件销毁时会自动从页面中移除
- 更改了ECharts的渲染方式，现在是使用svg模式渲染
- 修复了Snackbar的计时器bug，现在关闭后计时器也会停止
- [Tabel](/zh-CN/components/table)新增列宽指定，修改了Headers的类型(`List<string>=>List<TableHeaderOptions>`)，注意作相应的调整
- 修复了Autocomplete、Cascader的定位bug