---
title: Skeleton loaders
desc: "The `MSkeletonLoader` component is a versatile tool that can fill many roles within a project. At its heart, the component provides an indication to the user that something is coming but not yet available. There are over 30 pre-defined options available that can be combined to make custom examples."
related:
  - /blazor/components/cards
  - /blazor/components/progress-circular
  - /blazor/components/buttons

---

## Usage

The **MSkeletonLoader** component provides a user with a visual indicator that content is coming / loading. This is better received than traditional full-screen loaders.

<skeleton-loaders-usage></skeleton-loaders-usage>

## Examples

### Props

#### Type

The `Type` property is used to define the type of skeleton loader. Types can be combined to create more complex skeletons. For example, the card type is a combination of the image and heading types.

<masa-example file="Examples.components.skeleton_loaders.Type"></masa-example>

The following built-in types can be used:

| Type                          | Composition                                                                      |
|------------------------------|----------------------------------------------------------------------------------|
| actions                     | button@2                                                                         |
| article                     | heading, paragraph                                                               |
| avatar                      | avatar                                                                           |
| button                      | button                                                                           |
| card                        | image, card-heading                                                              |
| card-avatar                 | image, list-item-avatar                                                          |
| card-heading                | heading                                                                          |
| chip                        | chip                                                                             |
| date-picker                 | list-item, card-heading, divider, date-picker-options, date-picker-days, actions |
| date-picker-options         | text, avatar@2                                                                   |
| date-picker-days            | avatar@28                                                                        |
| heading                     | heading                                                                          |
| image                       | image                                                                            |
| list-item                   | text                                                                             |
| list-item-avatar            | avatar, text                                                                     |
| list-item-two-line          | sentences                                                                        |
| list-item-avatar-two-line   | avatar, sentences                                                                |
| list-item-three-line        | paragraph                                                                        |
| list-item-avatar-three-line | avatar, paragraph                                                                |
| paragraph                   | text@3                                                                           |
| sentences                   | text@2                                                                           |
| table                       | table-heading, table-thead, table-tbody, table-tfoot                             |
| table-heading               | heading, text                                                                    |
| table-thead                 | heading@6                                                                        |
| table-tbody                 | table-row-divider@6                                                              |
| table-row-divider           | table-row, divider                                                               |
| table-row                   | table-cell@6                                                                     |
| table-cell                  | text                                                                             |
| table-tfoot                 | text@2, avatar@2                                                                 |
| text                        | text                                                                             |
| divider                     | divider                                                                          |

#### Loading

The skeleton loader is considered to be loading if one of the following conditions is met:

- `ChildContent` is not used
- The `Loading` property is set to `true`

<masa-example file="Examples.components.skeleton_loaders.Loading"></masa-example>

#### Boilerplate

Disable the animation.

<masa-example file="Examples.components.skeleton_loaders.BoilerplateComponent"></masa-example>
