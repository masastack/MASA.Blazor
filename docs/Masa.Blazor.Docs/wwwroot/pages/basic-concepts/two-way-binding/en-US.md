# Two-way binding {#two-way-data-binding}

## Basic concept {#basic-concept}

`@bind-Value` is a two-way data binding directive provided by Blazor that simplifies the process of associating component parameter values with variables or properties in your code.

> While this article uses `@bind-Value` as an example, two-way binding is not limited to the `Value` property. Blazor supports two-way binding for any property, such as `@bind-Text`, `@bind-Selected`, etc., as long as the component follows the corresponding naming convention.

In MASA Blazor's component design, a component that supports two-way binding typically includes the following three key parameters, which are defined in many input component base classes (such as `MInput`):

- **Value**: A public parameter used to receive data.
  ```csharp
  [Parameter]
  public TValue? Value { get; set; }
  ```

- **ValueChanged**: An `EventCallback` type event that notifies the outside when the value inside the component changes.
  ```csharp
  [Parameter]
  public EventCallback<TValue> ValueChanged { get; set; }
  ```

- **ValueExpression**: An expression tree that is primarily used in form contexts to help the validation system obtain field metadata.
  ```csharp
  [Parameter]
  public Expression<Func<TValue>>? ValueExpression { get; set; }
  ```

`@bind-Value` is essentially syntactic sugar for `Value` and `ValueChanged`. When you use `@bind-Value`, the Blazor compiler automatically handles the binding of these two for you.

### Property naming convention {#property-naming-convention}

Two-way binding follows a specific naming convention:

- If the property name is `X`, the corresponding event callback should be named `XChanged`
- The expression for form validation should be named `XExpression`

For example:

- The `Text` property corresponds to the `TextChanged` event and `TextExpression` expression
- The `Selected` property corresponds to the `SelectedChanged` event and `SelectedExpression` expression

## How it works {#how-it-works}

When you write code like this:

```razor
<MTextField @bind-Value="myVariable" />
```

The Blazor compiler "desugars" it at compile time into something like this:

```razor
<MTextField Value="myVariable" ValueChanged="@((newValue) => myVariable = newValue)" />
```

The workflow is as follows:

1. **Parent component passes value to child component**: The value of `myVariable` is passed to the `Value` parameter of the `MTextField` component, which displays this value.
2. **User interaction triggers update**: When the user enters content in the text field, the `MTextField` component internally detects the change.
3. **Child component passes value back to parent component**: The component calls the `ValueChanged` event, sending the newly input value `newValue` to the parent component.
4. **Parent component updates its state**: The expression `(newValue) => myVariable = newValue` in the `ValueChanged` event executes, updating the value of `myVariable`.

This two-way value transfer mechanism ensures that data between the parent component and child component always stays in sync.

## Usage {#usage}

### Basic usage {#basic-usage}

Apply `@bind-Value` to any MASA Blazor component that supports this feature to achieve two-way binding with C# variables.

```razor
<MCheckbox @bind-Value="isChecked" Label="I agree to the terms"></MCheckbox>
<p>Current status: @(isChecked ? "Agreed" : "Not agreed")</p>

<MTextField @bind-Value="userMessage" Label="Enter message"></MTextField>
<p>Your message: @userMessage</p>

@code {
    private bool isChecked;
    private string? userMessage;
}
```

### Handle change events {#bind-value-after}

Sometimes, you may want to perform certain asynchronous operations after a value is updated, such as calling an API or reloading data. We recommend using the `@bind-Value:after` modifier provided by Blazor.

`@bind-Value:after` allows you to specify a parameterless method (which can be synchronous or an asynchronous Task) that will be called after the value has changed and been assigned to your variable.

```razor
<MCheckbox @bind-Value="agree"
           @bind-Value:after="HandleAgreeUpdated">
</MCheckbox>

@code {
    private bool agree;
    private async Task HandleAgreeUpdated()
    {
        Console.WriteLine($"newValue: {agree}");
    }
}
```

This approach makes the code more concise and ensures that the bound variable has already been assigned the latest value when you execute subsequent logic.

## Common issues and solutions {#common-issues}

### Issue 1: Correct way to execute logic after value changes {#issue1}

**Issue**: I want to execute some logic after a value changes. Should I break down `@bind-Value`?

**Answer**: Not recommended. While you can manually bind `Value` and `ValueChanged`, doing so makes the code verbose and prone to errors.

❌ **Not Recommended**:

```razor
<MTextField Value="myVar" ValueChanged="OnMyVarChanged" />

@code {
    private string? myVar;

    private async Task OnMyVarChanged(string newValue)
    {
        myVar = newValue; // have to manually update the variable
        // ... execute other logic ...
    }
}
```

✅ **Recommended**:

```razor
<MTextField @bind-Value="myVar" @bind-Value:after="DoSomething" />

@code {
    private string? myVar;

    private async Task DoSomething()
    {
        // myVar already has the latest value here
        // ... execute other logic ...
    }
}
```

Using `@bind-Value:after` more clearly expresses your intent—"do something after binding is complete"—and avoids the step of manually updating the state.

### Issue 2: Form validation not working {#issue2}

**Issue**: Why doesn't validation work when using `@bind-Value` in a form?

**Answer**: Make sure your input components are inside a `MForm` or Blazor's `EditForm` component. `@bind-Value` automatically handles the `ValueExpression` parameter in this environment. `ValueExpression` is crucial for the validation system because it tells the validator which property of the model the input box is bound to, allowing it to correctly display error messages from data annotations.

#### Correct way to split @bind-Value when needed

If, for some specific requirements, you must split `@bind-Value` without using `@bind-Value:after`, be sure to provide all three parameters: `Value`, `ValueChanged`, **and** `ValueExpression`. Missing `ValueExpression` will cause form validation to not work properly.

❌ **Incorrect** (Missing ValueExpression):

```razor
<MForm Model="_model">
    <MTextField Value="_model.UserName" 
                ValueChanged="@((val) => { _model.UserName = val; })" />
</MForm>
```

✅ **Correct**:

Or use a local variable for clearer handling:

```razor
<MForm Model="_model">
    <MTextField Value="_model.UserName" 
                ValueChanged="OnUserNameChanged"
                ValueExpression="@(() => _model.UserName)" />
</MForm>

@code {
    private void OnUserNameChanged(string? value)
    {
        _model.UserName = value;
    }
}
```

Remember, `ValueExpression` is key to making the form validation system work. It provides an expression tree that allows the validation system to find the corresponding data annotations and metadata.
