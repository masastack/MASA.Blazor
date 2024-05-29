namespace Microsoft.AspNetCore.Components.Web
{
    /// <summary>
    /// The extra <see cref="MouseEventArgs"/>.
    /// </summary>
    public class ExMouseEventArgs : MouseEventArgs
    {
        public EventTarget? Target { get; set; }
    }
}
