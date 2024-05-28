using Element = BemIt.Element;

namespace Masa.Blazor
{
    public partial class MListItemAvatar : MAvatar
    {
        [Parameter] public bool Horizontal { get; set; }

        private static Element _element = new("m-list-item", "avatar");
        private ModifierBuilder _modifierBuilder = _element.CreateModifierBuilder();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (!IsDirtyParameter(nameof(Size)))
            {
                // Use css instead of size prop
                Size = null;
            }
        }

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                new[]
                {
                    _modifierBuilder
                        .Add(Horizontal)
                        .AddClass("m-avatar-tile", Tile || Horizontal)
                        .Build()
                }
            );
        }
    }
}