
namespace Masa.Docs.Shared.Examples.virtual_scroll.Usages;

public class Usage : Components.Usage
{
    protected override Type UsageWrapperType => typeof(UsageWrapper);

    public Usage() : base(typeof(MVirtualScroll<string>))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            {nameof(MVirtualScroll<string>.ItemContent),(RenderFragment)(builder =>
                {
                    builder.OpenComponent<MListItem>(0);
                    builder.AddAttribute(1,nameof(MListItem.ChildContent), (RenderFragment)(childBuilder=>
                    {
                        childBuilder.OpenComponent<MListItemAction>(0);
                        childBuilder.AddAttribute(1,nameof(MListItemAction.ChildContent),(RenderFragment)(mliaChildBuilder=>
                        {
                            mliaChildBuilder.OpenComponent<MButton>(0);
                            mliaChildBuilder.AddAttribute(1,nameof(MButton.Fab),true);
                            mliaChildBuilder.AddAttribute(2,nameof(MButton.Small),true);
                            mliaChildBuilder.AddAttribute(3,nameof(MButton.Depressed),true);
                            mliaChildBuilder.AddAttribute(4,nameof(MButton.Color),"primary");
                            mliaChildBuilder.CloseComponent();
                        }));
                         childBuilder.CloseComponent();

                         childBuilder.OpenComponent<MListItemContent>(2);
                         childBuilder.AddAttribute(3,nameof(MListItemContent.ChildContent),(RenderFragment)(mlicChildBuilder=>{
                             mlicChildBuilder.OpenComponent<MListItemTitle>(0);
                             mlicChildBuilder.AddContent(1,"User Database Record");
                             mlicChildBuilder.OpenElement(2,"strong");
                             mlicChildBuilder.AddChildContent(3,"ID");
                             mlicChildBuilder.CloseComponent();
                         }));

                        childBuilder.OpenComponent<MListItemAction>(2);
                        childBuilder.AddAttribute(3,nameof(MListItemAction.ChildContent),(RenderFragment)(mliaChildBuilder=>{
                            mliaChildBuilder.OpenComponent<MIcon>(0);
                            mliaChildBuilder.AddAttribute(1,nameof(MIcon.Small),true);
                            mliaChildBuilder.AddChildContent(2,"mdi-open-in-new");
                            mliaChildBuilder.CloseComponent();
                        }));

                        childBuilder.CloseComponent();
                    }));

                     builder.CloseComponent();

                     builder.OpenComponent<MDivider>(2);
                     builder.CloseComponent();
                }) }
        };
    }
}
