using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a Select2 single-select control.
    /// </summary>
    public class Select2Single : SelectHtmlControlBase
    {
        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.Select2");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            writer.AddKnockoutDataBind("dotvvm-contrib-Select2", this, SelectedValueProperty, renderEvenInServerRenderingMode: true);
        }
    }
}
