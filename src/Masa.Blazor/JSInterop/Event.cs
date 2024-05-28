namespace BlazorComponent.Web
{
    public class Event
    {
        public Event(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }

        public string Name { get; }
    }
}
