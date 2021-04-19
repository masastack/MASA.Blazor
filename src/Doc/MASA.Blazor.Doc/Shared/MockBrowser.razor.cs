using AntDesign;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Shared
{
    public partial class MockBrowser
    {
        private ClassMapper ClassMapper { get; set; } = new ClassMapper();

        [Parameter]
        public int Height { get; set; }

        [Parameter]
        public string WithUrl { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            ClassMapper.Add("browser-mockup")
                .If("with-url", () => WithUrl != null);
        }
    }
}
