---
title: Empty state（空状态）
desc: "**MEmptyState** 组件用于表示某个列表是空的或者搜索结果是空的。"
release: v1.10.0
related:
  - /blazor/components/buttons
  - /blazor/components/icons
  - /blazor/components/avatars
---

## 使用 {#usage}

<masa-example file="Examples.components.empty_states.Usage"></masa-example>

## 示例 {#examples}

### 属性 {#props}

#### 内容 {#content}

有三个主要属性用于配置文本内容：`Headline`、`Title` 和 `Text`。

<masa-example file="Examples.components.empty_states.Content"></masa-example>

#### 媒体 {#media}

添加图标或图片到空状态中，以帮助传达其目的。

<masa-example file="Examples.components.empty_states.Media"></masa-example>

#### 操作 {#actions}

添加操作按钮到空状态中，以便用户可以采取行动。

<masa-example file="Examples.components.empty_states.Actions"></masa-example>

### 插槽 {#contents}

#### ChildContent

默认插槽位于 **Text** 和 **Actions** 之间。

<masa-example file="Examples.components.empty_states.ChildContent"></masa-example>

#### TitleContent

<masa-example file="Examples.components.empty_states.TitleContent"></masa-example>

#### ActionsContent

如果需要自定义操作按钮，可以使用 **ActionsContent**。

<masa-example file="Examples.components.empty_states.ActionsContent"></masa-example>
