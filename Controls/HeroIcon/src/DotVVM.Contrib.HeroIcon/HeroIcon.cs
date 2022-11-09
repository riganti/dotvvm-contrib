using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.HeroIcon
{
    /// <summary>
    /// Renders SVG Hero icon.
    /// </summary>
    public class HeroIcon : HtmlGenericControl
    {
        public HeroIcon() : base("svg")
        {
        }

        [MarkupOptions(Required = true, AllowBinding = false)]
        public HeroIcons Icon
        {
            get { return (HeroIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DotvvmProperty IconProperty
            = DotvvmProperty.Register<HeroIcons, HeroIcon>(c => c.Icon);

        [MarkupOptions(AllowBinding = false)]
        public VisualStyle VisualStyle
        {
            get { return (VisualStyle)GetValue(VisualStyleProperty); }
            set { SetValue(VisualStyleProperty, value); }
        }
        public static readonly DotvvmProperty VisualStyleProperty
            = DotvvmProperty.Register<VisualStyle, HeroIcon>(c => c.VisualStyle, VisualStyle.Outline);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("xmlns", "http://www.w3.org/2000/svg");
            writer.AddAttribute("fill", VisualStyle.Fill());
            writer.AddAttribute("viewBox", VisualStyle.ViewBox());
            if (VisualStyle == VisualStyle.Outline)
            {
                writer.AddAttribute("stroke", "currentColor");
                writer.AddAttribute("stroke-width", "2");
            }

            base.AddAttributesToRender(writer, context);
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.WriteUnencodedText(Icon.SvgContent(VisualStyle));
            base.RenderContents(writer, context);
        }
    }
}
