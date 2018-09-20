using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class FAIcon : HtmlGenericControl
    {
        [MarkupOptions(Required = true)]
        public FAIcons Icon
        {
            get { return (FAIcons) GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DotvvmProperty IconProperty =
            DotvvmProperty.Register<FAIcons, FAIcon>(c => c.Icon, FAIcons.font_awesome_brands);

        public FAIcon() : base("i")
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {

            if (RenderOnServer || !HasBinding(IconProperty))
            {
                writer.AddAttribute("class",Icon.Style()=="brands" ? "fab" : "fas");
                writer.AddAttribute("class",$"fa-{Icon.Key()}",true);
            }

            writer.AddKnockoutDataBind("dotvvm-contrib-FAIcon",this,IconProperty,renderEvenInServerRenderingMode:true);
            base.AddAttributesToRender(writer, context);
        }


        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.FAIcon");

            base.OnPreRender(context);
        }		
		
    }
}
