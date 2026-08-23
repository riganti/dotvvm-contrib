using System;
using System.Reflection;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.MonacoEditor
{
    public static class MonacoEditorConfigurationExtensions
    {

        /// <summary>
        /// Registers the Monaco editor control and its resources.
        /// </summary>
        /// <param name="config">The DotVVM configuration.</param>
        /// <param name="monacoBaseUrl">The URL containing the Monaco <c>vs</c> directory.</param>
        public static void AddContribMonacoEditorConfiguration(
            this DotvvmConfiguration config,
            string monacoBaseUrl = "https://cdn.jsdelivr.net/npm/monaco-editor@0.55.1/min")
        {
            if (string.IsNullOrWhiteSpace(monacoBaseUrl))
            {
                throw new ArgumentException("The Monaco base URL must not be empty.", nameof(monacoBaseUrl));
            }

            monacoBaseUrl = monacoBaseUrl.TrimEnd('/');

            config.Markup.Controls.Add(new DotvvmControlConfiguration()
            {
                Assembly = typeof(MonacoEditor).Assembly.GetName().Name,
                Namespace = typeof(MonacoEditor).Namespace,
                TagPrefix = "dc"
            });

            config.Resources.Register("dotvvm.contrib.MonacoEditor.loader", new ScriptResource(
                new UrlResourceLocation(monacoBaseUrl + "/vs/loader.js")));

            config.Resources.Register("dotvvm.contrib.MonacoEditor.css", new StylesheetResource(
                new UrlResourceLocation(monacoBaseUrl + "/vs/editor/editor.main.css")));

            config.Resources.Register("dotvvm.contrib.MonacoEditor", new ScriptResource()
            {
                Location = new EmbeddedResourceLocation(typeof(MonacoEditor).GetTypeInfo().Assembly, "DotVVM.Contrib.MonacoEditor.Scripts.DotVVM.Contrib.MonacoEditor.js"),
                Dependencies = new [] { "dotvvm", "dotvvm.contrib.MonacoEditor.loader", "dotvvm.contrib.MonacoEditor.css" }
            });
        }
    }
}
