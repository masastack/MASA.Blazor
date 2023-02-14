# Transitions（过渡动画）

平滑的动画让界面体验更好。 使用 Blazor 的过渡系统和可复用的功能组件, 你可以轻松的控制应用程序的运动. 大多数组件可以通过 **stansition** prop改变它们的过渡动画.

## 使用

MASA Blazor 提供了 10 多个 css 动画模版，可快速应用于众多官方组件或自定义的组件上。

<masa-example file="Examples.styles_and_animations.transitions.Index"></masa-example>

## 示例

### 属性

#### 自定义原点

用一个简单的 prop 以编程的方式控制变换原点。

<masa-example file="Examples.styles_and_animations.transitions.CustomOrigin"></masa-example>

### 其他

#### Expand X

扩展过渡用于扩展面板和列表组。 同样还有一个水平版本 Transition 可用.

<masa-example file="Examples.styles_and_animations.transitions.ExpandX"></masa-example>

#### Fab

Fab 过渡的示例.

<masa-example file="Examples.styles_and_animations.transitions.Fab"></masa-example>

#### Fade

Fade 过渡的示例。

<masa-example file="Examples.styles_and_animations.transitions.Fade"></masa-example>

#### Scale

许多 MASA Blazor 组件都包含一个 transition 属性允许你指定想要的效果。

<masa-example file="Examples.styles_and_animations.transitions.Scale"></masa-example>

#### Scroll X

X 轴滚动过渡沿着水平轴继续。

<masa-example file="Examples.styles_and_animations.transitions.ScrollX"></masa-example>

#### Scroll Y

Y 轴滚动过渡沿着垂直轴继续。

<masa-example file="Examples.styles_and_animations.transitions.ScrollY"></masa-example>

#### Slide X

X 轴滑动过渡可沿水平方向移动。

<masa-example file="Examples.styles_and_animations.transitions.SlideX"></masa-example>

#### Slide Y

动画使用应用程序的 $primary-transition。

<masa-example file="Examples.styles_and_animations.transitions.SlideY"></masa-example>

#### TodoList

使用多个自定义转场，可以轻松实现简单的待办事项清单！

<masa-example file="Examples.styles_and_animations.transitions.TodoList"></masa-example>