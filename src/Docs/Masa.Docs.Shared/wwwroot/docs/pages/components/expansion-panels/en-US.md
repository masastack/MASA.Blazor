---
title: Expansion panels
desc: "The **MExpansionPanel** component is useful for reducing vertical space with large amounts of information. The default
functionality of the component is to only display one expansion-panel body at a time; however, with the **Multiple**
property, the expansion-panel can remain open until explicitly closed."
related:
  - /components/cards
  - /components/data-tables
  - /components/lists
---

## Usage

Expansion panels in their simplest form display a list of expandable items.

<expansion-panels-usage></expansion-panels-usage>

## Examples

### Props

#### Accordion

`Accordion` expansion-panel hasn’t got margins around active panel.

<example file="" />

#### Disabled

ExpansionPanels Both the extension panel and its content can be `disabled` using the disabled attribute. 

<example file="" />

#### Focusable

Expansion panel headers can be made focusable using the `focusable` property.

<example file="" />

#### Inset

When the `inset` property is activated, the current expansion panel becomes smaller.

<example file="" />

#### Model

The extension panel can be controlled externally by modifying `@bind-Value`. Its value corresponds to the currently open expansion panel index (0-based). If the `multiple` attribute is used, an array containing the indices of the open items.

<example file="" />

#### Popout

The expansion panel also has a popout design. If the expansion panel activates the `popout` property, the expansion panel will expand when activated.

<example file="" />

#### Readonly

The `readonly` attribute does the same thing as disabled , but without styling.

<example file="" />

### Misc

#### Advanced

The expansion panel component provides a rich playground to build truly advanced implementations. Here we take advantage of slots in the **MExpansionPanelHeader**   component to react to the state of being open or closed by fading content in and out.

<example file="" />

#### Custom icon

Expand action icon can be customized with `ExpandIcon` prop or the **ActionsContent**.

<example file="" />

