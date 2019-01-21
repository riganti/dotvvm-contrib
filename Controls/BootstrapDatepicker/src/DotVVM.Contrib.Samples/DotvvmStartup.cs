using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace DotVVM.Contrib.Samples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.DefaultCulture = "cs";
            config.AddContribBootstrapDatepickerConfiguration(new[] { "cs", "fr" });

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

            config.Resources.Register("jquery", new ScriptResource()
            {
                Location = new UrlResourceLocation("https://code.jquery.com/jquery-3.3.1.min.js")
            });
            config.Resources.Register("bootstrap", new ScriptResource()
            {
                Location = new UrlResourceLocation("https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.bundle.min.js"),
                Dependencies = new[] { "jquery", "bootstrap-css" }
            });
            config.Resources.Register("bootstrap-css", new StylesheetResource()
            {
                Location = new UrlResourceLocation("https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css")
            });

            config.Resources.Register("fontawesome", new StylesheetResource()
            {
                Location = new UrlResourceLocation("https://use.fontawesome.com/releases/v5.6.3/css/all.css")
            });
        }

        public void ConfigureServices(IDotvvmServiceCollection services)
        {
            services.AddDefaultTempStorages("Temp");

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
