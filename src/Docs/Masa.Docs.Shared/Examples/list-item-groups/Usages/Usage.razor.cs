namespace Masa.Docs.Shared.Examples.list_item_groups
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override Type UsageWrapperType => typeof(UsageWrapper);

        public Usage() : base(typeof(MListItemGroup)) { }

        protected override RenderFragment GenChildContent() => builder =>
        {
            int i = 0;
            foreach (var item in items)
            {
                builder.OpenComponent<MListItem>(i);
                i++;
                builder.AddAttribute(i, nameof(MListItem.ChildContent), (RenderFragment)(childBuilder =>
                {
                    childBuilder.OpenComponent<MListItemIcon>(0);
                    childBuilder.AddAttribute(1, nameof(MListItemIcon), (RenderFragment)(mliiChildBuilder =>
                    {
                        mliiChildBuilder.OpenComponent<MIcon>(0);
                        mliiChildBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(b => b.AddContent(0, $"{item.Icon}")));
                        mliiChildBuilder.CloseComponent();
                    }));
                    childBuilder.CloseComponent();

                    childBuilder.OpenComponent<MListItemContent>(2);
                    childBuilder.AddAttribute(3, nameof(MListItemContent), (RenderFragment)(mlicChildBuilder =>
                    {
                        mlicChildBuilder.OpenComponent<MListItemTitle>(0);
                        mlicChildBuilder.AddChildContent(1, $"{item.Text}");
                        mlicChildBuilder.CloseComponent();
                    }));
                    childBuilder.CloseComponent();

                }));

                builder.CloseComponent();

                i++;
            }
        };

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
                { nameof(MListItemGroup.Value), selected },
              
            };
        }

        static readonly Item[] items =
        {
            new()
            {
                Icon = "mdi-inbox",
                Text = "Inbox"
            },
            new()
            {
                Icon = "mdi-star",
                Text = "Star"
            },
            new()
            {
                Icon = "mdi-send",
                Text = "Send"
            },
            new()
            {
                Icon = "mdi-email-open",
                Text = "Drafts"
            }
        };

        StringNumber selected = 0;

        public class Item
        {
            public string Icon { get; set; }
            public string Text { get; set; }
        }
    }
}
