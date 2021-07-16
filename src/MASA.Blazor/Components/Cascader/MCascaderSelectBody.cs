using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MCascaderSelectBody : BCascaderSelectBody
    {
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public double? Height { get; set; }

        [Parameter]
        public EventCallback<MCascaderSelectOption> OnOptionSelect { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BCascaderSelectBody>(styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add("display:inline-block")
                         .Add("vertical-align: top")
                         .Add("min-width: 180px")
                         .Add("background-color: white")
                         .AddIf($"height:{Height}px", () => Height != null)
                         .AddIf("border-right: 1px solid #f0f0f0", () => ShowSubItems);
                 });

            AbstractProvider
                .Apply<BList, MList>()
                .Apply<BListItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";
                })
                .Apply<BSelectOption<BCascaderNode, string>, MCascaderSelectOption>(props =>
                {
                    props[nameof(MCascaderSelectOption.OnOptionClick)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
                     {
                         if (option.Item.Children != null && option.Item.Children.Count > 0)
                         {
                             ShowSubItems = true;
                             SubItems = option.Item.Children;
                         }
                         else
                         {
                             ShowSubItems = false;

                             if (OnOptionSelect.HasDelegate)
                             {
                                 await OnOptionSelect.InvokeAsync(option);
                             }
                         }
                     });
                })
                .Apply<BCascaderSelectBody, MCascaderSelectBody>(props =>
                {
                    props[nameof(Visible)] = ShowSubItems;
                    props[nameof(Items)] = SubItems;
                    props[nameof(OnOptionSelect)] = OnOptionSelect;
                    props[nameof(Height)] = Height;
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (ShowSubItems && Height == null)
            {
                var rect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);
                Height = rect.ClientHeight;

                StateHasChanged();
            }
        }
    }
}
