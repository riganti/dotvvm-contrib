using System;

namespace DotVVM.Contrib.FAIcon
{
    public class FAStyleAttribute : Attribute
    {
        public FAStyleAttribute(string style)
        {
            Style = style;
        }

        public string Style { get; set; }
    }
}