namespace Masa.Blazor.Components.OptInput;

internal record OtpJsResult
{
    public string? Type { get; set; }
    public string? Value { get; set; }
    public int Index { get; set; }
}