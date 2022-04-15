using System.Text.Json;

namespace Masa.Blazor.Components.Editor
{
    public class EditorUploadFileItem
    {
        public string FileName { get; set; }

        public string ObjectURL { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }
    }
}
