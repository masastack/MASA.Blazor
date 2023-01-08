---
title: Steppers（步骤条）
desc: "**MStepper** 组件通过数字步骤显示进度。"
related:
  - /blazor/components/tabs
  - /blazor/components/buttons
  - /blazor/components/windows
---

## 使用

一个步骤组件适用于多种场景，包括购物车、创建记录等等。

<masa-example file="Examples.components.steppers.Usage"></masa-example>

## 示例

### 属性

#### 备用标签

**MStepper** 组件还有一个替代标签样式，将标题放在步骤下方。

<masa-example file="Examples.components.steppers.AlternateLabel"></masa-example>

#### 非线性的步骤条

 `NonLinear` 属性可以让用户按照自己选择路线在流程中移动。

<masa-example file="Examples.components.steppers.NonLinear"></masa-example>

#### 垂直

垂直步骤线通过定义的步骤沿Y轴移动用户。其他地方与水平方向的一致。

<masa-example file="Examples.components.steppers.Vertical"></masa-example>

### 其他

#### 错误状态的备用标签

错误状态同样可以应用于备用标签样式的显示。

<masa-example file="Examples.components.steppers.AlternativeLabelWithErrors"></masa-example>

#### 动态步骤

步骤条可以动态添加或删除它们的步骤。如果删除了当前活动的步骤，请务必通过更改应用的模型来解决这个问题。

<masa-example file="Examples.components.steppers.DynamicSteps"></masa-example>

#### 可编辑步骤

用户可以随时选择一个可编辑的步骤，并将他们导航到该步骤。

<masa-example file="Examples.components.steppers.EditableSteps"></masa-example>

#### 错误状态

可以显示错误状态来通知用户必须采取的一些行动。

<masa-example file="Examples.components.steppers.Errors"></masa-example>

#### 水平步骤线

水平步骤线通过定义的步骤沿 x 轴移动用户。

<masa-example file="Examples.components.steppers.HorizontalSteps"></masa-example>

#### 线性步骤

线性步骤始将始终通过您定义的路径移动用户。

<masa-example file="Examples.components.steppers.LinearSteppers"></masa-example>

#### 不可编辑步骤

不可编辑步骤（Non-editable steps）强制用户在整个流程中进行线性处理。

<masa-example file="Examples.components.steppers.NonEditableSteps"></masa-example>

#### 可选步骤

可以使用子文本调出可选步骤。

<masa-example file="Examples.components.steppers.OptionalSteps"></masa-example>

#### 垂直错误

相同的状态也适用于垂直的步骤。

<masa-example file="Examples.components.steppers.VerticalErrors"></masa-example>