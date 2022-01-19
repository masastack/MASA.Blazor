using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor.Doc.Services
{
    public static class BlazorMode
    {
        public const string Server = "server";

        public const string Wasm = "wasm";

        public static string Current { get; internal set; }
    }
}
