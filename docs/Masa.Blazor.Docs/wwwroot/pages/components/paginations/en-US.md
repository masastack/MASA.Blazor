---
title: Pagination
desc: "The **MPagination** component is used to separate long data sets so that user data information can be known. Provided the amount, the paging component will automatically scale. The current maintenance page provides value easily."
related:
  - /blazor/components/data-iterators
  - /blazor/components/data-tables
  - /blazor/components/lists
---

## Usage

By default, paging displays the number of pages according to the set `Length` property, and there are `Prev` and `Next` buttons on both sides to help navigation.

<masa-example file="Examples.components.paginations.Usage"></masa-example>

## Examples

### Props

#### Circle

The `Circle` property provides you with another style of paging button.

<masa-example file="Examples.components.paginations.Circle"></masa-example>

#### Disabled

Using the `Disabled` property, you can manually disable paging.

<masa-example file="Examples.components.paginations.Disabled"></masa-example>

#### Href format {released-on=v1.3.0}

Using `HrefFormat` property, you can customize the link format of the pagination button. It's helpful for SEO.

<masa-example file="Examples.components.paginations.HrefFormat"></masa-example>

#### Icon

The icons of the previous page and the next page can be customized through the `PrevIcon` and `NextIcon` properties.

<masa-example file="Examples.components.paginations.Icon"></masa-example>

#### Length

Use the `Length` property to set the length of **MPagination**. If the number of page buttons exceeds the parent container, the page will be truncated from it.

<masa-example file="Examples.components.paginations.Length"></masa-example>

#### Mini variant {released-on=v1.7.0}

By default, when the browser window is less than *600px*, the mini style is automatically used. Use the `MinVariant` property to set the mini style of the pagination.

<masa-example file="Examples.components.paginations.MiniVariant"></masa-example>

#### TotalVisible

You can also manually set the maximum number of visible pages through the `TotalVisible` property.

<masa-example file="Examples.components.paginations.TotalVisible"></masa-example>
