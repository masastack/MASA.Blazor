using System.Text.Json.Serialization;

namespace Masa.Blazor;

public class StandaloneThemeData
{
    /// <summary>
    ///  'vs' | 'vs-dark' | 'hc-black' | 'hc-light'
    /// </summary>
    [JsonPropertyName("base")]
    public string Base { get; set; }

    public bool inherit { get; set; }

    public TokenThemeRule[] rules { get; set; }

    public string[] encodedTokensColors { get; set; }

    public Dictionary<string,string> colors { get; set; }
}