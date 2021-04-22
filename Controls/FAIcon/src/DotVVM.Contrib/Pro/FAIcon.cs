using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.FAIcon.Pro
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class FAIcon : HtmlGenericControl
    {
        [MarkupOptions(Required = true)]
        public FAIconsPro Icon
        {
            get { return (FAIconsPro)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DotvvmProperty IconProperty =
            DotvvmProperty.Register<FAIconsPro, FAIcon>(c => c.Icon, FAIconsPro.font_awesome_brands);

        public FAIcon() : base("i")
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderOnServer || !HasBinding(IconProperty))
            {
                writer.AddAttribute("class", Icon.StylePrefix());
                writer.AddAttribute("class", $"fa-{Icon.Key()}", true);
            }

            writer.AddKnockoutDataBind("dotvvm-contrib-FAIcon", this, IconProperty, renderEvenInServerRenderingMode: true);
            base.AddAttributesToRender(writer, context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource(ResourceNames.ProStyleResourceName);

            base.OnPreRender(context);
        }
    }
}