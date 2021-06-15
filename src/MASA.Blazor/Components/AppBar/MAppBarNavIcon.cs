using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
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
                })
                .Apply<BIcon, MIcon>(prop =>
                {

                });

            await base.OnInitializedAsync();
        }
    }
}
