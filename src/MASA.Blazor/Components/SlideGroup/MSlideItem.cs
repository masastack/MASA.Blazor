using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public class MSlideItem : BItem
    {
        public MSlideItem() : base(GroupType.SlideGroup)
        {
        }

        protected override async Task ToggleAsync()
        {
            await base.ToggleAsync();
            await (ItemGroup as BSlideGroup)?.SetWidths(Value);
        }
    }
}