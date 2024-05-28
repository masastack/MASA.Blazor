using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlazorComponent
{
    public class ObservableObject : INotifyPropertyChanged
    {
        private readonly ConcurrentDictionary<string, IObservableProperty> _props = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected TValue? GetValue<TValue>(TValue? @default = default, [CallerMemberName] string name = "")
        {
            var prop = _props.GetOrAdd(name, _ => new ObservableProperty<TValue>(name));
            var property = (ObservableProperty<TValue>)prop;
            if (!property.HasValue)
            {
                property.SetValueWithNoEffect(@default);
            }

            return property.Value;
        }

        protected void SetValue<TValue>(TValue value, [CallerMemberName] string name = "")
        {
            SetValue(name, value);
        }

        private void SetValue<TValue>(string name, TValue value)
        {
            var prop = _props.GetOrAdd(name, _ => new ObservableProperty<TValue>(name, value: value));
            var property = (ObservableProperty<TValue>)prop;

            property.OnChange += NotifyPropertyChange;
            property.Value = value;
            property.OnChange -= NotifyPropertyChange;
        }

        private void NotifyPropertyChange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
