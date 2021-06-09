using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib
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
