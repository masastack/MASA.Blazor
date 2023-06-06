using System.Text.Json;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Components;

public partial class HttpRequestDiagram
{
    [Parameter] public string? Value { get; set; }

    private string? _internalValue;
    private bool _drawer;
    private HttpRequestModel _model = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        _internalValue = Value;
    }

    private void OnDblClick()
    {
        _drawer = true;

        if (!string.IsNullOrWhiteSpace(_internalValue))
        {
            var model = JsonSerializer.Deserialize<HttpRequestModel>(_internalValue);
            if (model != null)
            {
                _model = model;
            }
        }
    }

    private async Task OnSave(ModalActionEventArgs e)
    {
        // save to db here...

        _internalValue = JsonSerializer.Serialize(_model);
        _drawer = false;
    }
}

public class HttpRequestModel
{
    public string Method { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Headers { get; set; } = null!;

    public string QueryString { get; set; } = null!;

    public string Cookies { get; set; } = null!;

    public string Response { get; set; } = null!;
}
