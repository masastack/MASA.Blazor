namespace Masa.Blazor
{
    public partial class MMessages : BMessages, IThemeable
    {

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-messages";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(IsDark)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__wrapper");
                })
                .Apply("message", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__message");
                });

            AbstractProvider
                .Apply(typeof(BMessagesChildren<>), typeof(BMessagesChildren<MMessages>))
                .Apply(typeof(BMessagesMessage<>), typeof(BMessagesMessage<MMessages>));
        }
    }
}
