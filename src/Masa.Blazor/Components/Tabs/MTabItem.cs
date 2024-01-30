﻿namespace Masa.Blazor
{
    public partial class MTabItem : MWindowItem, ITabItem
    {
        [CascadingParameter]
        public MTabs? Tabs { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Tabs?.RegisterTabItem(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Tabs?.CallSlider();
            }
        }

        protected override ValueTask DisposeAsyncCore()
        {
            Tabs?.UnregisterTabItem(this);

            return base.DisposeAsyncCore();
        }
    }
}