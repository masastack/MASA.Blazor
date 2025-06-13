# 双向绑定 {#two-way-data-binding}

## 基本概念 {#basic-concept}

`@bind-Value` 是 Blazor 提供的一种双向数据绑定指令，它简化了将组件的参数值与代码中的变量或属性关联起来的过程。

> 虽然本文以 `@bind-Value` 为例，但双向绑定不限于 `Value` 属性。Blazor 支持任何属性的双向绑定，如 `@bind-Text`、
`@bind-Selected` 等，只要组件遵循相应的命名约定。

在 MASA Blazor 的组件设计中，一个支持双向绑定的组件通常会包含以下三个关键参数，这些参数在许多输入组件的基类（如 `MInput`
）中定义：

- **Value**：一个公开参数，用于接收数据。
  ```csharp
  [Parameter]
  public TValue? Value { get; set; }
  ```

- **ValueChanged**：一个 `EventCallback` 类型的事件回调，当组件内部的值发生变化时，通过它通知外部。
  ```csharp
  [Parameter]
  public EventCallback<TValue> ValueChanged { get; set; }
  ```

- **ValueExpression**：一个表达式树，它主要在表单上下文中使用，帮助验证系统获取字段的元数据。
  ```csharp
  [Parameter]
  public Expression<Func<TValue>>? ValueExpression { get; set; }
  ```

`@bind-Value` 本质上是 `Value` 和 `ValueChanged` 的语法糖。当您使用 `@bind-Value` 时，Blazor 编译器会自动为您处理这两者的绑定。

### 属性命名约定 {#property-naming-convention}

双向绑定遵循特定的命名约定：

- 如果属性名为 `X`，则相应的事件回调应命名为 `XChanged`
- 用于表单验证的表达式应命名为 `XExpression`

例如：

- `Text` 属性对应 `TextChanged` 事件和 `TextExpression` 表达式
- `Selected` 属性对应 `SelectedChanged` 事件和 `SelectedExpression` 表达式

## 工作原理 {#how-it-works}

当您编写如下代码时：

```razor
<MTextField @bind-Value="myVariable" />
```

Blazor 编译器在编译时会将其"解糖"（desugar）为类似下面的形式：

```razor
<MTextField Value="myVariable" ValueChanged="@((newValue) => myVariable = newValue)" />
```

工作流程如下：

1. **父组件向子组件传值**：`myVariable` 的值被传递给 `MTextField` 组件的 `Value` 参数，组件显示这个值。
2. **用户交互触发更新**：当用户在文本框中输入内容时，`MTextField` 组件内部会检测到变化。
3. **子组件向父组件回传值**：组件调用 `ValueChanged` 事件，将新输入的值 `newValue` 发送给父组件。
4. **父组件更新自身状态**：`ValueChanged` 事件中的表达式 `(newValue) => myVariable = newValue` 执行，更新 `myVariable` 的值。

这个双向的值传递机制确保了父组件和子组件之间的数据始终保持同步。

## 使用方式 {#usage}

### 基本用法 {#basic-usage}

将 `@bind-Value` 应用于任何支持该功能的 MASA Blazor 组件，即可实现与 C# 变量的双向绑定。

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

### 处理变更事件 {#bind-value-after}

有时，您希望在值更新之后执行某些异步操作，例如调用 API 或重新加载数据。我们推荐使用 Blazor 提供的 `@bind-Value:after` 修饰符。

`@bind-Value:after` 允许您指定一个无参数的方法（可以是同步或异步的 Task），该方法将在值变更并赋给您的变量之后被调用。

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

这种方式代码更简洁，且能确保您在执行后续逻辑时，绑定的变量已经被赋予了最新的值。

## 常见问题与解决方案 {#common-issues}

### 问题一：值变化后执行逻辑的正确方式 {#issue1}

**问题**：我想在值变化后执行一些逻辑，应该拆开 `@bind-Value` 吗？

**回答**：不建议。虽然您可以手动绑定 `Value` 和 `ValueChanged`，但这样做会使代码变得冗长且容易出错。

❌ **不推荐的写法**：

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

✅ **推荐的写法**：

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

使用 `@bind-Value:after` 更清晰地表达了您的意图——"在绑定完成后做某事"，并避免了手动更新状态的步骤。

### 问题二：表单无法验证 {#issue2}

**问题**：在表单中使用 `@bind-Value` 时，验证为什么不生效？

**回答**：请确保您的输入组件位于 `MForm` 或 Blazor 的 `EditForm` 组件内。`@bind-Value` 在这种环境下会自动处理
`ValueExpression` 参数。`ValueExpression` 对于验证系统至关重要，因为它告诉验证器当前输入框绑定到了模型的哪个属性，从而能够正确地显示来自数据注解的错误信息。

#### 当需要拆分 @bind-Value 时的正确写法

如果出于某些特定需求，您必须拆分 `@bind-Value` 而不使用 `@bind-Value:after`，请务必同时提供 `Value`、`ValueChanged` **和**
`ValueExpression` 三个参数。缺少 `ValueExpression` 将导致表单验证无法正常工作。

❌ **错误写法**（缺少 ValueExpression）：

```razor
<MForm Model="_model">
    <MTextField Value="_model.UserName" 
                ValueChanged="@((val) => { _model.UserName = val; })" />
</MForm>
```

✅ **正确写法**：

或者使用局部变量进行更清晰地处理：

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

记住，`ValueExpression` 是表单验证系统工作的关键。它提供了一个表达式树，让验证系统能够找到相应的数据注解和元数据。