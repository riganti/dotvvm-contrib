using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.NoUiSlider
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddContribNoUiSliderConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(Slider).Assembly.GetName().Name,
                Namespace = typeof(Slider).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.NoUiSlider", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.NoUiSlider.Scripts.DotVVM.Contrib.NoUiSlider.js"),
                Dependencies = new[] { "dotvvm", "NoUiSlider", "dotvvm.contrib.NoUiSlider.css" }
            });
            config.Resources.Register("NoUiSlider", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.NoUiSlider.Scripts.NoUiSlider.nouislider.min.js")
            });

            config.Resources.Register("dotvvm.contrib.NoUiSlider.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.NoUiSlider.Styles.DotVVM.Contrib.NoUiSlider.css"),
                Dependencies = new[] { "NoUiSlider.css" }
            });
            config.Resources.Register("NoUiSlider.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.NoUiSlider.Scripts.NoUiSlider.nouislider.min.css")
            });
        }

    }
}
