using System.Collections.Generic;

namespace Masa.Blazor.Doc.Models
{
    public class ConfigModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Descrption { get; set; }

        public GenerateRuleModel GenerateRule { get; set; }
    }

    public class GenerateRuleModel
    {
        public List<ConfigMenuModel> Menus { get; set; }
    }

    public class ConfigMenuModel
    {
        public string Key { get; set; }

        public List<ConfigMenuDescriptionModel> Descriptions { get; set; }

        public List<ConfigMenuModel> Children { get; set; }
    }

    public class ConfigMenuDescriptionModel
    {
        public string Lang { get; set; }

        public string Description { get; set; }
    }
}
