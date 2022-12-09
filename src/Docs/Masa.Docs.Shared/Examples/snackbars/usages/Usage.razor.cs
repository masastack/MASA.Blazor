using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Docs.Shared.Examples.snackbars.Usages;

public class Usage : Components.Usage
{
    protected override Type UsageWrapperType => typeof(UsageWrapper);

    public Usage() : base(typeof(MSnackbar))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.AddContent(0, "Hello, I'm a snackbar");
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MSnackbar.Dark),true},
            {
                nameof(MSnackbar.ActionContent), (RenderFragment)(builder =>
                {
                    builder.OpenComponent<MButton>(0);
                    builder.AddAttribute(1,nameof(MButton.Color),"pink");
                    builder.AddAttribute(2,nameof(MButton.Text),true);
                    //builder.AddAttribute(2,nameof(MButton.OnClick),"");
                    builder.AddChildContent(3, "Close");
                    builder.CloseComponent();
                })
            }
        };
    }
}
