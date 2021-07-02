using BlazorComponent.Doc.CLI.Models;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Components
    {
        private DemoComponentModel _demoComponent;

        [Parameter]
        public string Name { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            _demoComponent = await Service.GetComponentAsync(Name);
        }
    }
}
