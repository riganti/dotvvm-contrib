using System;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class SanitizedHtmlLiteral : HtmlGenericControl
    {

        public SanitizedHtmlLiteral() : base("div")
        {
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.SanitizedHtmlLiteral");

            base.OnPreRender(context);
        }		
		
    }
}
