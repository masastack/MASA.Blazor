---
category: Components
type: Select
title: Selects
cols: 1
related:
  - /components/autocompletes
  - /components/combobox
  - /components/forms
---

The `Select` component is used for collecting user provided information from a list of options.

## API

- [MSelect](/api/MSelect)

## 注意

<!--alert:info--> 
The default setting of browser auto completion is off, which may be changed or ignored by different browsers. **[MDN](https://developer.mozilla.org/en-US/docs/Web/Security/Securing_your_site/Turning_off_form_autocompletion)**
<!--/alert:info--> 

<!--alert:warning--> 
The **Auto** attribute of **menupprops** only supports the default input style.
<!--/alert:warning--> 

<!--alert:error--> 
When using an object as the attribute of **items**, you must use **itemtext** and **itemvalue** to associate with the incoming object. These values are **text** and **value** by default and can be changed.
<!--/alert:error--> 