namespace Masa.Blazor
{
    public partial class MListItemAvatar : MAvatar
    {
        [Parameter]
        public bool Horizontal { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-list-item__avatar";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--horizontal", () => Horizontal)
                        .AddIf("m-avatar-tile", () => Tile || Horizontal);
                });
        }
    }
}
