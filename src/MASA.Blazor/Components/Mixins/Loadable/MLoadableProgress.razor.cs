using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public partial class MLoadableProgress:BLoadableProgress<IMLoadable>, IMLoadable
    {
        public StringNumber LoaderHeight => Component.LoaderHeight;

        public string Color => Component.Color;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            AbstractProvider.Apply(typeof(BProgressLinear), typeof(MProgressLinear), props =>
            {
                if (Loading == false) return;
                props.Add("Absolute", true);
                props.Add("Color", (Loading == null || Loading == true || Loading == "") ? (Color ?? "primary") : Loading.ToString());
                props.Add("Height", LoaderHeight);
                props.Add("Indeterminate", true);
            });
        }
    }
}
