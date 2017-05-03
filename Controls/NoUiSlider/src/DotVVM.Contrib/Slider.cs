using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    public class Slider : DotVVM.Framework.Controls.HtmlGenericControl
    {
        public Slider() : base("div")
        {

        }

        [MarkupOptions(AllowBinding = false, AllowHardCodedValue = true)]
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DotvvmProperty MaxValueProperty
            = DotvvmProperty.Register<int, Slider>(c => c.MaxValue, 100);

        [MarkupOptions(AllowBinding = false, AllowHardCodedValue = true)]
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly DotvvmProperty MinValueProperty
            = DotvvmProperty.Register<int, Slider>(c => c.MinValue, 0);

        [MarkupOptions(AllowBinding = true, AllowHardCodedValue = false)]
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DotvvmProperty ValueProperty
            = DotvvmProperty.Register<int, Slider>(c => c.Value, 0);

        [MarkupOptions(AllowBinding = true, AllowHardCodedValue = true)]
        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }
        public static readonly DotvvmProperty EnabledProperty
            = DotvvmProperty.Register<bool, Slider>(c => c.Enabled, true);


        public SliderOrientation Orientation
        {
            get { return (SliderOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DotvvmProperty OrientationProperty
            = DotvvmProperty.Register<SliderOrientation, Slider>(c => c.Orientation, SliderOrientation.Horizontal);

        public SliderDirection Direction
        {
            get { return (SliderDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        public static readonly DotvvmProperty DirectionProperty
            = DotvvmProperty.Register<SliderDirection, Slider>(c => c.Direction, SliderDirection.LeftToRight);

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }
        public static readonly DotvvmProperty StepProperty
            = DotvvmProperty.Register<int, Slider>(c => c.Step, 1);
        

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.NoUiSlider");

            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            base.AddAttributesToRender(writer, context);

            writer.AddAttribute("class", "dotvvm-contrib-Slider", true);

            var group = new KnockoutBindingGroup();
            group.AddSimpleBinding("value", this, ValueProperty);
            group.AddSimpleBinding("maxValue", this, MaxValueProperty);
            group.AddSimpleBinding("minValue", this, MinValueProperty);
            group.AddSimpleBinding("orientation", this, OrientationProperty);
            group.AddSimpleBinding("direction", this, DirectionProperty);
            group.AddSimpleBinding("step", this, StepProperty);
            group.AddSimpleBinding("enabled", this, EnabledProperty);

            writer.AddKnockoutDataBind("dotvvm-contrib-Slider", group);
        }
    }
}
