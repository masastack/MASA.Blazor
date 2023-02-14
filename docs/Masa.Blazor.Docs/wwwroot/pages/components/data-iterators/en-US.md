---
title: Data iterators
desc: "The **MDataIterator** component is used for displaying data, and shares a majority of its functionality with the **MDataTable** component. Features include sorting, searching, pagination, and selection."
related:
  - /blazor/components/data-tables
  - /blazor/components/simple-tables
  - /blazor/components/toolbars
---

## Usage

<data-iterators-usage></data-iterators-usage>

## Examples

### Props

#### Default

The **MDataIterator** has internal state for both selection and expansion, just like **MDataTable**. In this example we use
the methods isExpanded and expand available on the default slot.

<masa-example file="Examples.components.data_iterators.Default"></masa-example>

#### Header and footer

The **MDataIterator**  has both a `HeaderContent` and `FooterContent` slot for adding extra content.

<masa-example file="Examples.components.data_iterators.HeaderAndFooter"></masa-example>

### Misc

#### Filter

Order, filters and pagination can be controlled externally by using the individual props.

<masa-example file="Examples.components.data_iterators.Filter"></masa-example>