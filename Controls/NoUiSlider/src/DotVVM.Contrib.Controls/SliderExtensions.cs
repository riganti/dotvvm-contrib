using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.Controls
{
    public static class SliderExtensions
    {
        public static void AddSliderConfiguration(this DotvvmConfiguration config)
        {
            RegisterControls(config);
            RegisterResources(config);
        }

        private static void RegisterControls(DotvvmConfiguration config)
        {
            config.Markup.AddCodeControl("dc", typeof(Slider).Namespace, typeof(Slider).Assembly.GetName().Name);
            config.Markup.AddCodeControl("dc", typeof(Switch).Namespace, typeof(Switch).Assembly.GetName().Name);
        }

        private static void RegisterResources(DotvvmConfiguration config)
        {
            config.Resources.Register("nouislider", new ScriptResource()
            {
                EmbeddedResourceAssembly = typeof(SliderExtensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.NoUiSlider.nouislider.min.js",
                Dependencies = new[] { "dotvvm", "nouislider.css", "jquery" }
            });
            config.Resources.Register("nouislider.css", new StylesheetResource()
            {
                EmbeddedResourceAssembly = typeof(SliderExtensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.NoUiSlider.nouislider.min.css"
            });
            config.Resources.Register("dotvvm-contrib-slider.css", new StylesheetResource()
            {
                EmbeddedResourceAssembly = typeof(SliderExtensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.DotvvmContrib-Slider.css",
                Dependencies = new [] { "nouislider.css" }
            });
            config.Resources.Register("dotvvm-contrib-slider", new ScriptResource()
            {
                EmbeddedResourceAssembly = typeof(SliderExtensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.DotvvmContrib-Slider.js",
                Dependencies = new[] { "nouislider" }
            });
        }

        internal static void RegisterRequiredResources(this IDotvvmControl control, IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm-contrib-slider");
            context.ResourceManager.AddRequiredResource("dotvvm-contrib-slider.css");
        }
    }
}
