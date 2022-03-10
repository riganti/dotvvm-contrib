using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.QrCode
{
    /// <summary>
    /// Renders a QrCode into canvas
    /// </summary>
    public class QrCode : HtmlGenericControl
    {
        public static readonly DotvvmProperty ContentProperty
           = DotvvmProperty.Register<string, QrCode>(c => c.Content, null);


        public QrCode() : base("div")
        {
        }

        [MarkupOptions(Required = true)]
        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
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
                {"content", this, ContentProperty}
            };
        }
    }
}
