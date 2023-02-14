using System.Xml.Serialization;

namespace Masa.Blazor.Doc.Models
{
    [XmlRoot(Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [XmlType("urlset")]
    public class Urlset
    {
        [XmlElement("url")]
        public List<Url> Urls { get; set; }
    }
}
