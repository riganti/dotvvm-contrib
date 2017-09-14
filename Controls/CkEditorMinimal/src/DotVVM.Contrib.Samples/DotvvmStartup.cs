using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.Samples
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddContribCkEditorMinimalConfiguration();

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("_Default", "", "Views/_default.dothtml");
            config.RouteTable.AutoDiscoverRoutes(new SamplesRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources

            config.Resources.Register("ckeditor", new ScriptResource(new UrlResourceLocation("~/Content/Lib/ckeditor/ckeditor.js"))
            {
                Dependencies = new[] { "jquery" }
            });
            config.Resources.Register("ckeditorConfig", new ScriptResource(new UrlResourceLocation("~/Content/Lib/ckeditor/config.js")));
        }
    }

    internal class SamplesRouteStrategy : DefaultRouteStrategy
    {
        public SamplesRouteStrategy(DotvvmConfiguration config) : base(config)
        {
        }

        protected override IEnumerable<RouteStrategyMarkupFileInfo> DiscoverMarkupFiles()
        {
            return base.DiscoverMarkupFiles().Where(r => !r.ViewsFolderRelativePath.StartsWith("_"));
        }
    }
}
