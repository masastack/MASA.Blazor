using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Components.Editor
{
    public class MEditorUpload
    {
        public string Action { get; set; }
        public string Methods { get; set; } = "POST";
        public string Token { get; set; }
        public string TokenName { get; set; }
        public string Name { get; set; } = "file";
        public string Accept { get; set; } = "image/png, image/gif, image/jpeg, image/bmp, image/x-icon";
        public string PathKey { get; set; } = "path";
    }
}
