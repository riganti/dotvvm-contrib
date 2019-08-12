using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.Samples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddContribQrCodeConfiguration();

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
            config.Resources.Register("jquery", new ScriptResource
            {
                Location = new UrlResourceLocation("~/js/lib/jquery/jquery.min.js")
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
