using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.Samples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddContribGoogleMapConfiguration(@"TODO GOOGLE AMP KEY");

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
