# Activator

## Basic Concept

In Masa.Blazor, the Activator is a core design pattern used to control the display and hiding behavior of components.
It allows developers to define any UI elements (such as buttons, links, etc.) to trigger component interactions without having to handle these logics directly inside the component.
Many components (such as [MMenu](/blazor/components/menus), [MDailog](/blazor/components/dialogs), [MTooltip](/blazor/components/tooltips))
are hidden by default and can be displayed through specific activator elements.

## How It Works

The implementation of the activator pattern is primarily based on the following mechanisms:

1. **Unique Identifier Generation**: Each activator component generates a unique activator ID (in the format of `_activator_{Guid.NewGuid()}`)
2. **Attribute Passing**: The unique identifier attribute is applied to the activator element through `@context.Attrs`
3. **JavaScript Interop**: When the component completes its first render, JavaScript is used to find elements with this unique identifier and attach event listeners to them
4. **Event Handling**: When users interact with the activator element (e.g., clicking, hovering), these pre-added event listeners trigger changes in the component state

## Usage

### Basic Usage

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

### Code Analysis

- `<MMenu>`: The main menu component
- `<ActivatorContent>`: The slot for defining the activator, providing context with the `Attrs` containing the activator ID
- `<MButton ... @attributes="context.Attrs">`: **Key step**, applying the generated activator ID and event handling properties to the button
- `<ChildContent>`: Content displayed when the menu is open

When a user clicks the button:

1. Event listeners have already been added to the button element with the activator ID during the component's initial render
2. The click event triggers these pre-added event handlers, notifying the component to toggle its state
3. The component state updates, and the menu is displayed or hidden

### Using Activator property

In addition to using the `ActivatorContent` slot, you can directly specify the activator element using the `Activator` property:

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

The `Activator` property supports the following usages:

- `Activator="parent"`: Use the parent element as the activator
- `Activator="#my-button"`: Use a CSS selector to specify the activator element

This approach can simplify code in certain scenarios, eliminating the need for the `ActivatorContent` slot and `@context.Attrs`.

## Common Issues

### Issue: Activator Not Working {#issue-activator-not-working}

**Possible Cause**: Forgetting to apply `@attributes="context.Attrs"`, resulting in the activator ID not being added to the element.

**Solution**: Ensure `@attributes="context.Attrs"` is correctly applied to the activator element.

## Complex Scenarios

When you need an element to trigger multiple behaviors, you can combine different techniques:

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

In this advanced example, we need to specially handle attribute merging to ensure that both components' activator IDs and event handling are correctly applied.
