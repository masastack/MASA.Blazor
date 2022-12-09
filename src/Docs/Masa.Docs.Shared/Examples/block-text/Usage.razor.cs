
using Masa.Blazor.Presets;

namespace Masa.Docs.Shared.Examples.block_text;

public class Usage : Masa.Docs.Shared.Components.Usage
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
