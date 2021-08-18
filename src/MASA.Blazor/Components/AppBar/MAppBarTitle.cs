using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MAppBarTitle : BLabel
    {
        protected override void SetComponentClass()
        {
            CssProvider.Apply(cssBuilder =>
            {
                cssBuilder.Add("m-toolbar__title");
            });
        }
    }
}
