using System;
using System.Linq;

namespace DotVVM.Contrib.FAIcon
{
    public class FAStyleAttribute : Attribute
    {
        public FAStyleAttribute(string style)
        {
            Style = style;
        }

        public string Style { get; set; }
        public string StylePrefix => $"fa{Style.ElementAt(0)}";
    }
}