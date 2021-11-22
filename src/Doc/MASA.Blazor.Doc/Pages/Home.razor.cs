using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Pages
{
    public partial class Home
    {
        //TODO:use i18n
        private bool _isEnglish;

        [Inject]
        public NavigationManager Navigation { get; set; }
    }
}
