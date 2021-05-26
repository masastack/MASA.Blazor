using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class ValidatorArgs
    {
        public ValidatorArgs(string value,List<string> messages)
        {
            Value = value;
            Messages = messages;
        }

        public string Value { get; set; }
        public List<string> Messages { get; set; }
    }
}
