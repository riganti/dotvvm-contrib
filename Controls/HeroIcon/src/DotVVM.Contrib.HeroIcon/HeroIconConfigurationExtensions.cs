using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib.HeroIcon
{
    public static class HeroIconConfigurationExtensions
    {

        public static void AddContribHeroIconConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(HeroIcon).Assembly.GetName().Name,
                Namespace = typeof(HeroIcon).Namespace,
                TagPrefix = "dc"
            });

            // NOTE: all resource names should start with "dotvvm.contrib.HeroIcon"
        }

    }
}
