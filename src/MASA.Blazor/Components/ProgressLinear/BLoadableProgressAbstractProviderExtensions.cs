using BlazorComponent;
using System;
using System.Collections.Generic;

namespace MASA.Blazor
{
    public static class BLoadableProgressAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyBLoadableProgressDefault(this ComponentAbstractProvider abstractProvider,ILoadable loadable)
        {
            return abstractProvider
                .Apply(typeof(BLoadableProgress<>), typeof(BLoadableProgress<ICard>))
                .Apply<BProgressLinear, MProgressLinear>(props => 
                {
                     
                    foreach(var (key,value) in loadable.GenProgress())
                    {
                        props[key] = value;
                    }
                    //var loading = loadable.Loading;
                    //props[nameof(MProgressLinear.Color)] = (loading==null || loading == true || loading == "") ? "primary" : loading.ToString();
                    //props[nameof(MProgressLinear.Height)] = loadable.LoaderHeight;
                });
        }
        public static ComponentCssProvider ApplyBLoadableProgressDefault(this ComponentCssProvider cssProvider)
        {
            return cssProvider.Apply("progress", cssBuilder =>
            {
                cssBuilder.Add("m-card__progress");
            });
        }
    }
}
