---
order: 1
title: 发行说明
---



目前我们是不定期更新，以下是新的功能和修复的bug情况。

## 更新日期：2021-07-28

- 部分组件增加了过渡动画效果，包括MListGroup,MExpansionPanelContent，文档站点的Demo代码
  - 新增了Transition组件，模仿了vue的transition标签，用于过渡动画
  - Transition用法暂不能令我十分满意，所以暂时不建议使用
  - 其他组件的过渡动画也会相继补全，敬请期待
- Tree组件现在可以禁用节点了，具体请查看组件示例
- 修复了Card的Flat属性，现在Flat模式下不会再有阴影了
- DatePicker现在可以指定最大和最小日期了，具体请查看组件示例
- 修复了Alert的Text模式下Type属性没有更改颜色的问题以及Close图标的颜色没有从Alert继承问题
- 修复了TextField的重叠问题，现在非string类型即值类型不会为空，而是默认值，无效输入也会转为默认值
  - 暂时不支持可空类型，一方面，我们认为这不是必要的；另一方面，这也给我们的设计带来了很大的挑战。从TValue判断出Nullable<>,再转回Nullable<>是非常困难的，后续有时间我们会认真考虑这些事情
- Switch新增了内联模式，现在Switch可以设置不占满整格，具体请查看组件示例
- 调整了Autocomplete和Cascader的代码，现在它们的行为可预期的了

## 更新日期：2021-07-16

- 丰富了组件示例
- 新增组件API
- 规范化事件和插槽命名，事件以On开头，插槽以Content结尾。原有名称依然可以使用，但我们会在未来版本移除，请及时修改
- 完善了部分组件功能，包括Chip、List等

## 更新日期：2021-07-09

- 修复autocomplete超出不展示问题

## 更新日期：2021-07-01

- 增加了表单验证的[示例](/zh-CN/components/form)，支持手动验证
- [TextField](zh-CN/components/textfield)新增泛型支持，如果使用了@bind-Value则无需改变(有类型推断)，否则需要指定TValue
- 修复了Tooltip导致页面无限撑大的问题，现在Tooltip组件销毁时会自动从页面中移除
- 更改了ECharts的渲染方式，现在是使用svg模式渲染
- 修复了Snackbar的计时器bug，现在关闭后计时器也会停止
- [Tabel](/zh-CN/components/table)新增列宽指定，修改了Headers的类型(`List<string>=>List<TableHeaderOptions>`)，注意作相应的调整
- 修复了Autocomplete、Cascader的定位bug