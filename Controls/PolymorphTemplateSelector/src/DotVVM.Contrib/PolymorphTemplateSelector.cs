using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System;
using System.Collections.Generic;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Compilation.Javascript.Ast;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders the first of the specified templates which has non-null DataContext.
    /// </summary>
    [ContainsDotvvmProperties]
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(Templates))]
    public class PolymorphTemplateSelector : HtmlGenericControl 
    {

        private readonly List<(PolymorphTemplate template, string templateId)> templatesWithIds = new();
        private string fallbackTemplateId;

        /// <summary>
        /// Gets or sets a list of possible templates.
        /// </summary>
        [MarkupOptions(Required = true, AllowBinding = false, MappingMode = MappingMode.InnerElement)]
        public List<PolymorphTemplate> Templates
        {
            get { return (List<PolymorphTemplate>)GetValue(TemplatesProperty); }
            set { SetValue(TemplatesProperty, value); }
        }
        public static readonly DotvvmProperty TemplatesProperty
            = DotvvmProperty.Register<List<PolymorphTemplate>, PolymorphTemplateSelector>(p => p.Templates, null, isValueInherited: true);

        /// <summary>
        /// Gets or sets a fallback template.
        /// </summary>
        public ITemplate FallbackTemplate
        {
            get { return (ITemplate)GetValue(FallbackTemplateProperty); }
            set { SetValue(FallbackTemplateProperty, value); }
        }
        public static readonly DotvvmProperty FallbackTemplateProperty
            = DotvvmProperty.Register<ITemplate, PolymorphTemplateSelector>(c => c.FallbackTemplate, null);


        /// <summary>
        /// Gets or sets whether the control should render a wrapper element.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool RenderWrapperTag
        {
            get { return (bool)GetValue(RenderWrapperTagProperty)!; }
            set { SetValue(RenderWrapperTagProperty, value); }
        }

        public static readonly DotvvmProperty RenderWrapperTagProperty =
            DotvvmProperty.Register<bool, PolymorphTemplateSelector>(t => t.RenderWrapperTag, true);

        /// <summary>
        /// Gets or sets the name of the tag that wraps the Repeater.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string WrapperTagName
        {
            get { return (string)GetValue(WrapperTagNameProperty)!; }
            set { SetValue(WrapperTagNameProperty, value); }
        }

        public static readonly DotvvmProperty WrapperTagNameProperty =
            DotvvmProperty.Register<string, PolymorphTemplateSelector>(t => t.WrapperTagName, "div");

        protected override bool RendersHtmlTag => RenderWrapperTag;


        protected override void OnLoad(IDotvvmRequestContext context)
        {
            BuildAndRegisterTemplates(context);
            BuildAndRegisterFallbackTemplate(context);

            base.OnLoad(context);
        }

        protected virtual void BuildAndRegisterFallbackTemplate(IDotvvmRequestContext context)
        {
            if (FallbackTemplate != null)
            {
                var fallbackPlaceholder = new PlaceHolder();
                FallbackTemplate.BuildContent(context, fallbackPlaceholder);
                Children.Add(fallbackPlaceholder);
                fallbackTemplateId = context.ResourceManager.AddTemplateResource(context, fallbackPlaceholder);
            }
            else
            {
                fallbackTemplateId = context.ResourceManager.AddTemplateResource("<!-- empty template -->");
            }
        }

        protected virtual void BuildAndRegisterTemplates(IDotvvmRequestContext context)
        {
            foreach (var template in Templates)
            {
                var placeholder = new PlaceHolder();
                template.ContentTemplate.BuildContent(context, placeholder);
                Children.Add(placeholder);

                var templateId = context.ResourceManager.AddTemplateResource(context, placeholder);
                templatesWithIds.Add((template, templateId));
            }
        }


        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            TagName = WrapperTagName;
            
            var expr = BuildKnockoutTemplateBindingExpression();

            if (RenderWrapperTag)
            {
                writer.AddKnockoutDataBind("template", expr);
                base.RenderBeginTag(writer, context);
            }
            else
            {
                writer.WriteKnockoutDataBindComment("template", expr);
            }
        }

        private string BuildKnockoutTemplateBindingExpression()
        {
            // build Knockout template binding:

            // FirstProp() !== null ? { name: 'first', data: FirstProp }
            //   : SecondProp() !== null ? { name: 'second', data: SecondProp }
            //     ...
            //       : { name: 'fallback', data: { } }

            JsExpression expression = new JsObjectExpression(
                new JsObjectProperty("name", new JsLiteral(fallbackTemplateId)),
                new JsObjectProperty("data", new JsObjectExpression())
            );

            foreach (var template in templatesWithIds)
            {
                var binding = template.template.GetValueBinding(DataContextProperty).GetParametrizedKnockoutExpression(this, unwrapped: true);
                var unwrappedBinding = template.template.GetValueBinding(DataContextProperty).GetParametrizedKnockoutExpression(this, unwrapped: false);

                expression = new JsConditionalExpression(
                    new JsBinaryExpression(JsSymbolicParameter.CreateCodePlaceholder(binding), BinaryOperatorType.StricltyNotEqual, new JsLiteral(null)),
                    new JsObjectExpression(
                        new JsObjectProperty("name", new JsLiteral(template.templateId)),
                        new JsObjectProperty("data", JsSymbolicParameter.CreateCodePlaceholder(unwrappedBinding))
                    ),
                    expression);
            }
            var expr = expression.FormatParametrizedScript().ToDefaultString();
            return expr;
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            // do not render contents
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderWrapperTag)
            {
                base.RenderEndTag(writer, context);
            }
            else
            {
                writer.WriteKnockoutDataBindEndComment();
            }
        }
    }
}
