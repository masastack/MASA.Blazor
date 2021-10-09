using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public class MSlideItem : BItem
    {
        public MSlideItem() : base(GroupType.SlideGroup)
        {
        }

        protected override Task ToggleItem()
        {
            (ItemGroup as BSlideGroup).SetWidths();

            return base.ToggleItem();
        }
    }
}