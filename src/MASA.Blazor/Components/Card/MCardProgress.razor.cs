using BlazorComponent;

namespace MASA.Blazor
{
    public class MCardProgress : BCardProgress<IMLoadable>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CssProvider.Apply("progress", cssBuilder =>
            {
                cssBuilder.Add("v-card__progress");
            });

            AbstractProvider.Apply(typeof(BLoadableProgress<>), typeof(MLoadableProgress));
        }
    }
}
