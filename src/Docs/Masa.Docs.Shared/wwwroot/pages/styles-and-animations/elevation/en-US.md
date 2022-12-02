# Elevation

This tool allows you to control the relative depth, or distance, between the two planes along the z-axis. There are 25 heights in total. You can set the elevation of an element by using the `elevation-{n}` class, where `n` is an integer between 0-24 corresponding to the desired altitude.

## Usage

The **Elevation** helper class allows you to assign a custom z-deep to any element.

<masa-example file="Examples.styles_and_animations.elevation.Basic"></masa-example>

## Examples

### Props

#### Dynamic elevation

Many components are mixed with `elevation` to obtain `elevation` prop. For unsupported components, you can dynamically change the class.

<masa-example file="Examples.styles_and_animations.elevation.Attributes"></masa-example>

