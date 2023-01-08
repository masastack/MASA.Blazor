---
title: Windows（窗口）
desc: "**MWindow** 组件提供了将内容从一个窗格过渡到另一个窗格的基础功能。 其他组件如 **MTabs**、**MCarousel** 和  **MStepper** 使用此组件作为其核心。"
related:
  - /blazor/components/carousels
  - /blazor/components/steppers
  - /blazor/components/tabs
---

## 使用

**MWindow** 被设计成可以轻松地循环浏览内容，它提供了一个简单的接口来创建真正的自定义实现。

<masa-example file="Examples.components.windows.Usage"></masa-example>

## 示例

### 属性

#### 反转

反转 **MWindow** 总是显示反向过渡。

<masa-example file="Examples.components.windows.Reverse"></masa-example>

#### 垂直

**MWindow** 可以是垂直的。 垂直窗口有 Y 轴过渡而不是 X 轴过渡。

<masa-example file="Examples.components.windows.Vertical"></masa-example>

#### 自定义箭头按钮

可以用 **PrevContent** 和 **NextContent** 槽来自定义窗口中的箭头部分。

<masa-example file="Examples.components.windows.CustomizedArrows"></masa-example>

### 其他

#### 创建账户

创建具有平滑动画的丰富表单。 **MWindow** 能够自动跟踪当前的选择索引，来改变过渡动画的方向。 这可以通过 `Reverse` 参数来手动控制。

<masa-example file="Examples.components.windows.AccountCreation"></masa-example>

#### 新手教学

**MWindow** 使创建自定义组件（如不同样式的幻灯片）变得很轻松。

<masa-example file="Examples.components.windows.Onboarding"></masa-example>