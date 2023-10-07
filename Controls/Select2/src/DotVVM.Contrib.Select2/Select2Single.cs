using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.Select2
{
    /// <summary>
    /// Renders a Select2 single-select control.
    /// </summary>
    public class Select2Single : SelectHtmlControlBase
    {
        public bool AllowClear
        {
            get => (bool)GetValue(AllowClearProperty);
            set => SetValue(AllowClearProperty, value);
        }

        public static readonly DotvvmProperty AllowClearProperty
            = DotvvmProperty.Register<bool, Select2Single>(c => c.AllowClear, true);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly DotvvmProperty PlaceholderProperty
            = DotvvmProperty.Register<string, Select2Single>(c => c.Placeholder, "");

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.Select2");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            var group = new KnockoutBindingGroup();
            group.Add("value", this, SelectedValueProperty);
            group.Add("Placeholder", this, PlaceholderProperty);
            group.Add("AllowClear", this, AllowClearProperty);

            writer.AddKnockoutDataBind("dotvvm-contrib-Select2", group);
        }
    }
}
