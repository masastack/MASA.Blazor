---
category: Components
type: Buttons
title: Buttons
cols: 1
cover: https://gw.alipayobjects.com/zos/alicdn/fNUKzY1sk/Buttons.svg
---

The `MButton` component replaces the standard html button with a material design theme and a multitude of options. Any color
helper class can be used to alter the background or text color. 

## API

- [MButton](/docs/api/MButton)
- [MButtonGroup](/docs/api/MButtonGroup)

## Caveats

<!--alert:warning--> 
`MButton` is the only component that behaves differently when using the **Dark** prop. Normally components use the **Dark** prop to denote that they have a dark colored background and need their text to be white. While this will work
for `MButton`, it is advised to only use the prop when the button **IS ON** a colored background due to the disabled state
blending in with white backgrounds. If you need white text, simply add the `white--text` class.
