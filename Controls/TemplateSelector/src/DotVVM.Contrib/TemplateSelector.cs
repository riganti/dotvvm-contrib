using System;
using System.Collections.Generic;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Selects a template with a specified key and renders it.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(Choices))]
    public class TemplateSelector : HtmlGenericControl
    {
        /// <summary>
        /// Gets or sets a binding expression that returns a key for the template to be used.
        /// </summary>
        [MarkupOptions(AllowHardCodedValue = false, Required = true)]
        public IValueBinding SelectedKeyBinding
        {
            get { return (IValueBinding)GetValue(SelectedKeyBindingProperty); }
            set { SetValue(SelectedKeyBindingProperty, value); }
        }
        public static readonly DotvvmProperty SelectedKeyBindingProperty
            = DotvvmProperty.Register<IValueBinding, TemplateSelector>(c => c.SelectedKeyBinding, null);

        /// <summary>
        /// Gets or sets a list of templates to be selected from.
        /// </summary>
        [MarkupOptions(MappingMode = MappingMode.InnerElement, Required = true)]
        public List<TemplateChoice> Choices
        {
            get { return (List<TemplateChoice>)GetValue(ChoicesProperty); }
            set { SetValue(ChoicesProperty, value); }
        }
        public static readonly DotvvmProperty ChoicesProperty
            = DotvvmProperty.Register<List<TemplateChoice>, TemplateSelector>(c => c.Choices, null);


        private List<PlaceHolder> templateHosts;


        public TemplateSelector() : base("div")
        {
        }


        protected override void OnInit(IDotvvmRequestContext context)
        {
            BuildTemplateHosts(context);

            base.OnInit(context);
        }

        private void BuildTemplateHosts(IDotvvmRequestContext context)
        {
            templateHosts = new List<PlaceHolder>();

            foreach (var choice in Choices)
            {
                var host = new PlaceHolder();
                templateHosts.Add(host);
                Children.Add(host);
                
                choice.Template.BuildContent(context, host);
            }
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var expr = SelectedKeyBinding.GetKnockoutBindingExpression();
            
            for (int i = 0; i < Choices.Count; i++)
            {
                var key = KnockoutHelper.MakeStringLiteral(Choices[i].Key);

                writer.WriteKnockoutDataBindComment("if", $"ko.unwrap({expr}) === {key}");
                templateHosts[i].Render(writer, context);
                writer.WriteKnockoutDataBindEndComment();
            }
        }
    }
}
