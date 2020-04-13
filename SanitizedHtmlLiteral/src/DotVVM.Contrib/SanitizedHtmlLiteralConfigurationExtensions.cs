using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class SanitizedHtmlLiteralConfigurationExtensions
    {

        public static void AddContribSanitizedHtmlLiteralConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(SanitizedHtmlLiteral).Assembly.GetName().Name,
                Namespace = typeof(SanitizedHtmlLiteral).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.SanitizedHtmlLiteral", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(SanitizedHtmlLiteral).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.SanitizedHtmlLiteral.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.SanitizedHtmlLiteral.css" }
            });
            config.Resources.Register("dotvvm.contrib.SanitizedHtmlLiteral.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(SanitizedHtmlLiteral).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.SanitizedHtmlLiteral.css")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.SanitizedHtmlLiteral"
        }

    }
}
