---
title: Autocompletes
desc: "The **MAutocomplete** component offers simple and flexible type-ahead functionality. This is useful when searching large sets of data or even dynamically requesting information from an API." 
related:
  - /blazor/components/combobox
  - /blazor/components/forms
  - /blazor/components/selects
---

## Usage

The autocomplete component extends **MSelect** and adds the ability to filter items.

<autocompletes-usage></autocompletes-usage>

## Caveats

<app-alert type="error" content="When using objects for the `Items` prop, you must associate `ItemText` and `ItemValue` with existing properties on your objects. These values are defaulted to  `Text` and `Value` and can be changed."></app-alert>

<app-alert type="warning" content="The `Auto` property of `MenuProps` is only supported for the default input style."></app-alert>

<app-alert type="info" content="Browser autocomplete is set to off by default, may vary by browser and may be ignored.  **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**"></app-alert>

## Examples

### Props

#### Dense

You can use `dense` prop to reduce autocomplete height and lower max height of list items.

<masa-example file="Examples.components.autocompletes.Dense"></masa-example>

#### Filter

The `filter` prop can be used to filter each individual item with custom logic. In this example we filter items by name.

<masa-example file="Examples.components.autocompletes.Filter"></masa-example>

### Contents

#### ItemAndSelection

With the power of slots, you can customize the visual output of the select. In this example we add a profile picture for both the chips and list items.

<masa-example file="Examples.components.autocompletes.ItemAndSelection"></masa-example>

### Misc

#### ApiSearch

Easily hook up dynamic data and create a unique experience. The MAutocomplete's expansive prop list makes it easy to fine tune every aspect of the input.

<masa-example file="Examples.components.autocompletes.ApiSearch"></masa-example>

#### AsynchronousItems

Sometimes you need to load data externally based upon a search query. Use the 'search-input' prop with the **.sync** modifier when using the autocomplete prop. We also make use of the new 'cache-items' prop. This will keep a unique list of all items that have been passed to the items prop and is **REQUIRED** when using asynchronous items and the **multiple** prop.

<masa-example file="Examples.components.autocompletes.AsynchronousItems"></masa-example>

#### CryptocurrencySelector

The `MAutocomplete` component is extremely flexible and can fit in just about any use-case. Create custom displays for no-data, item and selection slots to provide a unique user experience. Using slots enables you to easily customize the desired look for your application.

<masa-example file="Examples.components.autocompletes.CryptocurrencySelector"></masa-example>

#### StateSelector

Using a combination of MAutocomplete slots and transitions, you can create a stylish toggleable autocomplete field such as this state selector.

<masa-example file="Examples.components.autocompletes.StateSelector"></masa-example>

