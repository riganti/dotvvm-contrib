using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.MarkdownView
{
    /// <summary>
    /// Renders a Markdown string as HTML.
    /// </summary>
    public class MarkdownView : HtmlGenericControl
    {
        /// <summary>
        /// Gets or sets markdown content to be rendered.
        /// </summary>
        [MarkupOptions(AllowHardCodedValue = false)]
        public string Markdown
        {
            get { return (string)GetValue(MarkdownProperty); }
            set { SetValue(MarkdownProperty, value); }
        }
        public static readonly DotvvmProperty MarkdownProperty
            = DotvvmProperty.Register<string, MarkdownView>(c => c.Markdown, null);

        /// <summary>
        /// Gets or sets whether the conversion of markdown content is enabled.
        /// </summary>
        public bool ConversionEnabled
        {
            get { return (bool)GetValue(ConversionEnabledProperty); }
            set { SetValue(ConversionEnabledProperty, value); }
        }
        public static readonly DotvvmProperty ConversionEnabledProperty
            = DotvvmProperty.Register<bool, MarkdownView>(c => c.ConversionEnabled, true);


        public MarkdownView() : base("div")
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.MarkdownView");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddKnockoutDataBind("dotvvm-contrib-MarkdownView-ConversionEnabled", this, ConversionEnabledProperty, () =>
            {
                writer.AddKnockoutDataBind("dotvvm-contrib-MarkdownView-ConversionEnabled", ConversionEnabled ? "true" : "false");
            }, renderEvenInServerRenderingMode: true);

            writer.AddKnockoutDataBind("dotvvm-contrib-MarkdownView", this, MarkdownProperty, renderEvenInServerRenderingMode: true);

            base.AddAttributesToRender(writer, context);
        }
    }
}
