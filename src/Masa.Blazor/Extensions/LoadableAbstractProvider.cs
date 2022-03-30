namespace Masa.Blazor
{
    public static class LoadableAbstractProvider
    {
        public static ComponentAbstractProvider ApplyLoadable(this ComponentAbstractProvider abstractProvider, StringBoolean loading, string color, StringNumber loaderHeight)
        {
            return abstractProvider
                  .Apply(typeof(BLoadableProgress<>), typeof(BLoadableProgress<ILoadable>))
                   .Apply(typeof(BProgressLinear), typeof(MProgressLinear), attrs =>
                    {
                        attrs[nameof(MProgressLinear.Absolute)] = true;
                        attrs[nameof(MProgressLinear.Color)] = (loading == true || loading == "") ? (color ?? "primary") : loading.ToString();
                        attrs[nameof(MProgressLinear.Height)] = loaderHeight;
                        attrs[nameof(MProgressLinear.Indeterminate)] = true;
                    });
        }
    }
}
