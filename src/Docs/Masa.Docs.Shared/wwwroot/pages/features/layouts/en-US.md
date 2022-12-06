# Layouts

Build beautiful user experiences with customizable and expansive layout options.

## Usage

MASA Blazor comes with a built-in layout system that just works out of the box. Components such as MAppBar and MFooter upport a special property named app.  This property tells MASA Blazor that the corresponding component is part of your application’s layout. In this section you will learn the basics of how the layout system works, how to combine multiple layout components, and how to change them all dynamically.

There are 2 primary layout components in MASA Blazor， MApp and MMain。The MApp  component is the root of your application，`<MApp>//other layout</MApp>`。The MMain component is a semantic replacement for the main HTML element and the root of your application’s content. When  Blazor mounts to the DOM, any MASA Blazor component that is part of the layout registers its current height and/or width with the framework core. The MMain component then takes these values and adjusts its container size.

To better illustrate this, let’s create a basic Vuetify layout:

```html
<MApp>
  <MMain>
    Hello World
  </MMain>
</MApp>
```

With no layout components in the template, MMain doesn’t need to adjust its size and instead takes up the entire page. Let’s change that by adding a [MAppBar](/components/app-bars)above the MMain element:

```html
<MApp>
  <!-- Must have the app property -->
  <MAppBar App></MAppBar>

  <MMain>
    Hello World
  </MMain>
</MApp>
```

Because we gave MAppBar the app prop, MASA Blazor knows that it is part of the layout. MMain then takes the registered height of our bar and removes the same amount from its available content area—in this case 64px of space is removed from the top of  MMain's container。

As a final touch, let’s add a gutter by wrapping the content in a MContainer component：

```html
<MApp>
  <!-- Must have the app property -->
  <MAppBar App></MAppBar>

  <MMain>
    <MContainer>
      Hello World
    </MContainer>
  </MMain>
</MApp>
```

Up next, we take our newly established baseline and enhance it with new layout components and customization options.

## Combining layout components

More to follow

## Dynamic layouts

More to follow

