---
order: 0
title: Accessibility (a11y)
---

Web accessibility** (a11y for short)** is an inclusive practice designed to ensure that there are no barriers that prevent persons with disabilities from interacting with or accessing World Wide Web sites. The MASA Blazor component is built to provide keyboard interaction for all mouse-based operations and utilize HTML5 semantic elements where applicable.

## Material Design Accessibility

Google provides a detailed overview of how their components were created in a11y. They also provide some examples of how to ensure that best practices are used when creating applications (outside of the scope of MASA Blazor's default support). You can find more information about [Implementing Accessibility](https://material.io/design/usability/accessibility.html#understanding-accessibility) on the specification site.

## Activator slots

MASA Blazor uses activator slots for many components, such as `MMenu`, `MDialog`, etc. In some cases, these activator elements should have specific a11y attributes, linking them to the corresponding content. In order to achieve this, we pass the necessary a11y options through the slots scope.

```html
<!-- MASA Blazor Template HTML Markup -->
<MMenu>
    <ActivatorContent>
        <MButton @attributes="@context.Attrs">
            Click Me
        </MButton>
    </ActivatorContent>
</MMenu>

<MDialog>
    <ActivatorContent>
        <MButton @attributes="@context.Attrs">
            Click Me
        </MButton>
    </ActivatorContent>
</MDialog>
```
## Item slots

In some cases, you need to use slots for components. As shown in the `MMenu` example above. However, there are other more complex components, and you need to bind properties to the correct component to ensure proper support.

### MSelect

The `MSelect` component will automatically configure all required a11y attributes. Each project will generate corresponding `id`, `selected`, etc. by default:

```html
<!-- MASA Blazor Template HTML Markup -->
<MSelect @bind-Value="_value" 
         Items="_paddingSize" 
         TItem="_items" 
         TItemValue="string" 
         TValue="string" 
         ItemValue="r => r" 
         ItemText="r => r" 
         Label="@("Items")">
</MSelect>

@code
{
    private string _value;
    private List<string> _items = new(){ "a", "b", "c" };
}
```

When rendering, the content of the `MSelect` component will be similar to:

```html
<div
  class="m-input m-input--is-label-active m-input--is-dirty theme--light m-text-field m-text-field--is-booted m-select theme--light"
  id="B-b6a54e78-1abf-46c5-a381-a58827a3776c"
  _bl_c9d72053-e5da-4b7e-aeb6-aaeee3a699dc=""
>
  <div class="m-input__control">
    <div class="m-input__slot" _bl_58c38725-1928-4a47-baba-e8247d3d6bb7="">
      <div class="m-select__slot">
        <label
          class="m-label m-label--active theme--light"
          style="left: 0px; right: auto; position: absolute"
          for="B-b6a54e78-1abf-46c5-a381-a58827a3776c"
          >Items</label
        >
        <div class="m-select__selections">
          <input
            type="text"
            readonly=""
            _bl_1d417625-f630-4fb7-b247-6b6287e2ac6f=""
          />
        </div>
        <div class="m-input__append-inner">
          <div class="m-input__icon m-input__icon--append">
            <button
              type="button"
              class="m-icon m-icon--link theme--light mdi mdi-menu-down"
              _bl_3f06bc12-b560-496a-8d95-f050f3e672c2=""
            ></button>
          </div>
        </div>
        <input type="hidden" value="a" />
      </div>
      <div class="m-menu" _bl_2cc477be-9fe1-4957-9480-2d07b56c88f9=""></div>
    </div>
    <div class="m-text-field__details">
      <div class="m-messages theme--light">
        <div class="m-messages__wrapper"></div>
      </div>
    </div>
  </div>
</div>

```

