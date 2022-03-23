using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.ControlName
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class ControlName : HtmlGenericControl
    {

        public ControlName() : base("div")
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.ControlName");

            base.OnPreRender(context);
        }		
		
    }
}
