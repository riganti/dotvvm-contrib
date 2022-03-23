using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.QrCode
{
    public static class QrCodeConfigurationExtensions
    {
        public static void AddContribQrCodeConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(QrCode).Assembly.GetName().Name,
                Namespace = typeof(QrCode).Namespace,
                TagPrefix = "dc"
            });

            config.Resources.Register("qrcode", new ScriptResource
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.QrCode.Scripts.jquery.qrcode.min.js"),
                Dependencies = new[] { "jquery" }
            });

            // register additional resources for the control and set up dependencies
            config.Resources.Register("dotvvm.contrib.QrCode", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.QrCode.Scripts.DotVVM.Contrib.QrCode.js"),
                Dependencies = new[] { "dotvvm", "qrcode", "dotvvm.contrib.QrCode.css" }
            });
            config.Resources.Register("dotvvm.contrib.QrCode.css", new StylesheetResource()
            {
                Location = new EmbeddedResourceLocation(typeof(QrCode).GetTypeInfo().Assembly, "DotVVM.Contrib.QrCode.Styles.DotVVM.Contrib.QrCode.css")
            });
        }
    }
}
