using BlazorComponent;

namespace MASA.Blazor
{
    internal class MListGroupItem : MListItem
    {
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Attributes["role"] = "button";
            Attributes["aria-expanded"] = IsActive.ToString();
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-group__header");
                });
        }

        protected override void OnAfterRender(bool firstRender)
        {
        }
    }
}