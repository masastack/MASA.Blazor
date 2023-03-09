---
title: Timeline
desc: "**MTimeline** is very useful for displaying chronological information."
related:
  - /blazor/components/cards
  - /blazor/components/icons
  - /blazor/components/grid-system
---

## Usage

**MTimeline** displays a vertical time axis in its simplest form, and it should contain at least one **MTimelineItem**.

<masa-example file="Examples.components.timelines.Usage"></masa-example>

## Examples

### Props

#### Color

The full list of options can be found here [monaco-editor](https://microsoft.github.io/monaco-editor/docs.html)。

`Color` prop Colored points can create visual breakpoints, making your timeline easier to read.

<masa-example file="Examples.components.timelines.Color"></masa-example>

#### Dense

The `Dense` timeline puts everything on the right. In this example, **MAlert** replaces the card to provide a different
design.

<masa-example file="Examples.components.timelines.Dense"></masa-example>

#### Icon dots

Conditionally use icons within the **MTimelineItem**'s dot to provide additional context.

<masa-example file="Examples.components.timelines.IconDots"></masa-example>

#### Reverse

You use the `Reverse` attribute to determine the direction of the timeline item. This can work in both the default
mode and the `Dense` mode.

<masa-example file="Examples.components.timelines.Reverse"></masa-example>

#### Small

The `Small` attribute allows different styles to provide unique designs.

<masa-example file="Examples.components.timelines.Small"></masa-example>

### Contents

#### IconContent

Insert avatars into dots with use of the **IconContent** slot and **MAvatar**.

<masa-example file="Examples.components.timelines.IconContent"></masa-example>

#### OppositeContent

The **OppositeContent** slot provides an extra layer of customization in your timeline.

<masa-example file="Examples.components.timelines.OppositeContent"></masa-example>

#### Default

If you place a **MCard** inside of a **MTimelineItem**, a caret will appear on the side of the card.

<masa-example file="Examples.components.timelines.TimelineItemDefault"></masa-example>

### Misc

#### Advanced

Modular components allow you to create highly customized solutions that just work.

<masa-example file="Examples.components.timelines.Advanced"></masa-example>