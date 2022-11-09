using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotVVM.Contrib.HeroIcon
{
    public static class Extensions
    {
        public static string SvgContent(this HeroIcons icon, VisualStyle visualStyle)
        {
            var attribute = icon.GetAttributesOfType<HeroIconAttribute>()
                       .SingleOrDefault(x => x.VisualStyle == visualStyle)
                   ?? throw new InvalidOperationException(
                       $"Attribute with visual style {visualStyle} is not defined on icon.");
            return attribute.Content;
        }

        public static string Fill(this VisualStyle visualStyle)
            => GetAttributeOfType<VisualStyleAttribute>(visualStyle).Fill;

        public static string ViewBox(this VisualStyle visualStyle)
            => GetAttributeOfType<VisualStyleAttribute>(visualStyle).ViewBox;

        private static IEnumerable<T> GetAttributesOfType<T>(this Enum enumVal) where T : Attribute
        {
            return enumVal
                .GetType()
                .GetMember(enumVal.ToString())[0]
                .GetCustomAttributes<T>();
        }

        private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            return enumVal
                .GetType()
                .GetMember(enumVal.ToString())[0]
                .GetCustomAttribute<T>();
        }
    }
}