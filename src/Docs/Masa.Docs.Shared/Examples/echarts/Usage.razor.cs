using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Shared.Examples.echarts
{
    public class Usage : Masa.Docs.Shared.Components.Usage
    {
        public Usage() : base(typeof(MECharts)) { }

        protected override Dictionary<string, object>? GenAdditionalParameters()
        {
            return new Dictionary<string, object>()
            {
                 { nameof(MECharts.Option), _option },
            };
        }

        private object _option = new
        {
            Title = new
            {
                Left = "center",
                Text = "Getting started"
            },
            Tooltip = new { },
            Legend = new
            {
                Data = new[] { "Sales" }
            },
            XAxis = new
            {
                Data = new[] { "Shirt", "Cardigan", "Chiffon shirt", "Pants", "High heel", "Sock" }
            },
            YAxis = new { },
            Series = new[]
            {
                new
                {
                    Name = "sales",
                    Type= "bar",
                    Data= new []{ 5, 20, 36, 10, 10, 20 }
                }
            }
        };
    }
}
