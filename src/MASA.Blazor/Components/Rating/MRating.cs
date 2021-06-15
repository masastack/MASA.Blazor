using BlazorComponent;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MRating : BRating
    {
        protected async override Task OnInitializedAsync()
        {
            AbstractProvider
                .Apply<BButton, MButton>(prop =>
                {
                    prop[nameof(MButton.Icon)] = true;
                    prop[nameof(MButton.Small)] = Small;
                    prop[nameof(MButton.XLarge)] = XLarge;
                    prop[nameof(MButton.Large)] = Large;
                    prop[nameof(MButton.XSmall)] = XSmall;
                })
                .Apply<BIcon, MIcon>("star-outline", prop =>
                {
                    prop[nameof(MIcon.Color)] = BackgroundColor;
                })
                .Apply<BIcon, MIcon>("star", prop =>
                {
                    prop[nameof(MIcon.Color)] = Color;
                });

            await base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                 {
                     cssBuilder.Add("m-rating");
                 });
        }
    }
}
