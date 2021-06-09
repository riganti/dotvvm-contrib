using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class FileUploadNoIframeConfigurationExtensions
    {

        public static void AddContribFileUploadNoIframeConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(FileUploadNoIframe).Assembly.GetName().Name,
                Namespace = typeof(FileUploadNoIframe).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.FileUploadNoIframe", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(FileUploadNoIframe).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.FileUploadNoIframe.js")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.FileUploadNoIframe"
        }

    }
}
