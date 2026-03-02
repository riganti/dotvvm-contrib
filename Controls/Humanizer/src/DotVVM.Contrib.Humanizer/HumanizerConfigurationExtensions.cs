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
        ///   <item>JavaScript resources for client-side humanization</item>
        ///   <item>JavaScript translators for <c>Humanize()</c> extension methods on <c>DateTime</c>, <c>DateTimeOffset</c>, and <c>TimeSpan</c></item>
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

            config.Resources.Register("dotvvm.contrib.Humanizer", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(
                    typeof(HumanizeDateTime).GetTypeInfo().Assembly,
                    "DotVVM.Contrib.Humanizer.Scripts.DotVVM.Contrib.Humanizer.js"),
                Dependencies = new[] { "dotvvm" }
            });

            RegisterJavascriptTranslators(config);
        }

        private static void RegisterJavascriptTranslators(DotvvmConfiguration config)
        {
            // Register JS translators for DateHumanizeExtensions.Humanize()
            // This covers DateTime, DateTime?, DateTimeOffset, DateTimeOffset?, DateOnly, DateOnly?, TimeOnly, TimeOnly?
            // Note: Humanize() is a static extension method, so context (args[0]) is null.
            //       The date/time value is passed as the first explicit argument (args[1]).
            config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(
                typeof(DateHumanizeExtensions),
                "Humanize",
                new GenericMethodCompiler(args =>
                    new JsIdentifierExpression("dotvvmContribHumanizer")
                        .Member("humanizeDateTime")
                        .Invoke(args[1])),
                allowMultipleMethods: true);

            // Register JS translators for TimeSpanHumanizeExtensions.Humanize()
            // Same as above: args[0] is null (static extension method), args[1] is the TimeSpan value.
            config.Markup.JavascriptTranslator.MethodCollection.AddMethodTranslator(
                typeof(TimeSpanHumanizeExtensions),
                "Humanize",
                new GenericMethodCompiler(args =>
                    new JsIdentifierExpression("dotvvmContribHumanizer")
                        .Member("humanizeTimeSpan")
                        .Invoke(args[1])),
                allowMultipleMethods: true);
        }
    }
}
