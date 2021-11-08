using MASA.Blazor.Model;
using System;
using System.ComponentModel;

namespace BlazorComponent
{
    /// <summary>
    /// Cascading this will cause additional render,we may just cascading rtl in the feature
    /// </summary>
    public class GlobalConfig
    {
        private bool _rtl;

        public bool RTL
        {
            get
            {
                return _rtl;
            }
            set
            {
                if (_rtl != value)
                {
                    _rtl = value;
                    OnRTLChange?.Invoke(_rtl);
                }
            }
        }

        public event Action<bool> OnRTLChange;

        public Application Application { get; set; } = new();
    }
}
