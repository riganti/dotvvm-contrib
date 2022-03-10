using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib.TemplateSelector
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(Template))]
    public class TemplateChoice : DotvvmBindableObject
    {
        /// <summary>
        /// Gets or sets an unique key of the template.
        /// </summary>
        [MarkupOptions(AllowBinding = false, Required = true)]
        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
        public static readonly DotvvmProperty KeyProperty
            = DotvvmProperty.Register<string, TemplateChoice>(c => c.Key, null);

        /// <summary>
        /// Gets or sets the template to be rendered.
        /// </summary>
        [MarkupOptions(MappingMode = MappingMode.InnerElement, AllowBinding = false, Required = true)]
        public ITemplate Template
        {
            get { return (ITemplate)GetValue(TemplateProperty); }
            set { SetValue(TemplateProperty, value); }
        }
        public static readonly DotvvmProperty TemplateProperty
            = DotvvmProperty.Register<ITemplate, TemplateChoice>(c => c.Template, null);

    }
}