---
title: Bottom sheets（底部工作表）
desc: "底部表单是一个经过修改的 `MDialog` , 从屏幕的底部滑出，类似于 `MBottomNavigation`。 底部导航组件用于按钮和特定的应用程序级操作，而底部工作表可以包含任何内容。"
related:
  - /components/dialogs
  - /components/lists
  - /components/menus
---

## 使用

在这里，我们将显示一个示例操作列表，它可能出现在应用程序中。

<bottom-sheets-usage></bottom-sheets-usage>

## 示例

### 属性

#### 嵌入

底部工作表可以被嵌入，将桌面上的最大宽度减少到70%。 可以使用 `Width` 参数来进一步手动减少这种情况。

<example file="" />

#### 模型

底部工作表可以使用 **@bind-Value** 来控制。 你可以使用它来关闭他们，或者如果你不能使用 **ActivatorContent** 插槽。

<example file="" />

#### 保留

保留的底部工作表无法通过在外部单击来关闭。

<example file="" />

### 其他

#### 音乐播放器

使用嵌入式底部工作表，你可以制作一些实用的组件，比如这个简单的音乐播放器。

<example file="" />

#### 在列表中打开

通过将功能列表合并到底部表单中，您可以创建一个简单的 ‘OpEN IN’ 组件。

<example file="" />
