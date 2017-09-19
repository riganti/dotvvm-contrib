using System;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Binding;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class CkEditorMinimal : HtmlGenericControl
    {

        public CkEditorMinimal(): base("textarea") 
        {
        }

        /// <summary>
        /// Gets or sets the text in the control.
        /// </summary>
        [MarkupOptions(Required = true, AllowHardCodedValue = false)]
        public string Html
        {
            get { return Convert.ToString(GetValue(HtmlProperty)); }
            set { SetValue(HtmlProperty, value); }
        }

        public static readonly DotvvmProperty HtmlProperty =
            DotvvmProperty.Register<string, CkEditorMinimal>(t => t.Html, "");


        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.CkEditorMinimal");
            context.ResourceManager.AddRequiredResource("ckeditor-config");
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            this.AddDotvvmUniqueIdAttribute();
            base.AddAttributesToRender(writer, context);

            var group = new KnockoutBindingGroup();
            group.Add("html", this, HtmlProperty);

            writer.AddKnockoutDataBind("dotvvm-contrib-CkEditorMinimal", group);
        }

    }
}
