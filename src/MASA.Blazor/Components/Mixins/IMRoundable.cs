using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public interface IMRoundable 
    {
        StringBoolean Rounded { get; }

        bool Tile { get; }

        string RoundedClasses()
        {
            var composite = new List<string>();
            if (Tile is true)
            {
                composite.Add("rounded-0");
            }
            else if (Rounded == true)
            {
                composite.Add("rounded");
            }
            else if (Rounded == false)
            {
            }
            else if (Rounded is not null)
            {
                var values = Rounded.ToString().Split(" ");
                foreach (var value in values)
                {
                    composite.Add($"rounded-{value}");
                }
            }
            return String.Join(" ", composite);
        }
    }
}
