using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.Controls
{
    public class Select2 : MultiSelectHtmlControlBase
    {

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm-contrib-select2");
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            writer.AddKnockoutDataBind("dotvvm-contrib-Select2", this, SelectedValuesProperty, renderEvenInServerRenderingMode: true);
        }
    }
}
