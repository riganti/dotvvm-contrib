using System;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a Select2 multi-select control.
    /// </summary>
    public class Select2 : MultiSelectHtmlControlBase
    {

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.Select2");
            
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            writer.AddKnockoutDataBind("dotvvm-contrib-Select2", this, SelectedValuesProperty, renderEvenInServerRenderingMode: true);
        }
    }
}
