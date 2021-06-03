namespace MASA.Blazor.Presets
{
    public partial class Message
    {
        public class Model
        {
            public bool Visible { get; set; }

            public AlertType Type { get; set; } = AlertType.None;

            public int Timeout { get; set; } = 3000;

            public string Content { get; set; }
        }
    }
}
