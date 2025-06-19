using System.ComponentModel;

namespace Masa.Blazor
{
    public class Application : INotifyPropertyChanged
    {
        public double Bar
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bar)));
            }
        }

        public double Top
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Top)));
            }
        }

        public double Left
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
            }
        }

        public double InsetFooter
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InsetFooter)));
            }
        }

        public double Right
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Right)));
            }
        }

        public double Bottom
        {
            get;
            internal set
            {
                if (field == value) return;
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bottom)));
            }
        }

        public double Footer
        {
            get;
            internal set
            {
                if (field == value) return;

                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Footer)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}