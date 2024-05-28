using System.Collections;
using System.ComponentModel;

namespace BlazorComponent
{
    public class ObservableProperty<TValue> : ObservableProperty
    {
        private readonly IObservableProperty _internalProperty;

        private bool _hasValue;
        private TValue? _oldValue;
        private TValue? _value;

        public ObservableProperty(string name, TValue? value)
            : base(name)
        {
            Value = value;
            _internalProperty = this;
        }

        public ObservableProperty(string name) : base(name)
        {
            _internalProperty = this;
        }

        public ObservableProperty(IObservableProperty property, TValue? value)
            : base(property.Name)
        {
            Value = value;
            _internalProperty = property;
        }

        public TValue? Value
        {
            get => _value;
            set
            {
                //First time set value
                if (!_hasValue && value is INotifyPropertyChanged notify)
                {
                    notify.PropertyChanged += (_, _) => { NotifyChange(_value, _oldValue); };
                }

                //We can't detect whether reference type has changed
                //Only if we copy a new instance
                //We will think it over
                //
                //We just assume list always changed
                //This will be changed when we finished data-collect and deep watch
                if (!_hasValue || !EqualityComparer<TValue>.Default.Equals(_value, value))
                {
                    _oldValue = _value;
                    _value = value;
                    _hasValue = true;

                    NotifyChange(_value, _oldValue);
                }
            }
        }

        /// <summary>
        /// Set value without notification
        /// </summary>
        /// <param name="value"></param>
        public void SetValueWithNoEffect(TValue? value)
        {
            if (!_hasValue && value is INotifyPropertyChanged notify)
            {
                notify.PropertyChanged += (_, _) => { NotifyChange(_value, _oldValue); };
            }

            _value = value;
            _hasValue = true;
        }

        public Func<TValue>? ValueFactory { get; set; }

        /// <summary>
        /// If value has set return true,else false
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// Indicate whether this property is computed, only used for computed property
        /// TODO: maybe add a new class for computed property in the future
        /// </summary>
        public bool Computed { get; set; }

        public event Action<TValue?, TValue?>? OnValueChange;

        public void NotifyChange(TValue? newValue, TValue? oldValue)
        {
            OnValueChange?.Invoke(newValue, oldValue);
            _internalProperty?.NotifyChange();
        }
    }
}