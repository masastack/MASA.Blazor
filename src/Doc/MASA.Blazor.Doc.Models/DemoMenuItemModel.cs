using System.Collections.Generic;

namespace MASA.Blazor.Doc.Models
{
    public class DemoMenuItemModel
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Cover { get; set; }

        public string Icon { get; set; }

        public DemoMenuItemModel[] Children { get; set; }

        public List<ContentsItem> Contents { get; set; }
    }
}
