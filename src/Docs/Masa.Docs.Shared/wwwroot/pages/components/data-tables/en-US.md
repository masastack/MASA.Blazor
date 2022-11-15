---
title: Data tables
desc: "The MDataTable component is used for displaying tabular data. Features include sorting, searching, pagination, content-editing, and row selection."
related:
  - /components/data-iterators
  - /components/simple-tables
  - /components/lists
---

## Usage

The standard data-table will by default render your data as simple rows.

<data-iterators-usage></data-iterators-usage>

## Examples

### Props

#### CustomFilter

You can override the default filtering used with `Search` prop by supplying a function to the `CustomFilter` prop. If you
need to customize the filtering of a specific column, you can supply a function to the `Filter` property on header items.
The signature is `Func<object, string, TItem, bool>`. This function will always be run even if
`Search` prop has not been provided. Thus you need to make sure to exit early with a value of true if filter should not be
applied.

<example file="" />

#### Dense

Using the `Dense` prop you are able to give your data tables an alternate style.

<example file="" />

#### Filterable

You can easily disable specific columns from being included when searching through table rows by setting the property `Filterable` to false on the header item(s). In the example below the dessert name column is no longer searchable.

<example file="" />

#### Footer

The **MDataTable** renders a default footer using the **MDataFooter**  component. You can pass props to this component using `FooterProps`.

<example file="" />

#### Group

Using the `GroupBy` and `GroupDesc` props you can group rows on an item property. The `ShowGroupBy` prop will show a group
button in the default header. You can use the `Groupable` property on header items to disable the group button.

<example file="" />

#### Hide default header and footer

You can apply the `HideDefaultHeader` and `HideDefaultFooter` props to remove the default header and footer
respectively.

<example file="" />

#### Loading

You can use the `Loading` prop to indicate that data in the table is currently loading. If there is no data in the
table, a loading message will also be displayed. This message can be customized using the `LoadingText` prop or the
`LoadingContent` slot.

<example file="" />

#### Multi sort

Using the `MultiSort` prop will enable you to sort on multiple columns at the same time. When enabled, you can pass
arrays to both `SortBy` and `SortDesc` to programmatically control the sorting, instead of single values.

<example file="" />

#### ShowSelect

The `ShowSelect` prop will render a checkbox in the default header to toggle all rows, and a checkbox for each default
row. You can also switch between allowing multiple selected rows at the same time or just one with the `SingleSelect` prop.

<example file="" />

#### Search

The data table exposes a `Search` prop that allows you to filter your data.

<example file="" />

#### Fixed right

Using the `FixedRightFix` prop  the last column.

<example file="" />

#### Stripe

Striped table.

<example file="" />

### Contents

#### Header

You can use the dynamic slots **HeaderColContent** to customize only certain columns.

<example file="" />

#### Item

You can use the dynamic slots **ItemColContent** to customize only certain columns.

<example file="" />

#### Simple checkbox

When wanting to use a checkbox component inside of a slot template in your data tables, use the **MSimpleCheckbox**
component rather than the **MCheckbox** component. The **MSimplleChecbox** component is used internally and will respect
header alignment.

<example file="" />

### Misc

#### CRUD Actions

**MDataTable** with CRUD actions using a **MDialog** component for editing each row.

<example file="" />

#### EditDialog

The **MEditDialog** component can be used for editing data directly within a **MEditDialog**. You can block the closing of
the **MEditDialog** when clicked outside by adding the `Persistent` prop.

<example file="" />

#### ExpandableRow

The `ShowExpand`  prop will render an expand icon on each default row. You can customize this with the
**ItemDataTableExpandContent** slot. The position of this slot can be customized by adding a column
with `Value="data-table-expand"` to the **Headers** array. You can also switch between allowing multiple expanded rows
at the same time or just one with the `SingleExpand` prop. Row items require a unique key property for expansion to
work, use **ItemKey** prop to specify.

<example file="" />

#### External pagination

Pagination can be controlled externally by using the individual props, or by using the `Options` prop.

<example file="" />

#### External sorting

Sorting can also be controlled externally by using the individual props, or by using the the `Options` prop.

<example file="" />

#### Server-side paginate and sort

If you’re loading data already paginated and sorted from a backend, you can use the `ServerItemsLength` prop. Defining
this prop will disable the built-in sorting and pagination, and you will instead need to use the available events (
`OnPageUpdate`, `OnSortByUpdate`, `OnOptionsUpdate`, etc) to know when to request new pages from your backend. Use
the `Loading` prop to display a progress bar while fetching data.

<example file="" />