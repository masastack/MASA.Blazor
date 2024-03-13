namespace Masa.Blazor
{
    public partial class MListItemAvatar : MAvatar
    {
        [Parameter] public bool Horizontal { get; set; }

        private Block _block = new("m-list-item");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                _block.Element("avatar")
                    .Modifier(Horizontal)
                    .AddClass("m-avatar-tile", Tile || Horizontal)
                    .GenerateCssClasses()
            );
        }
    }
}