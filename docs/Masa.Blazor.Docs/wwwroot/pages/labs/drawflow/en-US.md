---
title: Drawflow
desc: "A simple flow component base on [Drawflow](https://github.com/jerosoler/Drawflow)."
tag: "JS Proxy"
---

You need to reference the following files before using it:

```html
<link href="https://unpkg.com/drawflow@0.0.59/dist/drawflow.min.css" rel="stylesheet"/>

<script src="https://unpkg.com/drawflow@0.0.59/dist/drawflow.min.js"></script>
```

## Usage

<masa-example file="Examples.labs.drawflow.Usage"></masa-example>

## Best practice

> It is recommended to use with [Blazor custom elements](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/js-spa-frameworks?view=aspnetcore-7.0#blazor-custom-elements).

```razor CustomElement.razor
@using System.Text.Json

@if (_data is null)
{
    <span>There's no data...</span>
}
else
{
    <span style="color: @TextColor">@_data.Text</span>
}

@code {

    [Parameter]
    public string? Value { get; set; } // Value is the JSON string of the drawflow df-data.

    [Parameter]
    public string? TextColor { get; set; }

    private Data? _data;

    protected override void OnInitialized()
    {
        if (!string.IsNullOrEmpty(Value))
            _data = JsonSerializer.Deserialize<Data>(Value)!;
    }

    public class Data
    {
        public string? Text { get; set; }
    }

}
```

```cs Program.cs
builder.Services.AddServerSideBlazor(options =>
{
    options.RootComponents.RegisterCustomElement<CustomElement>("my-custom-element");
});
```

```razor Index.razor
<MDrag DataValue="my-custom-element">My custom element</MDrag>

<MDrawflow OnDrop="@DropAsync" @ref="_drawflow" Style="height: 500px;"></MDrawflow>

@code {

    private MDrawflow? _drawflow;

    private async Task DropAsync(ExDragEventArgs args)
    {
        var customElementName = args.DataTransfer.Data.Value; // from the MDrag component above

        await _drawflow!.AddNodeAsync(customElementName,
            1,
            1,
            args.ClientX,
            args.ClientY,
            args.DataTransfer.Data.OffsetX,
            args.DataTransfer.Data.OffsetY,
            "",
            new { data = JsonSerializer.Serialize(new { Text = "Text set from data" }) },
            $"<{customElementName} text-color='white' df-data></{customElementName}>");
    }

}
```