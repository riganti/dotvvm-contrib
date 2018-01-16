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

        public static void AddContribTypeAheadConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = "DotVVM.Contrib.TypeAhead",
                Namespace = "DotVVM.Contrib",
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("typeahead", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.typeahead.bundle.min.js"),
                Dependencies = new[] { "jquery" }
            });
            config.Resources.Register("dotvvm.contrib.TypeAhead", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.TypeAhead.js"),
                Dependencies = new[] { "dotvvm", "typeahead", "dotvvm.contrib.TypeAhead.css" }
            });
            config.Resources.Register("dotvvm.contrib.TypeAhead.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(TypeAhead).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.TypeAhead.css")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.TypeAhead"
        }

    }
}
