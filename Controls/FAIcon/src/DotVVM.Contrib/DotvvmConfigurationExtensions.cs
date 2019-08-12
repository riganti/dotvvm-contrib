using System;
using System.Linq;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class DotvvmConfigurationExtensions
    {
        private static readonly string assemblyName = typeof(FAIcon.FAIcon).Assembly.GetName().Name;

        public const string ScriptDependencyName = "dotvvm.contrib.FAIcon.script";

        public static void AddContribFAIconConfiguration(this DotvvmConfiguration config)
        {
            RegisterTag(config);

            RegisterScriptResource(config);

            config.Resources.Register("dotvvm.contrib.FAIcon",
                new StylesheetResource()
                {
                    Location = new UrlResourceLocation("https://use.fontawesome.com/releases/v5.3.1/css/all.css"),
                    IntegrityHash = "sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU",
                    VerifyResourceIntegrity = true,
                    Dependencies = new[] { "dotvvm.contrib.FAIcon.script" }
                });

            // NOTE: all resource names should start with "dotvvm.contrib.FAIcon"
        }

        public static void AddContribFAIconConfiguration(this DotvvmConfiguration config, StylesheetResource FAIconResource)
        {
            RegisterTag(config);

            RegisterScriptResource(config);

            AddScriptResourceDependency(FAIconResource);

            config.Resources.Register("dotvvm.contrib.FAIcon", FAIconResource);

            // NOTE: all resource names should start with "dotvvm.contrib.FAIcon"
        }

        private static void AddScriptResourceDependency(StylesheetResource FAIconResource)
        {
            if (!FAIconResource.Dependencies.Contains(ScriptDependencyName))
            {
                var dependecies = new string[FAIconResource.Dependencies.Length + 1];
                Array.Copy(FAIconResource.Dependencies, dependecies, FAIconResource.Dependencies.Length);
                dependecies[FAIconResource.Dependencies.Length] = ScriptDependencyName;
                FAIconResource.Dependencies = dependecies;
            }
        }

        private static void RegisterScriptResource(DotvvmConfiguration config)
        {
            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.FAIcon.script", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(FAIcon.FAIcon).GetTypeInfo().Assembly,
                    $"{assemblyName}.Scripts.DotVVM.Contrib.FAIcon.js"),
                Dependencies = new[] { "dotvvm" }
            });
        }

        private static void RegisterTag(DotvvmConfiguration config)
        {
            // register tag prefix
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(FAIcon.FAIcon).Assembly.GetName().Name,
                Namespace = typeof(FAIcon.FAIcon).Namespace,
                TagPrefix = "dc"
            });
        }
    }
}