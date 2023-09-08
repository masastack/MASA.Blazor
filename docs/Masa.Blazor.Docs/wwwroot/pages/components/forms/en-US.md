---
title: Forms
desc: "When it comes to form validation, MASA Blazor has a multitude of integrations and baked in functionality."
related:
  - /blazor/components/selects
  - /blazor/components/selection-controls
  - /blazor/components/text-fields
---

## Usage

The internal **MForm** component makes it easy to add validation to form input. All input components have a rule prop, which accepts different types of group functions, Boolean values and strings. These allow you to specify that the input is invalid__ or__ Conditions. Whenever the input value is changed, each function in the array will receive a new value and each array element will be scored. If the function or array element returns false or string, and the verification fails, the string value will be displayed as an error message.

<masa-example file="Examples.components.forms.Usage"></masa-example>

## Examples

### Props

#### Rules

Rules allow you to apply custom validation on all form components. These are verified in order, and  maximum  errors will be displayed each time, so please make sure you sort the rules accordingly.

<masa-example file="Examples.components.forms.Rules"></masa-example>

### Misc

#### Validation

Verification can also be triggered through the submit button.

<masa-example file="Examples.components.forms.Validation"></masa-example>

#### Enable I18n

Enable I18n to support multilingual validation messages.How to use `II8n` please jump [I18n](I18n/features/internationalization).

<masa-example file="Examples.components.forms.EnableI18n"></masa-example>

#### Validation with submit & clear

**MForm** component has three functions, which can be accessed by setting ref on the component. Ref allows us to access the internal methods of components, such as `<MForm @ref = "_form" > `_ form.Validate() validates all inputs and returns whether they are valid._form.Reset() clears all inputs and resets validation errors._ form.Resetvalidation() will only reset input validation without changing their state.

<masa-example file="Examples.components.forms.ValidationWithSubmitAndClear"></masa-example>

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
