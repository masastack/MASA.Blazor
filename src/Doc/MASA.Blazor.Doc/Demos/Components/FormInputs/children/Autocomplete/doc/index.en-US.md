---
category: Components
type: Autocompletes
title: Autocompletes
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/5rWLU27so/Autocomplete.svg
related:
  - /components/combobox
  - /components/forms
  - /components/selects
---

The MAutocomplete component offers simple and flexible type-ahead functionality. This is useful when searching large sets of data or even dynamically requesting information from an API.

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