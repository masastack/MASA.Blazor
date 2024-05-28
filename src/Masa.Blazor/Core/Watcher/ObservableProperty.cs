namespace BlazorComponent
{
    public class ObservableProperty : IObservableProperty
    {
        public ObservableProperty(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public event Action<string>? OnChange;

        void IObservableProperty.NotifyChange()
        {
            OnChange?.Invoke(Name);
        }
    }
}
