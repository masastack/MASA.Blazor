using System.Globalization;

namespace Masa.Docs.Shared.Pages;

public partial class BlazorIntro
{
    [CascadingParameter(Name = "Culture")]
    private string? AppCulture { get; set; }

    private string? Name { get; set; }

    private string? Password { get; set; }

    private MForm? _form;
    private bool _loading;

    private static CultureInfo zhCn = new("zh-CN");
    private static CultureInfo enUs = new("en-US");

    string Culture => I18n.Culture.Equals(zhCn) ? "CN" : "EN";

    private void CultureChange(string val)
    {
        I18n.SetCulture(val == "CN" ? zhCn : enUs);
    }

    private async Task Commit()
    {
        _loading = true;
        StateHasChanged();

        if (_form!.Validate())
        {
            await Task.Delay(1000);
            await PopupService.EnqueueSnackbarAsync("Commit success", AlertTypes.Success);
        }

        _loading = false;
    }
}
