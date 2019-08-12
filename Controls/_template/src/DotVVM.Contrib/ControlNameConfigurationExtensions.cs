using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class ControlNameConfigurationExtensions
    {

        public static void AddContribControlNameConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(ControlName).Assembly.GetName().Name,
                Namespace = typeof(ControlName).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.ControlName", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(ControlName).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.ControlName.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.ControlName.css" }
            });
            config.Resources.Register("dotvvm.contrib.ControlName.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(ControlName).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.ControlName.css")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.ControlName"
        }

    }
}
