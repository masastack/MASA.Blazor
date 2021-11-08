using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public class MSlideItem : BItem
    {
        public MSlideItem() : base(GroupType.SlideGroup)
        {
        }

        protected override async Task ToggleItem()
        {
            await base.ToggleItem();

            await (ItemGroup as BSlideGroup)?.SetWidths();
        }
    }
}