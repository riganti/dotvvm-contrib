using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Newtonsoft.Json.Linq;

namespace DotVVM.Contrib.Samples.Chartist
{
    public class Chartist : DotvvmControl
    {
        public ChartType Type
        {
            get { return (ChartType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        [MarkupOptions(Required = true, AllowBinding = false, AllowHardCodedValue = true)]
        public static readonly DotvvmProperty TypeProperty
            = DotvvmProperty.Register<ChartType, Chartist>(c => c.Type);

        public ChartData Data
        {
            get { return (ChartData)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        [MarkupOptions(Required = true)]
        public static readonly DotvvmProperty DataProperty
            = DotvvmProperty.Register<ChartData, Chartist>(c => c.Data);

        public string Options
        {
            get { return (string)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }
        [MarkupOptions(AllowBinding = false, AllowHardCodedValue = true)]
        public static readonly DotvvmProperty OptionsProperty
            = DotvvmProperty.Register<string, Chartist>(c => c.Options);

        public string ResponsiveOptions
        {
            get { return (string)GetValue(ResponsiveOptionsProperty); }
            set { SetValue(ResponsiveOptionsProperty, value); }
        }
        [MarkupOptions(AllowBinding = false, AllowHardCodedValue = true)]
        public static readonly DotvvmProperty ResponsiveOptionsProperty
            = DotvvmProperty.Register<string, Chartist>(c => c.ResponsiveOptions);

        public string Class
        {
            get { return (string)GetValue(ClassProperty); }
            set { SetValue(ClassProperty, value); }
        }
        [MarkupOptions(AllowBinding = false, AllowHardCodedValue = true)]
        public static readonly DotvvmProperty ClassProperty
            = DotvvmProperty.Register<string, Chartist>(c => c.Class);

        protected override void OnInit(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("ReactChartist");

            var reactBridge = new ReactBridge
            {
                Name = "ReactChartist.default",
            };

            reactBridge.Attributes["type"] = Type.ToString();
            reactBridge.Attributes["data"] = GetValueBinding(DataProperty);

            if (!string.IsNullOrEmpty(Options))
            {
                reactBridge.Attributes["options"] = JObject.Parse(Options);
            }

            if (!string.IsNullOrEmpty(ResponsiveOptions))
            {
                reactBridge.Attributes["responsiveOptions"] = JArray.Parse(ResponsiveOptions);
            }

            if (!string.IsNullOrEmpty(Class))
            {
                reactBridge.Attributes["className"] = Class;
            }

            Children.Add(reactBridge);

            base.OnInit(context);
        }
    }
}
