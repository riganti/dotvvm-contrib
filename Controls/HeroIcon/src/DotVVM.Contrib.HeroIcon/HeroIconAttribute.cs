using System;

namespace DotVVM.Contrib.HeroIcon
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class HeroIconAttribute : Attribute
    {
        public HeroIconAttribute(VisualStyle visualStyle, string content)
        {
            VisualStyle = visualStyle;
            Content = content;
        }

        public VisualStyle VisualStyle { get; }
        public string Content { get; }
    }
}