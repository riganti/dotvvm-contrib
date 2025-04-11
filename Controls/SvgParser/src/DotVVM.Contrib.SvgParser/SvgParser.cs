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
    /// Renders the contents of the specified SVG file.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
	public class SvgParser : DotvvmControl
    {
        private readonly DotvvmConfiguration config;

        public SvgParser(DotvvmConfiguration config)
        {
            this.config = config;
        }

		/// <summary>
		/// Gets or sets the path to the SVG file.
		/// If the value is rendered on the server (hard-coded value or resource binding), the path should be application-relative.
		/// If the value is rendered on the client (value binding), the URL should be publicly accessible.
		/// If the path starts with "embedded://", the file is loaded from the assembly resources.
		/// </summary>
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

        protected override void RenderContents(IHtmlWriter writer, IDotvvmRequestContext context)
        {
	        if (GetValueBinding(SourceProperty) != null)
	        {
                // render as value binding
		        writer.AddKnockoutDataBind("dotvvm-contrib-SvgParser", this, SourceProperty);
		        writer.RenderBeginTag("svg");
		        writer.RenderEndTag();
			}
	        else
	        {
				// render hard-coded value
				Stream stream = null;
				try
				{
					if (Source.StartsWith("embedded://", StringComparison.Ordinal))
					{
						stream = LoadEmbeddedResource();
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
        }

        private Stream LoadEmbeddedResource()
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
	        var stream = assembly.GetManifestResourceStream(resourceName);
	        if (stream == null)
	        {
		        throw new DotvvmControlException(
			        $"File could not be loaded from resource {resourceName}.");
	        }
	        return stream;
        }
    }
}
