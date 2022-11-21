namespace Masa.Docs.Shared.Services
{
    public class BlazorMode
    {
        public const string Server = "server";

        public const string Wasm = "wasm";

        public static string Current { get; internal set; } = null!;
    }
}
