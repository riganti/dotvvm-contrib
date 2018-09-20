using System;

namespace DotVVM.Contrib
{
    public class FAIconAttribute : Attribute
    {
        public FAIconAttribute(string key, string label, FAStyle style, string unicode)
        {
            Key = key;
            Label = label;
            Style = style;
            Unicode = unicode;
        }

        public string Key { get; set; }

        public string Label { get; set; }

        public FAStyle Style { get; set; }

        public string Unicode { get; set; }
    }
}