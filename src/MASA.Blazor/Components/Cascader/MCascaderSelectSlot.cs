using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascaderSelectSlot : BCascaderSelectSlot
    {
        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public double Left { get; set; }

        [Parameter]
        public double Top { get; set; }

        [Parameter]
        public double? Height { get; set; }

        [Parameter]
        public EventCallback<MCascaderSelectOption> OnOptionSelect { get; set; }

        [Parameter]
        public BCascaderNode SelectNode { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BCascaderSelectSlot>(cssBuilder =>
                {
                    cssBuilder
                      .Add("m-application");
                }, styleBuilder =>
                 {
                     styleBuilder
                         .Add("display:inline-block")
                         .Add("vertical-align: top")
                         .Add("min-width: 180px")
                         .Add("background-color: white")
                         .AddIf($"height:{Height}px", () => Height != null)
                         .AddIf("border-right: 1px solid #f0f0f0", () => ShowSubItems);
                 });

            SlotProvider
                .Apply<BList, MList>()
                .Apply<BListItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";
                })
                .Apply<BSelectOption<BCascaderNode>, MCascaderSelectOption>(props =>
                {
                    props[nameof(MCascaderSelectOption.OnOptionClick)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
                     {
                         if (option.Value.Children != null && option.Value.Children.Count > 0)
                         {
                             ShowSubItems = true;
                             SubItems = option.Value.Children;
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
                .Apply<BCascaderSelectSlot, MCascaderSelectSlot>(props =>
                {
                    props[nameof(Visible)] = ShowSubItems;
                    props[nameof(Items)] = SubItems;
                    props[nameof(SelectNode)] = SelectNode;
                    props[nameof(OnOptionSelect)] = OnOptionSelect;
                    props[nameof(Height)] = Height;
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (ShowSubItems && Height == null)
            {
                var rect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
                Height = rect.Height;

                StateHasChanged();
            }
        }
    }
}
