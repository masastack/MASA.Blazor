---
title: Autocompletes
desc: "The MAutocomplete component offers simple and flexible type-ahead functionality. This is useful when searching large sets of data or even dynamically requesting information from an API." 
related:
  - /components/combobox
  - /components/forms
  - /components/selects
---

## Usage

The autocomplete component extends **MSelect** and adds the ability to filter items.

<autocomplete-usage></autocomplete-usage>

## API

- [MAutocomplete](/api/MAutocomplete)

## Caveats

<!--alert:error--> 
When using objects for the **Items** prop, you must associate **ItemText** and **ItemValue** with existing properties on your objects. These values are defaulted to  **Text** and **Value** and can be changed.
<!--/alert:error--> 

<!--alert:warning--> 
The **Auto** property of **MenuProps** is only supported for the default input style.
<!--/alert:warning--> 

<!--alert:info--> 
Browser autocomplete is set to off by default, may vary by browser and may be ignored.  **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 

## Examples

### Props

#### Dense

You can use `dense` prop to reduce autocomplete height and lower max height of list items.

<example file="" />

#### Filter

The `filter` prop can be used to filter each individual item with custom logic. In this example we filter items by name.

<example file="" />

### Contents

#### ItemAndSelection

With the power of slots, you can customize the visual output of the select. In this example we add a profile picture for both the chips and list items.

<example file="" />

### Misc

#### ApiSearch

Easily hook up dynamic data and create a unique experience. The MAutocomplete's expansive prop list makes it easy to fine tune every aspect of the input.

<example file="" />

#### AsynchronousItems

Sometimes you need to load data externally based upon a search query. Use the 'search-input' prop with the **.sync** modifier when using the autocomplete prop. We also make use of the new 'cache-items' prop. This will keep a unique list of all items that have been passed to the items prop and is **REQUIRED** when using asynchronous items and the **multiple** prop.

<example file="" />

#### CryptocurrencySelector

The `MAutocomplete` component is extremely flexible and can fit in just about any use-case. Create custom displays for no-data, item and selection slots to provide a unique user experience. Using slots enables you to easily customize the desired look for your application.

<example file="" />

#### StateSelector

Using a combination of MAutocomplete slots and transitions, you can create a stylish toggleable autocomplete field such as this state selector.

<example file="" />

