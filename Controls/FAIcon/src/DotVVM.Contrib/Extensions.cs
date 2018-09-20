using System;
using System.Reflection;

namespace DotVVM.Contrib
{
    public static class Extensions
    {
        /// <summary>
        /// Returns the key of the icon, eg "github-alt"
        /// </summary>
        public static string Key(this FAIcons icon) => icon.GetAttributeOfType<FAIconAttribute>().Key;

        /// <summary>
        /// Returns the label of the icon, eg "Alternate GitHub"
        /// </summary>
        public static string Label(this FAIcons icon) => icon.GetAttributeOfType<FAIconAttribute>().Label;

        /// <summary>
        /// Returns the unicode character of the icon, eg "f113"
        /// </summary>
        public static string Unicode(this FAIcons icon) => icon.GetAttributeOfType<FAIconAttribute>().Unicode;

        /// <summary>
        /// Returns the style enum of the icon, eg <see cref="FAStyle.Brands" />
        /// </summary>
        public static FAStyle FAStyle(this FAIcons icon) => icon.GetAttributeOfType<FAIconAttribute>().Style;

        /// <summary>
        /// Returns the style of the icon as a string, eg "brands"
        /// </summary>
        public static string Style(this FAIcons icon) => icon.FAStyle().Style();

        /// <summary>
        /// Returns the name of the style, eg "brands"
        /// </summary>
        public static string Style(this FAStyle icon) => icon.GetAttributeOfType<FAStyleAttribute>().Style;

        /// <summary>
        /// Returns the <see cref="FAIconAttribute" /> associated with the icon, containing its metadata
        /// </summary>
        public static FAIconAttribute GetFAIconAttribute(this FAIcons icon) =>
            icon.GetAttributeOfType<FAIconAttribute>();

        /// <summary>
        /// Returns the <see cref="FAStyleAttribute" /> associated with the style, containing its metadata
        /// </summary>
        public static FAStyleAttribute GetFAStyleAttribute(this FAStyle style) =>
            style.GetAttributeOfType<FAStyleAttribute>();

        private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            return enumVal
                    .GetType()
                    .GetMember(enumVal.ToString())[0]
                    .GetCustomAttribute<T>()
                ;
        }
    }
}