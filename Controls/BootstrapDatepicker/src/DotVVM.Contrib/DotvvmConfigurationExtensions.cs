using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotVVM.Contrib
{
    public static class BootstrapDatepickerDotvvmConfigurationExtensions
    {
        public static string[] CurrentLocales { get; private set; }

        /// <summary>
        /// Initialize Bootstrap datepicker
        /// </summary>
        /// <param name="locales">Default language is 'en', this collection contains additional languages. For full list of supported languages see <see cref="BootstrapDatepickerConsts.Locales"/></param>
        public static void AddContribBootstrapDatepickerConfiguration(this DotvvmConfiguration config, string[] locales = null)
        {
            // register tag prefix
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = typeof(BootstrapDatepicker).Assembly.GetName().Name,
                    Namespace = typeof(BootstrapDatepicker).Namespace,
                    TagPrefix = "dc"
                });
            }

            if (locales != null)
            {
                var missingLocale = locales.FirstOrDefault(p => !BootstrapDatepickerConsts.Locales.Contains(p, StringComparer.OrdinalIgnoreCase));
                if (!string.IsNullOrWhiteSpace(missingLocale))
                    throw new NotSupportedException($"Locale '{missingLocale}' is not supported in Bootstrap datepicker");

                foreach (var locale in locales)
                {
                    string name = $"dotvvm.contrib.BootstrapDatepicker-{locale}";
                    config.Resources.Register(name, new ScriptResource()
                    {
                        Location = new EmbeddedResourceLocation(typeof(BootstrapDatepicker).GetTypeInfo().Assembly, $"DotVVM.Contrib.Scripts.locales.bootstrap-datepicker.{locale}.min.js"),
                        Dependencies = new[] { "dotvvm.contrib.BootstrapDatepicker-js" }
                    });
                }

                CurrentLocales = locales;
            }

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.BootstrapDatepicker", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapDatepicker).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.BootstrapDatepicker.js"),
                Dependencies = new[] { "dotvvm", "jquery", "bootstrap", "dotvvm.contrib.BootstrapDatepicker-css", "dotvvm.contrib.BootstrapDatepicker-js" }
            });
            config.Resources.Register("dotvvm.contrib.BootstrapDatepicker-js", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapDatepicker).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.bootstrap-datepicker.min.js"),
                Dependencies = new[] { "dotvvm", "dotvvm.contrib.BootstrapDatepicker-css" }
            });
            config.Resources.Register("dotvvm.contrib.BootstrapDatepicker-css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(BootstrapDatepicker).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.bootstrap-datepicker3.standalone.min.css")
            });

            // NOTE: all resource names should start with "dotvvm.contrib.BootstrapDatepicker"
        }

    }
}
