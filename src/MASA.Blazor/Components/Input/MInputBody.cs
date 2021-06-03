using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MInputBody : BInputBody
    {
        public int Width => IsActive ? ComputeLabeLength() * 6 : 0;

        private int ComputeLabeLength()
        {
            if (string.IsNullOrEmpty(Label))
            {
                return 0;
            }

            var length = 0;
            for (int i = 0; i < Label.Length; i++)
            {
                if (Label[i] > 127)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }

            return length+1;
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__slot");
                })
                .Apply("legend", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add(() => $"width:{Width}px");
                });

            AbstractProvider
                .Apply<BLabel, MLabel>(properties =>
                {
                    properties[nameof(MLabel.Value)] = Label;
                    properties[nameof(MLabel.Absolute)] = true;
                    properties[nameof(MLabel.IsActive)] = IsActive;
                    properties[nameof(MLabel.IsFocused)] = IsFocused;
                });
        }
    }
}
