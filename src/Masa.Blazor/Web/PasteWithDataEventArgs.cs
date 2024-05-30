namespace Microsoft.AspNetCore.Components.Web
{
    public class PasteWithDataEventArgs : ClipboardEventArgs
    {
        public string? PastedData { get; set; }
    }
}
