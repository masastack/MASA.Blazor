namespace Masa.Docs.Shared.Examples.components.infinite_scroll
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        protected override Type UsageWrapperType => typeof(UsageWrapper);

        public Usage() : base(typeof(MInfiniteScroll)) { }
    }
}
