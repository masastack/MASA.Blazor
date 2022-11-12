---
title: Pagination（分页）
desc: "The **MPagination** component is used to separate long data sets so that user data information can be known. Provided the amount, the paging component will automatically scale. The current maintenance page provides value easily."
related:
  - /components/data-iterators
  - /components/data-tables
  - /components/lists
---

## Usage

By default, paging displays the number of pages according to the set `Length` property, and there are `Prev` and `Next` buttons on both sides to help navigation.

<pagination-usage></pagination-usage>

## Examples

### Props

#### Circle

The `Circle` property provides you with another style of paging button.

<example file="" />

#### Disabled

Using the `Disabled` property, you can manually disable paging.

<example file="" />

#### Icon

The icons of the previous page and the next page can be customized through the `PrevIcon` and `NextIcon` properties.

<example file="" />

#### Length

Use the `Length` property to set the length of **MPagination**. If the number of page buttons exceeds the parent container, the page will be truncated from it.

<example file="" />

#### TotalVisible

You can also manually set the maximum number of visible pages through the `TotalVisible` property.

<example file="" />