using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public partial class MCard : BCard
    {
        public override void SetComponentClass()
        {
            CssBuilder.Add("m-card").Add("elevation-2");

            CardCoverCssBuilder.Add("m-responsive").Add("m-image__image--cover");

            CoverStyleBuilder.Add("height: 250px;");

            TitleCssBuilder.Add("m-card__title");

            SubTitleCssBuilder.Add("m-card__subtitle");

            TextCssBuilder.Add("m-card__text");

            ActionsCssBuilder.Add("m-card__actions");
        }
    }
}
