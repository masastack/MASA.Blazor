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
    }
}
