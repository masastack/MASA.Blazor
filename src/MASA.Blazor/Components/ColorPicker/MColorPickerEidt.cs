using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MColorPickerEdit : BColorPickerEdit, IColorPickerEdit
    {
        [Parameter]
        public EventCallback<ColorPickerColor> OnColorUpdate { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply("edit", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-color-picker__edit");
                })
                .Apply("input", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-color-picker__input");
                });

            AbstractProvider
                .ApplyColorPickerEditDefault();
        }

        public override Task HandleOnChange(ChangeEventArgs args)
        {
            if (ColorType == ColorTypes.HEX)
            {
                if (OnColorUpdate.HasDelegate)
                {
                    var hexValue = ColorUtils.ParseHex(args.Value.ToString());
                    OnColorUpdate.InvokeAsync(ColorUtils.FromHexa(hexValue));
                }
            }

            return base.HandleOnChange(args);
        }
    }
}
