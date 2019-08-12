using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a bootstrap-datepicker input
    /// </summary>
    public class BootstrapDatepicker : HtmlGenericControl
    {
        public BootstrapDatepicker() : base("input")
        {
        }

        [MarkupOptions(Required = true)]
        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public static readonly DotvvmProperty DateProperty
            = DotvvmProperty.Register<DateTime?, BootstrapDatepicker>(c => c.Date, null);

        [MarkupOptions(AllowBinding = false)]
        public string Language
        {
            get { return (string)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }
        public static readonly DotvvmProperty LanguageProperty
            = DotvvmProperty.Register<string, BootstrapDatepicker>(c => c.Language, null);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            context.ResourceManager.AddCurrentCultureGlobalizationResource();

            IValueBinding dateBinding = null;
            foreach (var item in Properties)
                if (item.Key == DateProperty)
                    dateBinding = item.Value as IValueBinding;

            if (dateBinding == null)
            {
                var expression = dateBinding.GetKnockoutBindingExpression(this);
                expression = "dotvvm.globalize.formatString(" + JsonConvert.ToString("d.M.yyyy") + ", " + expression + ")";
                writer.AddKnockoutDataBind("dotvvm-contrib-BootstrapDatepicker", expression);
            }
            else
            {
                writer.AddKnockoutDataBind("dotvvm-contrib-BootstrapDatepicker", this, DateProperty, renderEvenInServerRenderingMode: true);
            }

            if (!string.IsNullOrWhiteSpace(Language))
                writer.AddAttribute("data-date-language", Language);

            base.AddAttributesToRender(writer, context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.BootstrapDatepicker");

            var language = Language;

            if (BootstrapDatepickerDotvvmConfigurationExtensions.CurrentLocales != null)
            {
                if (string.IsNullOrWhiteSpace(language))
                {
                    language = CultureInfo.CurrentUICulture.Name;
                    if (!BootstrapDatepickerDotvvmConfigurationExtensions.CurrentLocales.Any(p => p.Equals(language, StringComparison.OrdinalIgnoreCase)))
                    {
                        language = language.Substring(0, 2);
                        if (!BootstrapDatepickerDotvvmConfigurationExtensions.CurrentLocales.Any(p => p.Equals(language, StringComparison.OrdinalIgnoreCase)))
                            throw new Exception($"Language {CultureInfo.CurrentUICulture.Name} is not supported");
                    }
                }

                if (BootstrapDatepickerConsts.DefaultLocale.Equals(language, StringComparison.OrdinalIgnoreCase))
                    language = null;
            }

            Language = language;
            if (!string.IsNullOrWhiteSpace(language))
                context.ResourceManager.AddRequiredResource($"dotvvm.contrib.BootstrapDatepicker-{language}");

            base.OnPreRender(context);
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderSelfClosingTag(TagName);
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }
    }
}
