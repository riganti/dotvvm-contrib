using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.EditableForm
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(ContentTemplate))]
    public class EditableFormControl : DotvvmMarkupControl
    {

        [MarkupOptions(AllowBinding = false, MappingMode = MappingMode.InnerElement, Required = true)]
        public ITemplate ContentTemplate
        {
            get { return (ITemplate) GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly DotvvmProperty ContentTemplateProperty
            = DotvvmProperty.Register<ITemplate, EditableFormControl>(c => c.ContentTemplate, null);


        protected override void OnLoad(IDotvvmRequestContext context)
        {
            var container = FindControlByClientId("TemplateHost", true);
            ContentTemplate.BuildContent(context, container);

            base.OnLoad(context);
        }
    }
}
