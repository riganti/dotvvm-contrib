using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib.Controls
{
    public class Switch : DotVVM.Framework.Controls.HtmlGenericControl
    {
        public Switch() : base("div")
        {

        }
        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DotvvmProperty ValueProperty
            = DotvvmProperty.Register<bool, Switch>(c => c.Value, false);

        [MarkupOptions(AllowBinding = true, AllowHardCodedValue = true)]
        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }
        public static readonly DotvvmProperty EnabledProperty
            = DotvvmProperty.Register<bool, Switch>(c => c.Enabled, true);


        public string OffText
        {
            get { return (string)GetValue(OffTextProperty); }
            set { SetValue(OffTextProperty, value); }
        }
        public static readonly DotvvmProperty OffTextProperty
            = DotvvmProperty.Register<string, Switch>(c => c.OffText, Resources.Strings.Switch_OffText);


        public string OnText
        {
            get { return (string)GetValue(OnTextProperty); }
            set { SetValue(OnTextProperty, value); }
        }
        public static readonly DotvvmProperty OnTextProperty
            = DotvvmProperty.Register<string, Switch>(c => c.OnText, Resources.Strings.Switch_OnText);

        public SliderOrientation Orientation
        {
            get { return (SliderOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DotvvmProperty OrientationProperty
            = DotvvmProperty.Register<SliderOrientation, Switch>(c => c.Orientation, SliderOrientation.Horizontal);

        public SliderDirection Direction
        {
            get { return (SliderDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly DotvvmProperty DirectionProperty
            = DotvvmProperty.Register<SliderDirection, Switch>(c => c.Direction, SliderDirection.LeftToRight);

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            this.RegisterRequiredResources(context);
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {

            base.AddAttributesToRender(writer, context);

            writer.AddAttribute("class", "dotvvm-contrib-switch", true);

            KnockoutBindingGroup group = new KnockoutBindingGroup() { };
            group.AddSimpleBinding("value", this, ValueProperty);
            group.AddSimpleBinding("orientation", this, OrientationProperty);
            group.AddSimpleBinding("direction", this, DirectionProperty);
            group.AddSimpleBinding("enabled", this, EnabledProperty);

            writer.AddKnockoutDataBind("dotvvm-contrib-switch", group);
        }
    }
}
