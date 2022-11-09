using System;

namespace DotVVM.Contrib.HeroIcon
{
    [AttributeUsage(AttributeTargets.Field)]
    public class VisualStyleAttribute : Attribute
    {
        public VisualStyleAttribute(string fill, string viewBox)
        {
            Fill = fill;
            ViewBox = viewBox;
        }

        public string Fill { get; }
        public string ViewBox { get; }
    }
}