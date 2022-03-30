namespace Masa.Blazor.Presets
{
    public partial class Message
    {
        public class Model
        {
            public bool Visible { get; set; }

            public AlertTypes Type { get; set; } = AlertTypes.None;

            public int Timeout { get; set; } = 3000;

            public string Content { get; set; }
        }
    }
}
