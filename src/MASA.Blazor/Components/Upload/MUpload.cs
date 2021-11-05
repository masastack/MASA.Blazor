using BlazorComponent;
using System.Linq;

namespace MASA.Blazor
{
    public partial class MUpload : BUpload
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(styleAction: style =>
                {
                    style.Add("display: inline-block");
                })
                .Apply("chips-wrapper", css => { }, style =>
                {
                    style.Add("padding: 8px 0");
                });

            AbstractProvider
                .Apply<BList, MList>(prop =>
                {
                    prop[nameof(MList.Style)] = Files.Any() ? "" : "padding:0";
                    prop[nameof(MList.Dense)] = true;
                })
                .Apply<BListItem, MListItem>()
                .Apply<BListItemContent, MListItemContent>()
                .Apply<BListItemIcon, MListItemIcon>()
                .Apply<BListItemTitle, MListItemTitle>()
                .Apply<BListItemAction, MListItemAction>(prop =>
                {
                    prop[nameof(MListItemAction.Style)] = "margin: 0px";
                })
                .Apply<BButton, MButton>()
                .Apply<BIcon, MIcon>()
                .Apply<BHintMessage, MHintMessage>(prop =>
                {
                    prop[nameof(MHintMessage.Style)] = "text-align: right";
                })
                .Apply<BChip, MChip>()
                .Apply<BAvatar, MAvatar>(prop =>
                {
                    prop[nameof(MAvatar.Style)] = "margin-right: 4px";
                })
                .Apply<BAvatar, MListItemAvatar>("list-item")
                .Apply<BTooltip, MTooltip>()
                .Apply(typeof(IImage), typeof(MImage))
                .Apply<BOverlay, MOverlay>()
                .Apply<ICard, MCard>();
        }

        protected override string GetColorCss(bool uploaded) => uploaded ? "" : "red--text";

        protected override string GetListItemStyle(bool uploaded) => $"border: 1px solid {(uploaded ? "lightgrey" : "#F44336")}; border-radius:4px;";

    }
}
