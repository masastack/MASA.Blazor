using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMCard : ICard, IMLoadable<IMCard>, IMRoutable, IMVSheet
    {

        public void SetCardComponentClass()
        {
            CssProvider
               .Apply(cssBuilder =>
               {
                   cssBuilder.Add(() => CardClasses());

               }, styleBuilder =>
               {
                   styleBuilder
                       .Add(() => CardStyles());
               });

            AbstractProvider.Apply(typeof(BCardProgress<,>), typeof(BCardProgress<IMCard,MProgressLinear>));
        }
        public string CardClasses()
        {
            var composite = new List<string>();
            composite.Add("m-card");
            composite.Add(RoutableClasses());
            if (Flat) composite.Add("m-card--flat");
            if (Hover) composite.Add("m-card--hover");
            if (IsClickable(HasClick)) composite.Add("m-card--link");
            if (Loading == true) composite.Add("m-card--loading");
            if (Disabled) composite.Add("m-card--disabled");
            if (Raised) composite.Add("m-card--raised");
            composite.Add(VSheetClasses());

            return String.Join(" ", composite);
        }

        public string CardStyles()
        {
            var styles = VSheetStyles();
            if (string.IsNullOrWhiteSpace(Img) is false) styles += $"background:url(\"{Img}\") center center / cover no-repeat;";
            return styles;
        }
    }
}
