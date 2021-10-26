using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMRoutable
    {
        string ActiveClass { get; }

        bool Append { get; }

        bool Disabled { get; }

        bool? Exact { get; }

        bool ExactPath { get; }

        string ExactActiveClass { get; }

        bool Link { get; }

        object Href { get; }

        object To { get; }

        bool Nuxt { get; }

        bool Replace { get; }

        object Ripple { get; }

        string Tag { get; }

        string Target { get; }

        public bool IsClickable(bool hasClick)
        {
            if (Disabled) return false;
            return IsLink() || hasClick;
        }
        bool IsLink()
        {
            return To is not null || Href is not null || Link;
        }
    }
}
