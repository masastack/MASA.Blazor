using BlazorComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Model
{
    public class Application: INotifyPropertyChanged
    {
        private double _bar;
        private double _top;
        private double _left;
        private double _insetFooter;
        private double _right;
        private double _bottom;
        private double _footer;
        private bool _isBooted;

        public double Bar
        {
            get
            { 
                return _bar;
            }
            set
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
            get
            {
                return _top;
            }
            set
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
            get
            {
                return _left;
            }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Left)));
                }
            }
        }

        public double InsetFooter
        {
            get
            {
                return _insetFooter;
            }
            set
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
            get
            {
                return _right;
            }
            set
            {
                if (_right != value)
                {
                    _right = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Right)));
                }
            }
        }

        public double Bottom
        {
            get
            {
                return _bottom;
            }
            set
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
            get
            {
                return _footer;
            }
            set
            {
                if (_footer != value)
                {
                    _footer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Footer)));
                }
            }
        }

        public bool IsBooted
        {
            get
            {
                return _isBooted;
            }
            set
            {
                if (_isBooted != value)
                {
                    _isBooted = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBooted)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
