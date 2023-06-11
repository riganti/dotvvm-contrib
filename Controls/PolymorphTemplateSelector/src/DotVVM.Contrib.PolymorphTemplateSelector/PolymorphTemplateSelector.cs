using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Compilation.Javascript.Ast;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.PolymorphTemplateSelector
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
        private object memoizedItem;

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
        [MarkupOptions(AllowBinding = false, MappingMode = MappingMode.InnerElement)]
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
            if (context.IsPostBack)
            {
                DataBind(context);
                memoizedItem = DataContext;
            }

            base.OnLoad(context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            if (RenderOnServer)
            {
                if (DataContext != memoizedItem)
                {
                    DataBind(context);
                }
            }
            else
            {
                // build controls for client-side templates
                BuildClientTemplates(context);
            }

            base.OnPreRender(context);
        }

        protected virtual void BuildClientTemplates(IDotvvmRequestContext context)
        {
            foreach (var template in Templates)
            {
                var placeholder = new PlaceHolder();
                placeholder.SetValue(Internal.PathFragmentProperty, template.GetValueBinding(DataContextProperty).GetKnockoutBindingExpression(this));
                placeholder.SetDataContextType(template.GetDataContextType());
                template.ContentTemplate.BuildContent(context, placeholder);
                Children.Add(placeholder);
            }

            if (FallbackTemplate != null)
            {
                var fallbackPlaceholder = new PlaceHolder();
                FallbackTemplate.BuildContent(context, fallbackPlaceholder);
                Children.Add(fallbackPlaceholder);
            }
        }

        private void DataBind(IDotvvmRequestContext context)
        {
            if (DataContext == null)
            {
                return;
            }

            Children.Clear();

            if (FindActiveTemplate() is { } activeTemplate)
            {
                // instantiate the template
                var placeholder = new PlaceHolder();
                placeholder.SetValueRaw(DataContextProperty, activeTemplate.GetValueRaw(DataContextProperty));
                Children.Add(placeholder);
                activeTemplate.ContentTemplate.BuildContent(context, placeholder);
            }
            else if (FallbackTemplate != null)
            {
                // instantiate fallback template
                var placeholder = new PlaceHolder();
                Children.Add(placeholder);
                FallbackTemplate.BuildContent(context, placeholder);
            }
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            TagName = WrapperTagName;

            if (!RenderOnServer)
            {
                // client-side rendering
                RegisterTemplates(context);
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
            else
            {
                // server-side rendering
                base.RenderBeginTag(writer, context);
            }
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (!RenderOnServer)
            {
                // do not render anything
            }
            else
            {
                base.RenderContents(writer, context);
            }
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (!RenderOnServer)
            {
                // client-side rendering
                if (RenderWrapperTag)
                {
                    base.RenderEndTag(writer, context);
                }
                else
                {
                    writer.WriteKnockoutDataBindEndComment();
                }
            }
            else
            {
                // server-side rendering
                base.RenderEndTag(writer, context);
            }
        }

        private PolymorphTemplate FindActiveTemplate()
        {
            return Templates.FirstOrDefault(t => t.GetValueBinding(DataContextProperty)?.Evaluate(this) != null);
        }


        protected virtual void RegisterTemplates(IDotvvmRequestContext context)
        {
            for (var i = 0; i < Templates.Count; i++)
            {
                var templateId = context.ResourceManager.AddTemplateResource(context, Children[i]);
                templatesWithIds.Add((Templates[i], templateId));
            }

            if (Children.Count > Templates.Count)
            {
                fallbackTemplateId = context.ResourceManager.AddTemplateResource(context, Children[Children.Count - 1]);
            }
            else
            {
                fallbackTemplateId = context.ResourceManager.AddTemplateResource("<!-- empty template -->");
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
                    new JsBinaryExpression(JsSymbolicParameter.CreateCodePlaceholder(binding), BinaryOperatorType.StrictlyNotEqual, new JsLiteral(null)),
                    new JsObjectExpression(
                        new JsObjectProperty("name", new JsLiteral(template.templateId)),
                        new JsObjectProperty("data", JsSymbolicParameter.CreateCodePlaceholder(unwrappedBinding))
                    ),
                    expression);
            }
            var expr = expression.FormatParametrizedScript().ToDefaultString();
            return expr;
        }

    }
}
