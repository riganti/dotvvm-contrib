using System;
using System.Collections.Generic;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib
{
    public class CookieBarRule : DotvvmBindableObject
    {

        [MarkupOptions(AllowBinding = false, Required = true)]
        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
        public static readonly DotvvmProperty KeyProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Key, null);

        [MarkupOptions(AllowBinding = false, Required = true)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Title, null);

        [MarkupOptions(AllowBinding = false)]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Description, null);

    }
}
