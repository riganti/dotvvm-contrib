using System.Linq;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {
        public static void AddContribStaticCommandPostBackHandlerConfiguration(this DotvvmConfiguration config)
        {
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }

            config.Resources.Register("dotvvm.contrib.StaticCommandPostBackHandler", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(StaticCommandPostBackHandler).GetTypeInfo().Assembly, 
                                                        "DotVVM.Contrib.Scripts.DotVVM.Contrib.StaticCommandPostBackHandler.js"),
                Dependencies = new [] { "dotvvm"}
            });
        }
    }
}
