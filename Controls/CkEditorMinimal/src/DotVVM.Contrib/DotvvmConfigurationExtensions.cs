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

        public static void AddContribCkEditorMinimalConfiguration(this DotvvmConfiguration config)
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
            config.Resources.Register("dotvvm.contrib.CkEditorMinimal", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(CkEditorMinimal).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.CkEditorMinimal.js"),
                Dependencies = new [] { "dotvvm" }
            });
        

            // NOTE: all resource names should start with "dotvvm.contrib.CkEditorMinimal"
        }

    }
}
