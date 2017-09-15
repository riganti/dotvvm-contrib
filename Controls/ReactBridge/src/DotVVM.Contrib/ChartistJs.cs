using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DotVVM.Contrib
{
    public class ChartistJs : DotvvmControl
    {
        public ChartType Type
        {
            get { return (ChartType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        [MarkupOptions(Required = true)]
        public static readonly DotvvmProperty TypeProperty
            = DotvvmProperty.Register<ChartType, ChartistJs>(c => c.Type);

        public ChartData Data
        {
            get { return (ChartData)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        [MarkupOptions(Required = true)]
        public static readonly DotvvmProperty DataProperty
            = DotvvmProperty.Register<ChartData, ChartistJs>(c => c.Data);

        public string Options
        {
            get { return (string)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }
        [MarkupOptions(AllowBinding = false)]
        public static readonly DotvvmProperty OptionsProperty
            = DotvvmProperty.Register<string, ChartistJs>(c => c.Options);

        protected override void OnInit(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("ReactChartist");

            var reactBridge = new ReactBridge
            {
                Name = "ReactChartist.default",
            };

            reactBridge.Attributes["type"] = Type.ToString();
            reactBridge.Attributes["data"] = GetValueBinding(DataProperty);

            if (Options != null)
            {
                reactBridge.Attributes["options"] = JObject.Parse(Options);
            }

            Children.Add(reactBridge);

            base.OnInit(context);
        }
    }
}
