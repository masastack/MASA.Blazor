# 布局

使用可定制和扩展的布局选项，构建漂亮的用户体验。

## 使用

MASA Blazor 有一个内置的开箱即用的布局系统。 组件如 MAppBar 和 MFooter 支持一个名为 app 的特殊属性。 此属性告诉 MASA Blazor，相应的组件是应用程序布局的一部分。 在本节中，您将学习布局系统如何工作的基本知识。 如何合并多个布局组件，以及如何动态地改变它们。

MASA Blazor有两个主要布局组件， MApp 和 MMain。 MApp 组件是您的应用程序的根节点，`<MApp>//其它布局内容</MApp>`。 MMain 组件是替换 main HTML 元素和您应用程序的根节点 内容 的语义替代。 当 Blazor 挂载到 DOM 时，作为布局一部分的任何 MASA Blazor 组件都会将其当前高度和/或宽度注册到框架核心。 MMain 组件接下来需要这些值并调整容器的大小。

为了更好地说明这一点，让我们创建一个基本的 MASA Blazor 布局：

```html
<MApp>
  <MMain>
    Hello World
  </MMain>
</MApp>
```

模板中没有布局组件, MMain 不需要调整其大小, 而是要占用整个页面。 让我们在 MMain 元素上面添加 [MAppBar](/components/app-bars) 来更改布局：

```html
<MApp>
  <!-- Must have the app property -->
  <MAppBar App></MAppBar>

  <MMain>
    Hello World
  </MMain>
</MApp>
```

因为我们给了 MAppBar App 属性, MASA Blazor 知道它是布局的一部分。 MMain 然后获取我们的 bar 的注册高度，并从其可用内容区域中移除相同数量的空间。在这个例子中，64px的空间从 MMain 的容器顶部移除。

最后，让我们通过将内容包装在 MContainer 组件中来添加一个 gutter ：

```html
<MApp>
  <!-- Must have the app property -->
  <MAppBar App></MAppBar>

  <MMain>
    <MContainer>
      Hello World
    </MContainer>
  </MMain>
</MApp>
```

接下来，我们使用新建立的基线并使用新的布局组件和定制选项对其进行增强。

## 组合布局组件

关注更多

## 动态布局

关注更多

