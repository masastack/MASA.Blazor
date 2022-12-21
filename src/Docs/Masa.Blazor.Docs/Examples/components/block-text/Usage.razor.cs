
using Masa.Blazor.Presets;

namespace Masa.Blazor.Docs.Examples.components.block_text;

[JSCustomElement(IncludeNamespace = true)]
public class Usage : Masa.Blazor.Docs.Components.Usage
{
    public Usage() : base(typeof(PBlockText))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(PBlockText.Primary), DateOnly.FromDateTime(DateTime.Now).ToString() },
            { nameof(PBlockText.Secondary), TimeOnly.FromDateTime(DateTime.Now).ToString() },
        };
    }
}
