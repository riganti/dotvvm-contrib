using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Binding;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a QrCode into canvas
    /// </summary>
    public class QrCode : HtmlGenericControl
    {
        public static readonly DotvvmProperty UrlProperty
           = DotvvmProperty.Register<string, QrCode>(c => c.Url, null);


        public QrCode() : base("div")
        {
        }

        [MarkupOptions(Required = true)]
        public string Url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.QrCode");
            base.OnPreRender(context);
        }
        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddKnockoutDataBind("dotvvm-contrib-QrCode", GetControlBinding());
            base.AddAttributesToRender(writer, context);
        }
        private KnockoutBindingGroup GetControlBinding()
        {
            return new KnockoutBindingGroup
            {
                {"url", this, UrlProperty}
            };
        }
    }
}
