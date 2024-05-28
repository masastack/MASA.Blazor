namespace BlazorComponent
{
    public interface IObservableProperty
    {
        string Name { get; }

        event Action<string> OnChange;

        void NotifyChange();
    }
}
