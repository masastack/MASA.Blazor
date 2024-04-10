---
title: Skeleton loaders（骨架装载器）
desc: "**MSkeletonLoader** 组件是一个多功能工具，可以在一个项目中填充许多角色。 在其核心部分，该组件向用户提供了一个指示，指出某些东西即将出现但尚未可用。 有超过30个预先定义的选项，可以组合成定制的示例。"
related:
  - /blazor/components/cards
  - /blazor/components/progress-circular
  - /blazor/components/buttons
---

## 使用

**MSkeletonLoader** 组件为用户提供了一个内容即将到来/加载的视觉指示器。这比传统的全屏加载器要好。

<skeleton-loaders-usage></skeleton-loaders-usage>

## 示例

### 属性

#### 类型

`Type` 属性用于定义骨架加载程序的类型。类型可以组合以创建更复杂的骨架。例如，卡片类型是图像和标题类型的组合。

<masa-example file="Examples.components.skeleton_loaders.Type"></masa-example>

可以使用以下内置类型：

| 类型                          | 组成                                                                               |
|-----------------------------|----------------------------------------------------------------------------------|
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

#### 加载

如果满足以下条件之一，则认为骨架装载器处于装载状态：

- 不使用默认内容 `ChildContent`
- `Loading` 属性设置为 `true`

<masa-example file="Examples.components.skeleton_loaders.Loading"></masa-example>

#### 模板

禁用动画效果。

<masa-example file="Examples.components.skeleton_loaders.Boilerplate"></masa-example>
