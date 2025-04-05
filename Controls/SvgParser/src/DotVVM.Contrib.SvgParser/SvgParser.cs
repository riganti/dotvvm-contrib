using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.IO;
using System.Reflection;
using System;
using DotVVM.Framework.Configuration;

namespace DotVVM.Contrib.SvgParser
{
    /// <summary>
    /// Renders a ...
    /// </summary>
    public class SvgParser : HtmlGenericControl
    {
        private readonly DotvvmConfiguration config;

        public SvgParser(DotvvmConfiguration config) : base("svg")
        {
            this.config = config;
        }


        [MarkupOptions(Required = true, AllowBinding = true)]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DotvvmProperty SourceProperty
            = DotvvmProperty.Register<string, SvgParser>(c => c.Source, "");


        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.SvgParser");

            base.OnPreRender(context);
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            if (RenderOnServer)
            {
                Stream stream = null;
                try
                {
                    if (Source.StartsWith("embedded://", StringComparison.Ordinal))
                    {
                        var resourceName = Source.Remove(0, "embedded://".Length);
                        if (resourceName.IndexOf('/') == -1 || resourceName.IndexOf('/') == 0)
                        {
                            throw new DotvvmControlException(
                                "Wrong format of embedded resource in the Source property.");
                        }

                        var assemblyName = resourceName.Substring(0, resourceName.IndexOf('/'));
                        Assembly assembly = null;
                        try
                        {
                            assembly = Assembly.Load(new AssemblyName(assemblyName));
                        }
                        catch (FileLoadException)
                        {
                            throw new DotvvmControlException($"Assembly {assemblyName} was not found.");
                        }

                        resourceName = resourceName.Replace('/', '.');
                        if (assembly.GetManifestResourceInfo(resourceName) == null)
                        {
                            throw new DotvvmControlException(
                                $"Resource {resourceName} was not found in assembly {assemblyName}.");
                        }

                        //load the file
                        stream = assembly.GetManifestResourceStream(resourceName);
                        if (stream == null)
                        {
                            throw new DotvvmControlException(
                                $"File could not be loaded from resource {resourceName}.");
                        }
                    }
                    else
                    {
                        var path = Path.Combine(config.ApplicationPhysicalPath, Source);
                        stream = File.OpenRead(path);
                    }

                    using (var streamReader = new StreamReader(stream))
                    {
                        var svgString = streamReader.ReadToEnd();
                        writer.WriteUnencodedText(string.Join(string.Empty, svgString));

                    }
                }
                finally
                {
                    stream?.Dispose();
                }
            }
            else
            {
                var path = Source;
                writer.AddKnockoutDataBind("dotvvm-contrib-SvgParser", $"'{path}'");
                writer.RenderSelfClosingTag("svg");
            }
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }



    }
}
