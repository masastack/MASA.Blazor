using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MChipGroup : MItemGroup
    {
        [Parameter]
        public bool Column { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.InitType(GroupType.ChipGroup);
            await base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            CssProvider
                 .Apply(cssBuilder =>
                 {
                     cssBuilder.Add("m-item-group theme--light m-slide-group m-chip-group")
                      .AddIf("m-chip-group--column ", () => Column);
                 })
                 .Apply("prev", cssBuilder =>
                 {
                     cssBuilder.Add("m-slide-group__prev m-slide-group__prev--disabled");
                 })
                 .Apply("wrapper", cssBuilder =>
                 {
                     cssBuilder.Add("m-slide-group__wrapper");
                 })
                 .Apply("content", cssBuilder =>
                 {
                     cssBuilder.Add("m-slide-group__content");
                 })
                 .Apply("next", cssBuilder =>
                 {
                     cssBuilder.Add("m-slide-group__next m-slide-group__next--disabled");
                 });
        }
    }
}
