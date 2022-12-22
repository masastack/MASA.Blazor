namespace Masa.Blazor.Docs.Examples.components.bottom_navigation
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        // TODO: the text of UserTemplate need to insert to the example code. 

        protected override RenderFragment GenChildContent() => builder =>
        {
            new[] { ("recent", "mdi-history"), ("favorites", "mdi-heart"), ("nearby", "mdi-map-marker") }.ForEach(item =>
            {
                builder.OpenRegion(0);
                builder.OpenComponent<MButton>(1);
                builder.AddAttribute(2, nameof(MButton.Value), (StringNumber)item.Item1);
                builder.AddAttribute(3, nameof(MButton.ChildContent), new RenderFragment(child =>
                {
                    child.OpenElement(0, "span");
                    child.AddContent(1, item.Item1);
                    child.CloseElement();

                    child.OpenComponent<MIcon>(2);
                    child.AddChildContent(3, item.Item2);
                    child.CloseComponent();
                }));
                builder.CloseComponent();
                builder.CloseRegion();
            });
        };

        public Usage() : base(typeof(MBottomNavigation))
        {
        }
    }
}
