namespace Masa.Blazor.Doc.Models
{
    public class ContentsItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public ContentsItem()
        {
        }

        public ContentsItem(string title, string id, int level)
        {
            Id = id;
            Title = title;
            Level = level;
        }

        public static ContentsItem GenerateExample(string lang)
        {
            var title = lang == "zh-CN" ? "示例" : "Examples";
            return new ContentsItem(title, "examples", 2);
        }

        public static ContentsItem GenerateProps(string lang)
        {
            var title = lang == "zh-CN" ? "属性" : "Props";
            return new ContentsItem(title, "props", 3);
        }

        public static ContentsItem GenerateEvents(string lang)
        {
            var title = lang == "zh-CN" ? "事件" : "Events";
            return new ContentsItem(title, "events", 3);
        }

        public static ContentsItem GenerateContents(string lang)
        {
            var title = lang == "zh-CN" ? "插槽" : "Contents";
            return new ContentsItem(title, "contents", 3);
        }

        public static ContentsItem GenerateMisc(string lang)
        {
            var title = lang == "zh-CN" ? "其他" : "Misc";
            return new ContentsItem(title, "misc", 3);
        }
    }
}