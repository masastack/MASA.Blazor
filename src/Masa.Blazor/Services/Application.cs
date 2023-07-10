using System.ComponentModel;

namespace Masa.Blazor
{
    public class Application : INotifyPropertyChanged
    {
        private double _bar;
        private double _top;
        private double? _left;
        private double _insetFooter;
        private double? _right;
        private double _bottom;
        private double _footer;

        /// <summary>
        /// Determines if the application has a navigation drawer(required the App property).
        /// </summary>
        internal bool HasNavigationDrawer { get; set; }

        /// <summary>
        /// Determines if the value of <see cref="Left"/> or <see cref="Right"/> has been set.
        /// </summary>
        public bool LeftRightCalculated { get; private set; }

        public double Bar
        {
            get => _bar;
            internal set
            {
                if (_bar != value)
                {
                    _bar = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bar)));
                }
            }
        }

        public double Top
        {
            get => _top;
            internal set
            {
                if (_top != value)
                {
                    _top = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Top)));
                }
            }
        }

        public double Left
        {
            get => _left ?? 0;
            internal set
            {
                LeftRightCalculated = true;

                if (_left is null || _left != value)
                {
                    _left = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
                }
            }
        }

        public double InsetFooter
        {
            get => _insetFooter;
            internal set
            {
                if (_insetFooter != value)
                {
                    _insetFooter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InsetFooter)));
                }
            }
        }

        public double Right
        {
            get => _right ?? 0;
            internal set
            {
                LeftRightCalculated = true;

                if (_right is null || _right != value)
                {
                    _right = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Right)));
                }
            }
        }

        public double Bottom
        {
            get => _bottom;
            internal set
            {
                if (_bottom != value)
                {
                    _bottom = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bottom)));
                }
            }
        }

        public double Footer
        {
            get => _footer;
            internal set
            {
                if (_footer != value)
                {
                    _footer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Footer)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
