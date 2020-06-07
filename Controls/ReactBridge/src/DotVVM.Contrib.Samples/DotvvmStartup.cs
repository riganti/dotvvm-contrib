using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotVVM.Contrib.Samples.Chartist;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.Samples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        public void ConfigureServices(IDotvvmServiceCollection services)
        {
            services.AddDefaultTempStorages("Temp");
        }

        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddReactBridgeConfiguration();

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
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(Chartist.Chartist).Assembly.GetName().Name,
                Namespace = typeof(Chartist.Chartist).Namespace,
                TagPrefix = "dc"
            });
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("react-trend", new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-trend@1.2.4/umd/react-trend.js")) { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types-DD", new ScriptResource(new UrlResourceLocation("https://unpkg.com/prop-types/prop-types.js"))
            { Dependencies = new[] { "react" } });
            config.Resources.Register("prop-types", new InlineScriptResource("window['prop-types'] = window['PropTypes']")
            {
                Dependencies = new[] { "prop-types-DD" }
            });
            config.Resources.Register("react-numeric-input",
                new ScriptResource(new UrlResourceLocation("https://unpkg.com/react-numeric-input@2.1.0/dist/react-numeric-input.js"))
                {
                    Dependencies = new[] { "react", "prop-types", "ReactBridge" }
                });

            config.Resources.Register("chartist-css",
               new StylesheetResource(new FileResourceLocation("wwwroot/Styles/chartist.min.css")));

            config.Resources.Register("chartist-js",
         new ScriptResource(new UrlResourceLocation("https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.js"))
         {
             LocationFallback = new ResourceLocationFallback(
                 "window.Chartist",
                 new FileResourceLocation("wwwroot/Script/chartist.min.js")),
             Dependencies = new[] { "chartist-css" }
         });

            config.Resources.Register("ReactChartist", new ScriptResource(
                new FileResourceLocation("wwwroot/Scripts/ReactChartis.js"))
            {
                Dependencies = new[] { "dotvvm", "react", "prop-types", "chartist-js", "ReactBridge" }
            });
        }
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
