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

#### Auto label {released-on=v1.7.0}

When `AutoLabel` is `true`, **MForm** will automatically get the value of the `[Display]` or `[DisplayName]` attribute of the form field as the label.
By default, it resolves the `[Display]` attribute, and you can also configure the `AttributeType` to resolve the `[DisplayName]` attribute by setting the `Masa.Blazor.Components.Form.AutoLabelOptions` subcomponent.

<masa-example file="Examples.components.forms.AutoLabel"></masa-example>

#### Validate on {released-on=v1.7.0}

The `ValidateOn` prop is used to specify when to validate the form, with optional values of `Input`, `Blur` and `Submit`.

<masa-example file="Examples.components.forms.ValidateOnDemo"></masa-example>

### Misc

#### Model validation

In addition to validating on each input component via the `Rules` prop, you can also validate a single object model.

<masa-example file="Examples.components.forms.Validation"></masa-example>

#### Validate single field {released-on=v1.6.0}

Validate a single field using the `Validate` method of the **MForm** instance.

<masa-example file="Examples.components.forms.ValidateField"></masa-example>

#### Submit and clear {#submit-and-clear updated-in=v1.6.0}

You can use the methods provided by `Context` in the content of **MForm**, or use the component instance provided by `@ref` outside of **MForm**.

<masa-example file="Examples.components.forms.ValidationWithSubmitAndClear"></masa-example>

#### DataAnnotations with I18n {#enable-i18n updated-in=v1.6.0}

Enable [I18n](/blazor/features/internationalization) to support multilingual validation messages. Locale resources used in the example can be found in [GitHub](https://github.com/masastack/MASA.Blazor/blob/0f4a450479bceb816d58bbbb7b8f8ca7655e2f94/docs/Masa.Docs.Shared/wwwroot/locale/en-US.json#L128).

<app-alert type="warning" content="Only support localization for property names with an index of `0`, such as error messages for `[Range]` may not be localized correctly."></app-alert>

<masa-example file="Examples.components.forms.EnableI18n"></masa-example>

#### Validate with FluentValidation {#fluent-validation}

<app-alert type="warning" content="Validators need to be registered, see [FluentValidation Dependency Injection](https://docs.fluentvalidation.net/en/latest/di.html) for details."></app-alert>

**MForm** supports FluentValidation validation.

<masa-example file="Examples.components.forms.ValidateWithFluentValidation"></masa-example>

#### Parse external validation result {#parse-form-validation}

**MForm** supports parsing of `ValidationResult` , which users can use as `FormContext.ParseFormValidation' parameter that displays the validation results in a front-end form, using the validation collection as an example.

<masa-example file="Examples.components.forms.ParseFormValidation"></masa-example>
