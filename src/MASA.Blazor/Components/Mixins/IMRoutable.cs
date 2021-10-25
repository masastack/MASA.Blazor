using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMRoutable : IRoutable
    {
        public string RoutableClasses()
        {
            if (To is not null) return "";
            var composite = new List<string>();
            if (ActiveClass is not null) composite.Add(ActiveClass);
            return String.Join(" ", composite);
        }

        public bool IsClickable(bool hasClick)
        {
            if (Disabled) return false;
            return IsLink() || hasClick;
        }
        bool IsLink()
        {
            return To is not null || Href is not null || Link;
        }
        public string RoutableClass()
        {
            if (To is not null) return "";
            return ActiveClass ?? "";
        }
        public (string tag, Dictionary<string, object> props) GenerateRouteLink(string routableClass)
        {
            var tag = (Href is not null ? "a" : Tag ?? "div");
            var props = new Dictionary<string, object>();
            if(tag == "a") props.Add("href",Href);
            props.Add("class", routableClass??RoutableClass());
            return (tag, props);
        }
    }
}
