namespace Masa.Docs.Shared.Examples.timelines;

public class Usage : Components.Usage
{
    public Usage() : base(typeof(MTimeline))
    {
    }

    protected override RenderFragment GenChildContent() => builder =>
    {
        builder.OpenComponent<MTimelineItem>(0);
        builder.AddChildContent(1, "timeline item");
        builder.CloseComponent();

        builder.OpenComponent<MTimelineItem>(2);
        builder.AddAttribute(3, nameof(MTimelineItem.Class), "text-right");
        builder.AddChildContent(4, "timeline item");
        builder.CloseComponent();

        builder.OpenComponent<MTimelineItem>(5);
        builder.AddChildContent(6, "timeline item");
        builder.CloseComponent();
    };

}
