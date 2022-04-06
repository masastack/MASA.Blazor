using System;
using System.Collections.Generic;

namespace Masa.Blazor.Doc.Models
{
    public class DemoComponentModel : DemoFrontMatter
    {
        public string Desc { get; set; }

        public int Order { get; set; }

        public Dictionary<string, string> OtherDocs { get; set; }

        public DateTime LastWriteTime { get; set; }

        public List<DemoItemModel> DemoList { get; set; } = new();

        public List<DemoComponentModel> Children { get; set; } = new();

        public DemoComponentModel()
        {
        }

        public DemoComponentModel(DemoFrontMatter matter, string desc)
        {
            Category = matter.Category;
            Cols = matter.Cols;
            Cover = matter.Cover;
            Desc = desc;
            Related = matter.Related;
            Subtitle = matter.Subtitle;
            Title = matter.Title;
            Type = matter.Type;
        }

        public DemoComponentModel(DemoFrontMatter matter, string desc, Dictionary<string, string> otherDocs)
        {
            Category = matter.Category;
            Cols = matter.Cols;
            Cover = matter.Cover;
            Desc = desc;
            OtherDocs = otherDocs;
            Related = matter.Related;
            Subtitle = matter.Subtitle;
            Title = matter.Title;
            Type = matter.Type;
        }
    }

    public class DemoItemModel
    {
        public decimal Order { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public string Style { get; set; }

        public int? Iframe { get; set; }

        public bool? Docs { get; set; }

        public bool Debug { get; set; }

        public DemoGroup Group { get; set; }

        public DemoItemModel()
        {
        }

        public static DemoItemModel GenerateExample(string lang)
        {
            var title = lang == "zh-CN" ? "示例" : "Examples";
            return new DemoItemModel() { Title = title };
        }

        public static DemoItemModel GenerateProps(string lang)
        {
            var title = lang == "zh-CN" ? "属性" : "Props";
            return new DemoItemModel { Title = title };
        }

        public static DemoItemModel GenerateEvents(string lang)
        {
            var title = lang == "zh-CN" ? "事件" : "Events";
            return new DemoItemModel { Title = title };
        }

        public static DemoItemModel GenerateContents(string lang)
        {
            var title = lang == "zh-CN" ? "插槽" : "Contents";
            return new DemoItemModel { Title = title };
        }

        public static DemoItemModel GenerateMisc(string lang)
        {
            var title = lang == "zh-CN" ? "其他" : "Misc";
            return new DemoItemModel { Title = title };
        }
    }

    public enum DemoGroup
    {
        /// <summary>
        /// 属性
        /// </summary>
        Props = 0,

        /// <summary>
        /// 事件
        /// </summary>
        Events,

        /// <summary>
        /// 插槽
        /// </summary>
        Contents,

        /// <summary>
        /// 其他
        /// </summary>
        Misc,

        /// <summary>
        /// 使用
        /// </summary>
        Usage
    }

    public class ApiItem
    {
        public string Name { get; set; }
        public string Href { get; set; }

        public ApiItem()
        {
        }

        public ApiItem(string name, string href)
        {
            Name = name;
            Href = href;
        }
    }
}