using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddContribNoUiSliderConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = "DotVVM.Contrib.NoUiSlider",
                Namespace = "DotVVM.Contrib",
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.NoUiSlider", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.NoUiSlider.js"),
                Dependencies = new[] { "dotvvm", "NoUiSlider", "dotvvm.contrib.NoUiSlider.css" }
            });
            config.Resources.Register("NoUiSlider", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.NoUiSlider.nouislider.min.js")
            });

            config.Resources.Register("dotvvm.contrib.NoUiSlider.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.NoUiSlider.css"),
                Dependencies = new[] { "NoUiSlider.css" }
            });
            config.Resources.Register("NoUiSlider.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Slider).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.NoUiSlider.nouislider.min.css")
            });
        }

    }
}
