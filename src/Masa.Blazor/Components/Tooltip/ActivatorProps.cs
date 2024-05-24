namespace BlazorComponent
{
    public class ActivatorProps
    {
        public ActivatorProps(Dictionary<string, object> attrs)
        {
            Attrs = attrs;
        }

        public Dictionary<string, object> Attrs { get; set; }
    }
}