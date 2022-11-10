using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Shared.Examples.icons
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MIcon)) { }

        protected override ParameterList<SelectParameter> GenSelectParameters() => new()
        {
           //{ nameof(MIcon.ChildContent), new SelectParameter(new List<string>() { "mdi-plus", "mdi-minus", "mdi-access-point", "mdi-antenna"}, "mdi-heart") },
           { nameof(MIcon.Color), new SelectParameter(new List<string>() { "red", "orange", "yellow", "green", "blue", "purple" }) },
           { nameof(MIcon.Size), new SelectParameter(new List<string>() { "XSmall", "Small", "Large", "XLarge" }) },
        };

        protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
        {
            { nameof(MIcon.Disabled), new CheckboxParameter("false", true) },
            { nameof(MIcon.Dense), new CheckboxParameter("false", true) },
        };

        protected override RenderFragment GenChildContent() => builder =>
        {
            builder.AddContent(0, "mdi-heart");
            //builder.CloseComponent();
            //builder.OpenComponent<UsageTemplate>(1);
            //builder.CloseComponent();
        };

        protected override object? CastValue(ParameterItem<object?> parameter)
        {
            if (parameter.Value == null)
            {
                return parameter.Value;
            }

            return parameter.Key switch
            {
                nameof(MIcon.Size) => (StringNumber)(double)parameter.Value,
                //nameof(MIcon.ChildContent) => new RenderFragment(builder => builder.AddContent(0, parameter.Value)),
                _ => parameter.Value
            };
        }

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
                //{nameof(MIcon.Icon), "mdi-plus"},
            };
        }
    }
}
