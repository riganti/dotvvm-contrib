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

        public static void AddContribSelect2Configuration(this DotvvmConfiguration config)
        {
            // register tag prefix
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }

            // register additional resources for the control and set up dependencies
            config.Resources.Register("select2", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Select2).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.select2.select2.min.js"),
                Dependencies = new[] { "dotvvm", "select2.css", "jquery" }
            });
            config.Resources.Register("select2.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Select2).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.select2.select2.min.css")
            });


            config.Resources.Register("dotvvm.contrib.Select2", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(Select2).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.Select2.js"),
                Dependencies = new [] { "select2" }
            });
        }

    }
}
