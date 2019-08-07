using System.Linq;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib
{
    public static class QrCodeConfigurationExtensions
    {
        public static void AddContribQrCodeConfiguration(this DotvvmConfiguration config)
        {
            // register tag prefix
            if (!config.Markup.Controls.Any(c => c.TagPrefix == "dc"))
            {
                config.Markup.Controls.Add(new DotvvmControlConfiguration()
                {
                    Assembly = "DotVVM.Contrib.QrCode",
                    Namespace = "DotVVM.Contrib",
                    TagPrefix = "dc"
                });
            }

            config.Resources.Register("qrcode", new ScriptResource
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.jquery.qrcode.min.js"),
                Dependencies = new[] { "jquery" }
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.QrCode", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.Scripts.DotVVM.Contrib.QrCode.js"),
                Dependencies = new[] { "dotvvm", "qrcode", "dotvvm.contrib.QrCode.css" }
            });
            config.Resources.Register("dotvvm.contrib.QrCode.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.Styles.DotVVM.Contrib.QrCode.css")
            });
        }
    }
}
