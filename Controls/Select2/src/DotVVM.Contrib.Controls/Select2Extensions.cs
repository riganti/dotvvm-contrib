using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.Controls
{
    public static class Select2Extensions
    {

        /// <summary>
        /// Extends the DotVVM configuration with Select2 control.
        /// </summary>
        public static void AddSelect2Configuration(this DotvvmConfiguration config)
        {
            RegisterControls(config);
            RegisterResources(config);
        }

        private static void RegisterControls(DotvvmConfiguration config)
        {
            config.Markup.AddCodeControl("dc", typeof (Select2Extensions).Namespace, typeof (Select2Extensions).Assembly.GetName().Name);
        }

        private static void RegisterResources(DotvvmConfiguration config)
        {
            config.Resources.Register("select2", new ScriptResource()
            {
                EmbeddedResourceAssembly = typeof(Select2Extensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.select2.select2.min.js",
                Dependencies = new [] { "dotvvm", "select2.css", "jquery" }
            });
            config.Resources.Register("select2.css", new StylesheetResource()
            {
                EmbeddedResourceAssembly = typeof(Select2Extensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.select2.select2.min.css"
            });
            config.Resources.Register("dotvvm-contrib-select2", new ScriptResource()
            {
                EmbeddedResourceAssembly = typeof(Select2Extensions).Assembly.GetName().Name,
                Url = "DotVVM.Contrib.Controls.Resources.DotvvmContrib-Select2.js",
                Dependencies = new [] { "select2" }
            });
        }
    }
    
}
