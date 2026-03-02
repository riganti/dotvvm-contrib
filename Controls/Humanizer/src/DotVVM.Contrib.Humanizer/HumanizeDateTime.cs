using System;
using System.Globalization;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Humanizer;

namespace DotVVM.Contrib.Humanizer
{
    /// <summary>
    /// Renders a humanized representation of a DateTime value (e.g. "3 hours ago", "2 days from now").
    /// Supports both hard-coded values and data bindings.
    /// When used with a binding, the value is updated client-side using a Knockout binding handler.
    /// </summary>
    public class HumanizeDateTime : HtmlGenericControl
    {
        public HumanizeDateTime() : base("span")
        {
        }

        /// <summary>
        /// Gets or sets the DateTime value to humanize.
        /// </summary>
        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DotvvmProperty ValueProperty
            = DotvvmProperty.Register<DateTime?, HumanizeDateTime>(c => c.Value, null);

        /// <summary>
        /// Gets or sets whether the displayed value should be recalculated automatically on the client every minute.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool AutoUpdate
        {
            get { return (bool)GetValue(AutoUpdateProperty); }
            set { SetValue(AutoUpdateProperty, value); }
        }
        public static readonly DotvvmProperty AutoUpdateProperty
            = DotvvmProperty.Register<bool, HumanizeDateTime>(c => c.AutoUpdate, false);

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.Humanizer");
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var valueBinding = GetValueBinding(ValueProperty);

            if (valueBinding != null || AutoUpdate)
            {
                var group = new KnockoutBindingGroup();
                group.Add("value", this, ValueProperty);
                group.Add("autoUpdate", AutoUpdate ? "true" : "false");
                writer.AddKnockoutDataBind("dotvvm-contrib-HumanizeDateTime", group);
            }

            base.AddAttributesToRender(writer, context);
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (Value.HasValue)
            {
                // Render the server-side humanized text as the initial content
                writer.WriteText(Value.Value.Humanize(culture: CultureInfo.CurrentCulture));
            }
            else
            {
                base.RenderContents(writer, context);
            }
        }
    }
}
