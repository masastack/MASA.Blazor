using System.Xml.Serialization;

namespace Masa.Blazor.Doc.Models
{
    [XmlType("url")]
    public class Url
    {
        public string loc { get; set; }
    }
}
