namespace Masa.Blazor.Doc.Services
{
    public static class BlazorMode
    {
        public const string Server = "server";

        public const string Wasm = "wasm";

        public static string Current { get; internal set; }
    }
}
