using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;

namespace DotVVM.Contrib.CkEditor5Minimal
{
    /// <summary>
    /// Renders a textarea enhanced with CKEditor 5.
    /// </summary>
    public class CkEditor5Minimal : HtmlGenericControl
    {
        private string configModuleUrl;

        public CkEditor5Minimal() : base("textarea", true)
        {
        }

        /// <summary>
        /// Gets or sets the HTML content of the editor.
        /// </summary>
        [MarkupOptions(Required = true, AllowHardCodedValue = false)]
        public string Html
        {
            get { return Convert.ToString(GetValue(HtmlProperty)); }
            set { SetValue(HtmlProperty, value); }
        }

        public static readonly DotvvmProperty HtmlProperty =
            DotvvmProperty.Register<string, CkEditor5Minimal>(t => t.Html, "");

        /// <summary>
        /// Gets or sets the name of a <see cref="ScriptModuleResource"/> that exports a custom editor factory.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string ConfigResourceName
        {
            get { return Convert.ToString(GetValue(ConfigResourceNameProperty)); }
            set { SetValue(ConfigResourceNameProperty, value); }
        }

        public static readonly DotvvmProperty ConfigResourceNameProperty =
            DotvvmProperty.Register<string, CkEditor5Minimal>(t => t.ConfigResourceName, null);

        /// <summary>
        /// Gets or sets the optional configuration name passed to a custom editor factory.
        /// </summary>
        public string ConfigName
        {
            get { return Convert.ToString(GetValue(ConfigNameProperty)); }
            set { SetValue(ConfigNameProperty, value); }
        }

        public static readonly DotvvmProperty ConfigNameProperty =
            DotvvmProperty.Register<string, CkEditor5Minimal>(t => t.ConfigName, null);

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.CkEditor5Minimal");
            if (!string.IsNullOrWhiteSpace(ConfigResourceName))
            {
                var resource = context.ResourceManager.FindResource(ConfigResourceName);
                if (!(resource is ScriptModuleResource moduleResource))
                {
                    throw new InvalidOperationException(
                        $"The resource '{ConfigResourceName}' specified by {nameof(ConfigResourceName)} must be a {nameof(ScriptModuleResource)}.");
                }

                context.ResourceManager.AddRequiredResource(ConfigResourceName);
                configModuleUrl = context.TranslateVirtualPath(moduleResource.Location.GetUrl(context, ConfigResourceName));
            }
            base.OnPreRender(context);
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            AddDotvvmUniqueIdAttribute();
            base.AddAttributesToRender(writer, context);

            var group = new KnockoutBindingGroup();
            group.Add("html", this, HtmlProperty);
            group.Add("configName", this, ConfigNameProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-CkEditor5Minimal", group);
            if (configModuleUrl != null)
            {
                writer.AddAttribute("data-config-module-url", configModuleUrl);
            }
        }
    }
}
