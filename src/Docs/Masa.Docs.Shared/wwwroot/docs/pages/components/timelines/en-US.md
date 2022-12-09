---
category: Components
type: Timeline
title: Timelines
cols: 1
related:
  - /components/cards
  - /components/icons
  - /components/grid-system
---

# Timelines

MTimeline is very useful for displaying chronological information.

## Usage

**MTimeline** displays a vertical time axis in its simplest form, and it should contain at least one **MTimelineItem**.

<timelines-usage></timelines-usage>

## API

- [MTimeline](/api/MTimeline)
- [MTimelineItem](/api/MTimelineItem)

## Examples

### Props

#### Color

**Color** prop Colored points can create visual breakpoints, making your timeline easier to read.

<example file="" />

#### Dense

The **Dense** timeline puts everything on the right. In this example, **MAlert** replaces the card to provide a different
design.

<example file="" />

#### Icon dots

Conditionally use icons within the **MTimelineItem**'s dot to provide additional context.

<example file="" />

#### Reverse

You use the `Reverse` attribute to determine the direction of the timeline item. This can work in both the default
mode and the `Dense` mode.

<example file="" />

#### Small

The `Small` attribute allows different styles to provide unique designs.

<example file="" />

### Contents

#### IconContent

Insert avatars into dots with use of the `IconContent` slot and **MAvatar**.

<example file="" />

#### OppositeContent

The **OppositeContent** slot provides an extra layer of customization in your timeline.

<example file="" />

#### Default

If you place a **MCard** inside of a **MTimelineItem**, a caret will appear on the side of the card.

<example file="" />