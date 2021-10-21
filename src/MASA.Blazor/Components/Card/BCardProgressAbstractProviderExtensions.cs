using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public static class BCardProgressAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyBCardProgressDefault(this ComponentAbstractProvider abstractProvider, ILoadable loadable)
        {
            abstractProvider.Apply(typeof(BCardProgress<>), typeof(BCardProgress<ICard>))
                            .ApplyBLoadableProgressDefault(loadable);
            return abstractProvider;
        }
    }
}
