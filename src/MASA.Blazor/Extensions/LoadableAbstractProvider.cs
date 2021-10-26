using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public static class LoadableAbstractProvider
    {
        public static ComponentAbstractProvider ApplyLoadable(this ComponentAbstractProvider abstractProvider,StringBoolean loading,string color,StringNumber loaderHeight)
        {
          return  abstractProvider
                .Apply(typeof(BLoadableProgress<>), typeof(BLoadableProgress<ILoadable>))
                 .Apply(typeof(BProgressLinear),typeof(MProgressLinear), props =>
                 {
                     props[nameof(MProgressLinear.Absolute)] = true;
                     props[nameof(MProgressLinear.Color)] = (loading == true || loading == "") ? (color ?? "primary") : loading.ToString();
                     props[nameof(MProgressLinear.Height)] = loaderHeight;
                     props[nameof(MProgressLinear.Indeterminate)] = true;
                 });
        }
    }
}
