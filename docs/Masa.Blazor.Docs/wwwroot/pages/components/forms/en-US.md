---
title: Forms
desc: "When it comes to form validation, MASA Blazor has a multitude of integrations and baked in functionality."
related:
  - /blazor/components/selects
  - /blazor/components/selection-controls
  - /blazor/components/text-fields
---

## Usage

The internal **MForm** component makes it easy to add validation to form inputs. All input components have a `Rules` prop which accepts a array of type `Func<string?, StringBoolean>`. These allow you to specify conditions in which the input is _valid_ or _invalid_. Whenever the value of an input is changed, each function in the array will receive the new value and each array element will be evaluated. If a function or array element returns `false` or a `string`, validation has failed and the string value will be presented as an error message.

<masa-example file="Examples.components.forms.Usage"></masa-example>

## Examples

### Props

#### Rules

Rules allow you to apply custom validation on all form components. These are verified in order, and  maximum  errors will be displayed each time, so please make sure you sort the rules accordingly.

<masa-example file="Examples.components.forms.Rules"></masa-example>

### Misc

#### Model validation

In addition to validating on each input component via the `Rules` prop, you can also validate a single object model.

<masa-example file="Examples.components.forms.Validation"></masa-example>

#### Validation with submit and clear

You can use the methods provided by `Context` in the content of **MForm**, or use the component instance provided by `@ref` outside of **MForm**.

<masa-example file="Examples.components.forms.ValidationWithSubmitAndClear"></masa-example>

#### Enable I18n

Enable [I18n](/blazor/features/internationalization) to support multilingual validation messages.

<masa-example file="Examples.components.forms.EnableI18n"></masa-example>

#### Validate complex type with DataAnnotations 

Use `[ValidateComplexType]` attribute and `<ObjectGraphDataAnnotationsValidator />` to validate complex types.

```xml Project.csproj
<PackageReference Include="Microsoft.AspNetCore.Components.DataAnnotations.Validation" Version="3.2.0-rc1.20223.4" />
```

<masa-example file="Examples.components.forms.ValidateComplexType"></masa-example>

#### Validate with FluentValidation

<app-alert type="warning" content="Validators need to be registered, see [FluentValidation Dependency Injection](https://docs.fluentvalidation.net/en/latest/di.html) for details."></app-alert>

**MForm** supports FluentValidation validation.

<masa-example file="Examples.components.forms.ValidateWithFluentValidation"></masa-example>

#### Parse external validation result

**MForm** supports parsing of `ValidationResult` , which users can use as `FormContext.ParseFormValidation' parameter that displays the validation results in a front-end form, using the validation collection as an example.

<masa-example file="Examples.components.forms.ParseFormValidation"></masa-example>
