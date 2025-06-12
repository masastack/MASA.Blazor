# 触发器 {#activator}

## 基本概念 {#basic-concept}

在 Masa.Blazor 中，触发器（Activator）是一种核心设计模式，用于控制组件的显示和隐藏行为。
它允许开发者定义任何 UI 元素（如按钮、链接等）来触发组件的交互，而不需要直接在组件内部处理这些逻辑。
许多组件（如 [MMenu](/blazor/components/menus)、[MDailog](/blazor/components/dialogs)、[MTooltip](/blazor/components/tooltips)）
在默认情况下是隐藏的，可以通过特定的触发器元素来显示。

## 工作原理 {#how-it-works}

触发器模式的实现主要基于以下机制：

1. **唯一标识符生成**：每个触发器组件都会生成一个唯一的 activator ID（格式为 `_activator_{Guid.NewGuid()}`）
2. **属性传递**：通过 `@context.Attrs` 将这个唯一标识符属性应用到触发器元素上
3. **JavaScript 互操作**：组件在第一次渲染完成时，使用 JavaScript 来查找带有这个唯一标识符的元素，并为其添加事件监听器
4. **事件处理**：当用户与触发器元素交互时（如点击、悬停），这些预先添加的事件监听器会触发组件状态的改变

## 使用方式 {#usage}

### 基本用法 {#basic-usage}

```razor
<MMenu>
    <ActivatorContent>
        <MButton @attributes="context.Attrs">
            Open Menu
        </MButton>
    </ActivatorContent>
    <ChildContent>
        <MList>
            <MListItem>Item 1</MListItem>
            <MListItem>Item 2</MListItem>
        </MList>
    </ChildContent>
</MMenu>
```

### 代码解析 {#code-analysis}

- `<MMenu>`：主菜单组件
- `<ActivatorContent>`：定义触发器的插槽，提供包含 activator ID 的 `Attrs` 的上下文
- `<MButton ... @attributes="context.Attrs">`：**关键步骤**，将生成的 activator ID 和事件处理属性应用到按钮上
- `<ChildContent>`：菜单打开后显示的内容

当用户点击按钮时：

1. 事件监听器早已在组件初始渲染时被添加到带有 activator ID 的按钮元素上
2. 点击事件触发这些预先添加的事件处理函数，通知组件切换状态
3. 组件状态更新，菜单显示或隐藏

### 使用 Activator 属性 {#using-activator-property}

除了使用 `ActivatorContent` 插槽外，您还可以通过 `Activator` 属性直接指定触发器元素：

```razor
<MButton>
    <MMenu Activator="parent">
        <MList>
            <MListItem>Item 1</MListItem>
            <MListItem>Item 2</MListItem>
        </MList>
    </MMenu>
    Open Menu
</MButton>
```

`Activator` 属性支持以下用法：

- `Activator="parent"`：使用父元素作为触发器
- `Activator="#my-button"`：使用 CSS 选择器指定触发器元素

这种方式在某些场景下可以简化代码，无需使用 `ActivatorContent` 插槽和 `@context.Attrs`。

## 常见问题与解决方案 {#common-issues}

### 问题：触发器不工作 {#issue-activator-not-working}

**可能原因**：忘记应用 `@attributes="context.Attrs"`，导致 activator ID 未被添加到元素上。

**解决方案**：确保在触发器元素上正确应用 `@attributes="context.Attrs"`。

## 复杂场景 {#complex-scenarios}

当需要一个元素同时触发多个行为时，您可以组合使用不同的技术：

```razor
<MTooltip>
    <ActivatorContent Context="tooltipContext">
        <MMenu>
            <ActivatorContent Context="menuContext">
                <MButton @attributes="@tooltipContext.MergeAttrs(menuContext.Attrs)">
                    Menu with Tooltip
                </MButton>
            </ActivatorContent>
            <ChildContent>
                <MList>
                    <MListItem>Item 1</MListItem>
                    <MListItem>Item 2</MListItem>
                </MList>
            </ChildContent>
        </MMenu>
    </ActivatorContent>
    <ChildContent>
        Tooltip content here
    </ChildContent>
</MTooltip>
```

在这个高级示例中，我们需要特别处理属性合并，确保两个组件的 activator ID 和事件处理都能正确应用。
