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
                });
        }
        //public static ComponentCssProvider ApplyBLoadableProgressDefault(this ComponentCssProvider cssProvider)
        //{
        //    return cssProvider.Apply("progress", cssBuilder =>
        //    {
        //        cssBuilder.Add("m-card__progress");
        //    });
        //}
    }
}
