# Overflow

Configure how content overflows when it becomes out of container bounds.

### Misc

#### Property

**How to run**

Specify the elements `overflow` and `overflow-x` attributes. These classes can be applied using the following format: `{overflow}-{value}`. Where **reference `** refers to the type: `overflow`, `overflow-x` or `overflow-y`, **value** can be one of the following: `auto`, `hidden` or `visible`.

This is the list of attributes:

* **Overflow Auto**
* **Overflow Hide**
* **Overflow visible**
* **Overflow-x-auto**
* **Overflow-x-hide**
* **Overflow-x-visible**
* **Overflow-y-auto**
* **Overflow-y-hide**
* **Overflow visible**

`overflow-auto` is used to add a scroll bar to the element when the content of the element overflows the border. And `overflow-hidden` is used to clip any content that overflows the boundary. `overflow-visible` will prevent the content from being clipped, even if it overflows the border.

<masa-example file="Examples.styles_and_animations.overflow.Property"></masa-example>

#### XProperty

**overflow-x** can be used to specify horizontal overflows to an element if needed.

<masa-example file="Examples.styles_and_animations.overflow.XProperty"></masa-example>