using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.Select2
{
    /// <summary>
    /// Renders a Select2 multi-select control.
    /// </summary>
    public class Select2 : MultiSelectHtmlControlBase
    {
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly DotvvmProperty PlaceholderProperty
            = DotvvmProperty.Register<string, Select2>(c => c.Placeholder, "");

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.Select2");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            var group = new KnockoutBindingGroup();
            group.AddSimpleBinding("value", this, SelectedValuesProperty);
            group.AddSimpleBinding("Placeholder", this, PlaceholderProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-Select2", group);
        }
    }
}
