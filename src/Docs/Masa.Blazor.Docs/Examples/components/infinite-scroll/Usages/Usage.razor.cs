namespace Masa.Blazor.Docs.Examples.components.infinite_scroll
{
    public class Usage : Masa.Blazor.Docs.Components.Usage
    {
        protected override Type UsageWrapperType => typeof(UsageWrapper);

        public Usage() : base(typeof(MInfiniteScroll)) { }
    }
}
