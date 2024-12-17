using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.SvgParser
{
    public static class SvgParserConfigurationExtensions
    {

        public static void AddContribSvgParserConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(SvgParser).Assembly.GetName().Name,
                Namespace = typeof(SvgParser).Namespace,
                TagPrefix = "dc"
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.SvgParser", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(SvgParser).GetTypeInfo().Assembly, "DotVVM.Contrib.SvgParser.Scripts.DotVVM.Contrib.SvgParser.js"),
                Dependencies = new[] { 
                    "dotvvm",
                    //"dotvvm.contrib.SvgParser.css" 
                }
            });
            //config.Resources.Register("dotvvm.contrib.SvgParser.css", new StylesheetResource()
            //{
            //    Location = new EmbeddedResourceLocation(typeof(SvgParser).GetTypeInfo().Assembly, "DotVVM.Contrib.SvgParser.Styles.DotVVM.Contrib.SvgParser.css")
            //});

            // NOTE: all resource names should start with "dotvvm.contrib.SvgParser"
        }

    }
}
