using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib.TemplateSelector
{
    public static class DotvvmConfigurationExtensions
    {
        public static void AddContribTemplateSelectorConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(TemplateSelector).Assembly.GetName().Name,
                Namespace = typeof(TemplateSelector).Namespace,
                TagPrefix = "dc"
            });
        }

    }
}
