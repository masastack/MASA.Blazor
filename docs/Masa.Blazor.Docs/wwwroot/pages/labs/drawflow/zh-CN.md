---
title: Drawflow（流程）
desc: "一个基于 [Drawflow](https://github.com/jerosoler/Drawflow) 的流程组件。"
tag: "JS代理"
---

在使用之前你必须引入以下文件：

```html
<link href="https://cdn.masastack.com/npm/drawflow/0.0.59/drawflow.min.css" rel="stylesheet"/>

<script src="https://cdn.masastack.com/npm/drawflow/0.0.59/drawflow.min.js"></script>
```

## 使用

<masa-example file="Examples.labs.drawflow.Usage"></masa-example>


## 最佳实践

> 推荐与 [Blazor自定义元素](https://learn.microsoft.com/zh-cn/aspnet/core/blazor/components/js-spa-frameworks?view=aspnetcore-7.0#blazor-custom-elements) 一起使用。

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
    public string? Value { get; set; } // Value 来自 drawflow df-data 定义的 JSON 字符串。

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