using System;
using System.Reflection;
using DotVVM.Framework.Compilation;
using DotVVM.Framework.Compilation.Javascript;
using DotVVM.Framework.Compilation.Javascript.Ast;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using Humanizer;

namespace DotVVM.Contrib.Humanizer
{
    public static class HumanizerConfigurationExtensions
    {
        /// <summary>
        /// Registers the Humanizer control library with DotVVM. This registers:
        /// <list type="bullet">
        ///   <item>The <c>dc:HumanizeDateTime</c> control</item>
        ///   <item>JavaScript resources for client-side humanization (dayjs + relativeTime plugin)</item>
        ///   <item>JavaScript translators for <c>Humanize()</c> extension method on <c>DateTime</c></item>
        ///   <item>The <c>Humanizer</c> namespace import so <c>Humanize()</c> can be used in DotVVM value bindings</item>
        /// </list>
        /// </summary>
        public static void AddContribHumanizerConfiguration(this DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(HumanizeDateTime).Assembly.GetName().Name,
                Namespace = typeof(HumanizeDateTime).Namespace,
                TagPrefix = "dc"
            });

            // Import the Humanizer namespace so Humanize() extension methods are accessible in DotVVM bindings
            config.Markup.ImportedNamespaces.Add(new NamespaceImport("Humanizer"));

            var assembly = typeof(HumanizeDateTime).GetTypeInfo().Assembly;

            config.Resources.Register("dayjs", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(assembly,
                    "DotVVM.Contrib.Humanizer.Scripts.dayjs.dayjs.min.js")
            });

            config.Resources.Register("dayjs.relativeTime", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(assembly,
                    "DotVVM.Contrib.Humanizer.Scripts.dayjs.relativeTime.js"),
                Dependencies = new[] { "dayjs" }
            });

            config.Resources.Register("dayjs.locales", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(assembly,
                    "DotVVM.Contrib.Humanizer.Scripts.dayjs.locales.js"),
                Dependencies = new[] { "dayjs" }
            });

            config.Resources.Register("dotvvm.contrib.Humanizer", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(assembly,
                    "DotVVM.Contrib.Humanizer.Scripts.DotVVM.Contrib.Humanizer.js"),
                Dependencies = new[] { "dotvvm", "dayjs.relativeTime", "dayjs.locales" }
            });

            RegisterJavascriptTranslators(config);
        }

        private static void RegisterJavascriptTranslators(DotvvmConfiguration config)
        {
            // Register JS translator for DateTime.Humanize()
            // Note: Humanize() is a static extension method, so context (args[0]) is null.
            //       The date/time value is passed as the first explicit argument (args[1]).
            var translator = new GenericMethodCompiler(args =>
                new JsIdentifierExpression("dotvvmContribHumanizer")
                    .Member("humanizeDateTime")
                    .Invoke(args[1]));

            config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(
                typeof(DateHumanizeExtensions),
                "Humanize",
                translator,
                parameters: new[] { typeof(DateTime), typeof(bool?), typeof(DateTime?), typeof(System.Globalization.CultureInfo) });

            config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(
                typeof(DateHumanizeExtensions),
                "Humanize",
                translator,
                parameters: new[] { typeof(DateTime?), typeof(bool?), typeof(DateTime?), typeof(System.Globalization.CultureInfo) });
        }
    }
}
