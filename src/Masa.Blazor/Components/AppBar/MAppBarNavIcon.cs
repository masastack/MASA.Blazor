namespace Masa.Blazor
{
    public partial class MAppBarNavIcon : BAppBarNavIcon
    {
        protected async override Task OnInitializedAsync()
        {
            AbstractProvider
                .Apply<BButton, MButton>(prop =>
                {
                    prop[nameof(MButton.Class)] = "m-app-bar__nav-icon";
                    prop[nameof(MButton.Icon)] = true;

                    foreach (var attr in Attributes)
                    {
                        prop[attr.Key] = attr.Value;
                    }
                })
                .Apply<BIcon, MIcon>();

            await base.OnInitializedAsync();
        }
    }
}
