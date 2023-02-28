---
title: Badges（徽章）
desc: "**MBadge** 组件可以上覆或订阅一个像头像的图标，或者内容上的文本来突出显示用户的信息，或只是提醒注意某个特定元素。 徽章中的内容通常包含数字或图标。"
related:
  - /blazor/components/avatars
  - /blazor/components/icons
  - /blazor/components/toolbars
---

## 使用

最简单形式的徽章显示在它包装的内容的右上角，并且需要徽章插槽。

<badges-usage></badges-usage>

## 示例

### 其他

#### 自定义选项

**MBadge** 组件是灵活的，可以用于众多元素的各种使用案例。 也可以通过 `offset-x` 和 `offset-y` 属性调整位置的选项。

<masa-example file="Examples.components.badges.Customize"></masa-example>

#### 动态通知

你可以将徽章与动态内容合并，以创建系统通知之类的东西。

<masa-example file="Examples.components.badges.DynamicNotification"></masa-example>

#### 鼠标悬停显示

你可以用可见性控制做很多事情。例如，在鼠标悬停时显示徽章。

<masa-example file="Examples.components.badges.Hover"></masa-example>

#### 标签页

徽章可以用各种方式向用户传递信息。

<masa-example file="Examples.components.badges.Tabs"></masa-example>

