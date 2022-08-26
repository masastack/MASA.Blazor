using System.Collections.Generic;

namespace Masa.Blazor.Doc.Models
{
    public class DemoMenuItemModel
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string FullTitle => !string.IsNullOrEmpty(SubTitle) ? $"{Title} ({SubTitle})" : Title;

        public string Type { get; set; }

        public string Group { get; set; }

        public string Url { get; set; }

        public string Cover { get; set; }

        public string Icon { get; set; }

        public string Tag { get; set; }

        public DemoMenuItemModel[] Children { get; set; }

        public List<ContentsItem> Contents { get; set; }
    }
}
