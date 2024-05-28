namespace BlazorComponent
{
    public class DomEventSub
    {
        public Delegate Delegate { get; set; }
        public Type Type { get; set; }

        public DomEventSub(Delegate @delegate, Type type)
        {
            Delegate = @delegate;
            Type = type;
        }
    }
}
