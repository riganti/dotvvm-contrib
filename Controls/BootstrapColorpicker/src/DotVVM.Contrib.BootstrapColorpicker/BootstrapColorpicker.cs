using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.BootstrapColorpicker
{
    /// <summary>
    /// Renders a Bootstrap Colorpicker widget.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class BootstrapColorpicker : HtmlGenericControl
    {

        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DotvvmProperty ColorProperty
            = DotvvmProperty.Register<string, BootstrapColorpicker>(c => c.Color, null);


        public BootstrapColorpicker() : base("input")
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.BootstrapColorpicker");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("class", "form-control");
            writer.AddAttribute("type", "text");
            writer.AddKnockoutDataBind("dotvvm-contrib-BootstrapColorpicker", this, ColorProperty, renderEvenInServerRenderingMode: true, nullBindingAction: () =>
            {
                writer.AddAttribute("value", Color);
            });

            base.AddAttributesToRender(writer, context);
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderSelfClosingTag(TagName);
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            // this method is left blank intentionally
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            // this method is left blank intentionally
        }
    }
}
