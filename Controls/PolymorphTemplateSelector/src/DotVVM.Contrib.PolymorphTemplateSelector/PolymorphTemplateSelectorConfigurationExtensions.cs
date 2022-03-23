using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib.PolymorphTemplateSelector
{
    public static class PolymorphTemplateSelectorConfigurationExtensions
    {

        public static void AddContribPolymorphTemplateSelectorConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(PolymorphTemplateSelector).Assembly.GetName().Name,
                Namespace = typeof(PolymorphTemplateSelector).Namespace,
                TagPrefix = "dc"
            });
        }

    }
}
