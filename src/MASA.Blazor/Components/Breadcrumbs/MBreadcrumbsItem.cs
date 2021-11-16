using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MBreadcrumbsItem : BBreadcrumbsItem, IRoutable
    {
        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string ActiveClass { get; set; } = "m-breadcrumbs__item--disabled";

        [Parameter]
        public bool Append { get; set; }

        [Parameter]
        public bool? Exact { get; set; }

        [Parameter]
        public bool ExactPath { get; set; }

        [Parameter]
        public string ExactActiveClass { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public object To { get; set; }

        [Parameter]
        public bool Nuxt { get; set; }

        [Parameter]
        public bool Replace { get; set; }

        [Parameter]
        public object Ripple { get; set; }


        protected override void OnParametersSet()
        {
            var tag = (Href is not null ? "a" : Tag ?? "div");
            var props = new Dictionary<string, object>();
            if (tag == "a") props.Add("href", Href);
            var composite = new List<string>();
            composite.Add("m-breadcrumbs__item");
            if (Disabled) composite.Add(ActiveClass);
            var routableClass = string.Join(" ", composite);
            props.Add("class", routableClass ?? RoutableClass());

            ChildProps = (tag, props);

            string RoutableClass()
            {
                if (To is not null) return "";
                return ActiveClass ?? "";
            }
        }
    }
}