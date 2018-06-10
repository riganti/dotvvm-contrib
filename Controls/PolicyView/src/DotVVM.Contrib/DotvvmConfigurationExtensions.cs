using System.Linq;
using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {
        public static void AddContribPolicyViewConfiguration(this DotvvmConfiguration config)
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
        }
    }
}
