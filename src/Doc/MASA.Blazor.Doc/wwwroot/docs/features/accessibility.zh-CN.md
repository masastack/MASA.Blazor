---
order: 0
title: 无障碍 (a11y)
---

网络可访问性**(简称a11y)**是一种包容性的实践，旨在确保没有阻碍残疾人与万维网网站互动或访问的障碍。 MASA Blazor组件的构建旨在为所有基于鼠标的操作提供键盘交互，并在适用的情况下利用HTML5语义元素。

## Material Design 无障碍

Google提供了一个详细的概述，介绍了它们的组件是如何在a11y中创建的。 他们还提供了一些示例，说明如何确保在创建应用程序时使用最佳实践（超出了MASA Blazor默认支持的范围）。 您可以在规范站点找到更多关于[实现辅助功能](https://material.io/design/usability/accessibility.html#understanding-accessibility)的信息。

## 激活器插槽

MASA Blazor对许多组件使用激活器插槽，如 `MMenu`, `MDialog` 等等。 在某些情况下，这些激活器元素应该具有特定的 a11y 属性，将它们与相应的内容联系起来。 为了实现这一点，我们通过slots scope传递必要的a11y选项。

```html
<!-- MASA Blazor Template HTML Markup -->
<MMenu>
    <ActivatorContent>
        <MButton @attributes="@context.Attrs">
            点击我
        </MButton>
    </ActivatorContent>
</MMenu>

<MDialog>
    <ActivatorContent>
        <MButton @attributes="@context.Attrs">
            点击我
        </MButton>
    </ActivatorContent>
</MDialog>
```
## 项目插槽

在某些情况下，您需要为组件使用插槽。 就像上面的 `MMenu` 示例所示。 但是，还有其他更复杂的组件，您需要将属性绑定到正确的组件以确保适当的支持。

### MSelect

`MSelect` 组件将自动配置所有需要的 a11y 属性。 每个项目在默认情况下将生成相应的 `id`, `selected`等 ：

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

渲染时， `MSelect` 组件的内容将类似于：

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

