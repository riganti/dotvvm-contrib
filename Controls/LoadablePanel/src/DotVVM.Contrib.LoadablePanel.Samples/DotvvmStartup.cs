using System.Collections.Generic;
using System.Linq;
using DotVVM.Contrib.LoadablePanel.Samples.Controls.Pager;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.LoadablePanel.Samples
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.AddContribLoadablePanelConfiguration();

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("_Default", "", "Pages/Default/_default.dothtml");
            config.RouteTable.Add("Sample1", "sample1", "Pages/Sample1/Sample1.dothtml");
            config.RouteTable.Add("Sample2", "sample2", "Pages/Sample2/Sample2.dothtml");
            config.RouteTable.Add("Sample3", "sample3", "Pages/Sample3/Sample3.dothtml");
            config.RouteTable.Add("PagingRepeater", "PagingRepeater", "Pages/PagingRepeater/PagingRepeater.dothtml");
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            config.Markup.ImportedNamespaces.Add(new NamespaceImport(typeof(PagerExtensions).FullName, "_pager"));
            config.Markup.AddMarkupControl("cc", "Pager", "Controls/Pager/Pager.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            config.Resources.Register("style-css", new StylesheetResource()
            {
                Location = new FileResourceLocation("Resources/css/style.css")
            });

            config.Resources.Register("pager-js", new ScriptResource(new FileResourceLocation("Controls/Pager/Pager.js"), defer: true));
        }

        public void ConfigureServices(IDotvvmServiceCollection options)
        {
            options.AddDefaultTempStorages("Temp");
            options.Services.AddPagerExtensions();
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
