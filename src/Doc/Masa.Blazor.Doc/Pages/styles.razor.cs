﻿using BlazorComponent.I18n;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Masa.Blazor.Doc.Pages
{
    public partial class Styles
    {
        [Inject]
        public DemoService Service { get; set; }

        [CascadingParameter(Name = "Culture")]
        public CultureInfo Culture { get; set; }

        [Parameter]
        public string Name { get; set; }

        private DemoComponentModel _demoComponent;

        private DemoItemModel Usage { get; set; }

        private List<DemoItemModel> PropsList { get; set; }

        private List<DemoItemModel> ContentsList { get; set; }

        private List<DemoItemModel> EventsList { get; set; }

        private List<DemoItemModel> MiscList { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Name.Contains('?'))
            {
                Name = Name.Split("?")[0];
            }

            if (Name.Contains('#'))
            {
                Name = Name.Split("#")[0];
            }

            Service.ChangeLanguage(Culture);
            _demoComponent = await Service.GetStyleAsync(Name);
            if (_demoComponent is null)
                this.ThrowNotFoundException($"Style name:{Name} not found");

            var demos = _demoComponent.DemoList?
                                      .Where(x => !x.Debug && !x.Docs.HasValue)
                                      .OrderBy(x => x.Order)
                                      .ThenBy(r => r.Name) ?? Enumerable.Empty<DemoItemModel>();
            if (demos is null)
                this.ThrowNotFoundException($"Style name:{Name} demoList not found");

            Usage = demos.FirstOrDefault(demo => demo.Group == DemoGroup.Usage);
            PropsList = demos.Where(demo => demo.Group == DemoGroup.Props).ToList();
            EventsList = demos.Where(demo => demo.Group == DemoGroup.Events).ToList();
            ContentsList = demos.Where(demo => demo.Group == DemoGroup.Contents).ToList();
            MiscList = demos.Where(demo => demo.Group == DemoGroup.Misc).ToList();
        }
    }
}
