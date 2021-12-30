using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DotVVM.Contrib.Resources;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    [ControlMarkupOptions(AllowContent = false, DefaultContentProperty = nameof(CookieBar.Rules))]
    public class CookieBar : DotvvmMarkupControl
    {

        [MarkupOptions(AllowBinding = false, MappingMode = MappingMode.InnerElement)]
        public List<CookieBarRule> Rules
        {
            get { return (List<CookieBarRule>)GetValue(RulesProperty); }
            set { SetValue(RulesProperty, value); }
        }
        public static readonly DotvvmProperty RulesProperty
            = DotvvmProperty.Register<List<CookieBarRule>, CookieBar>(c => c.Rules, null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.Title, null);

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.Description, null);

        public string DialogSubtitle
        {
            get { return (string)GetValue(DialogSubtitleProperty); }
            set { SetValue(DialogSubtitleProperty, value); }
        }
        public static readonly DotvvmProperty DialogSubtitleProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.DialogSubtitle, null);

        public string NecessaryCookiesTitle
        {
            get { return (string)GetValue(NecessaryCookiesTitleProperty); }
            set { SetValue(NecessaryCookiesTitleProperty, value); }
        }
        public static readonly DotvvmProperty NecessaryCookiesTitleProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.NecessaryCookiesTitle, null);

        public string NecessaryCookiesDescription
        {
            get { return (string)GetValue(NecessaryCookiesDescriptionProperty); }
            set { SetValue(NecessaryCookiesDescriptionProperty, value); }
        }
        public static readonly DotvvmProperty NecessaryCookiesDescriptionProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.NecessaryCookiesDescription, null);

        public string MoreOptionsButtonText
        {
            get { return (string)GetValue(MoreOptionsButtonTextProperty); }
            set { SetValue(MoreOptionsButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty MoreOptionsButtonTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.MoreOptionsButtonText, null);

        public string AcceptAllButtonText
        {
            get { return (string)GetValue(AcceptAllButtonTextProperty); }
            set { SetValue(AcceptAllButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty AcceptAllButtonTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.AcceptAllButtonText, null);

        public string SaveAndCloseButtonText
        {
            get { return (string)GetValue(SaveAndCloseButtonTextProperty); }
            set { SetValue(SaveAndCloseButtonTextProperty, value); }
        }
        public static readonly DotvvmProperty SaveAndCloseButtonTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.SaveAndCloseButtonText, null);

        public string AlwaysAllowedText
        {
            get { return (string)GetValue(AlwaysAllowedTextProperty); }
            set { SetValue(AlwaysAllowedTextProperty, value); }
        }
        public static readonly DotvvmProperty AlwaysAllowedTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.AlwaysAllowedText, null);

        public string AllowedText
        {
            get { return (string)GetValue(AllowedTextProperty); }
            set { SetValue(AllowedTextProperty, value); }
        }
        public static readonly DotvvmProperty AllowedTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.AllowedText, null);

        public string DisallowedText
        {
            get { return (string)GetValue(DisallowedTextProperty); }
            set { SetValue(DisallowedTextProperty, value); }
        }
        public static readonly DotvvmProperty DisallowedTextProperty
            = DotvvmProperty.Register<string, CookieBar>(c => c.DisallowedText, null);



        public CookieBar()
        {
            Title = CookieTexts.Title;
            Description = CookieTexts.Description;
            DialogSubtitle = CookieTexts.Dialog_Subtitle;
            NecessaryCookiesTitle = CookieTexts.RuleTitle_Necessary;
            NecessaryCookiesDescription = CookieTexts.Rule_Necessary;
            MoreOptionsButtonText = CookieTexts.Btn_MoreOptions;
            AcceptAllButtonText = CookieTexts.Btn_AcceptAll;
            SaveAndCloseButtonText = CookieTexts.Btn_SaveAndClose;
            AlwaysAllowedText = CookieTexts.AlwaysAllowed;
            AllowedText = CookieTexts.Toggle_True;
            DisallowedText = CookieTexts.Toggle_False;

            Rules = new List<CookieBarRule>()
            {
                new CookieBarRule()
                {
                    Key = "analytics_storage",
                    Title = CookieTexts.RuleTitle_Analytics,
                    Description = CookieTexts.Rule_Analytics
                }
            };
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            var script = new HtmlLiteral() { RenderWrapperTag = false };
            script.SetValue(RenderSettings.ModeProperty, RenderMode.Server);
            script.Html = $@"
window.dataLayer = window.dataLayer || [];
function gtag() {{ dataLayer.push(arguments); }}
gtag('consent', 'default', {{
{string.Join(",\n", Rules.Select(r => $"    '{r.Key}': 'denied'"))}
}});";
            var defaultConsent = new HtmlGenericControl("script");
            defaultConsent.Children.Add(script);

            var head = context.View.GetAllDescendants().OfType<HtmlGenericControl>()
                .FirstOrDefault(e => string.Equals(e.TagName, "head", StringComparison.OrdinalIgnoreCase));
            if (head == null)
            {
                throw new DotvvmControlException(this, "The <head> element was not found in the page!");
            }
            head.Children.Insert(0, defaultConsent);

            base.OnPreRender(context);
        }
    }
}

