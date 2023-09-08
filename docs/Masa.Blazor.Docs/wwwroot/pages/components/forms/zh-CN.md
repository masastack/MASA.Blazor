---
title: Forms（表单）
desc: "在表单验证方面，**MASA Blazor** 具有大量集成和内置功能。"
related:
  - /blazor/components/selects
  - /blazor/components/selection-controls
  - /blazor/components/text-fields
---

## 使用

内部 **MForm** 组件可以很容易地为表单输入添加验证。 所有输入组件都有一个规则道具，它接受类型函数、布尔值和字符串的混合数组。 这些允许您指定输入有效或无效的条件。 每当输入的值发生更改时，数组中的每个函数都将接收新值并评估每个数组元素。 如果函数或数组元素返回 false 或字符串，则验证失败，字符串值将显示为错误消息。

<masa-example file="Examples.components.forms.Usage"></masa-example>

## 示例

### 属性

#### 规则

规则允许您对所有表单组件应用自定义验证。这些是按顺序验证的，一次最多显示 1 个错误，因此请确保相应地对规则进行排序。

<masa-example file="Examples.components.forms.Rules"></masa-example>

### 其他

#### 验证

验证也可以通过提交按钮触发。

<masa-example file="Examples.components.forms.Validation"></masa-example>

#### 启用I18n

通过 `EnableI18n` 属性启用I18n以支持验证信息多语言。如何使用II8n请跳转I18n。

<masa-example file="Examples.components.forms.EnableI18n"></masa-example>

#### 通过提交和清除进行验证

**MForm** 组件有三个功能，可以通过在组件上设置 `ref` 属性来访问。 `ref` 允许我们访问组件的内部方法，例如 `<MForm @ref="_form">`。 `_form.Validate()` 将验证所有输入并返回验证状态。 `_form.Reset()` 将清除所有输入并重置验证状态。 `_form.ResetValidation()` 将只重置输入验证状态，并不改变验证状态。

<masa-example file="Examples.components.forms.ValidationWithSubmitAndClear"></masa-example>

#### 通过 DataAnnotations 验证复杂类型

使用 `[ValidateComplexType]` 属性和 `<ObjectGraphDataAnnotationsValidator />`，可以验证复杂类型。

```xml Project.csproj
<PackageReference Include="Microsoft.AspNetCore.Components.DataAnnotations.Validation" Version="3.2.0-rc1.20223.4" />
```

<masa-example file="Examples.components.forms.ValidateComplexType"></masa-example>

#### 使用 FluentValidation 验证

**MForm** 支持 **FluentValidation** 验证。

<app-alert type="warning" content="验证器需要注册，详情请查看 [FluentValidation Dependency Injection](https://docs.fluentvalidation.net/en/latest/di.html)。"></app-alert>

<masa-example file="Examples.components.forms.ValidateWithFluentValidation"></masa-example>

#### 解析外部验证结果

**MForm** 支持解析 `ValidationResult`，用户可以将服务端表单验证返回的 `ValidationResult` 作为 `FormContext.ParseFormValidation` 的参数，将验证结果在前端表单展示,以验证集合为示例。

<masa-example file="Examples.components.forms.ParseFormValidation"></masa-example>
