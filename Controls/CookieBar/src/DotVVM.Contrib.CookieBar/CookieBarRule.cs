using System.Text.Json.Serialization;
using DotVVM.Contrib.CookieBar.Resources;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;

namespace DotVVM.Contrib.CookieBar
{
    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class CookieBarRule : DotvvmBindableObject
    {
        [MarkupOptions(AllowBinding = false)]
        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public static readonly DotvvmProperty KeyProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Key, null);

        [MarkupOptions(AllowBinding = false)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Title, null);

        [MarkupOptions(AllowBinding = false)]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Description, null);

        public string[] CookieNameRegexes
        {
            get { return (string[])GetValue(CookieNameRegexesProperty); }
            set { SetValue(CookieNameRegexesProperty, value); }
        }

        public static readonly DotvvmProperty CookieNameRegexesProperty
            = DotvvmProperty.Register<string[], CookieBarRule>(c => c.CookieNameRegexes, null);
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class GoogleAnalyticsRule : CookieBarRule
    {
        public GoogleAnalyticsRule()
        {
            Key = "analytics_storage";
            Title = CookieTexts.RuleTitle_Analytics;
            Description = CookieTexts.Rule_Analytics;
            CookieNameRegexes = new[] { "^_ga(?:_.+)?$", "^_gid$", "^_gat(?:_.+)?$", "^_gac_.+$", "^__utm[abctvz]$", "^_dc_gtm_.+$", "^AMP_TOKEN$", "^FPID$", "^FPLC$" };
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class GoogleAdsRule : CookieBarRule
    {
        public GoogleAdsRule()
        {
            Key = "ad_storage";
            Title = CookieTexts.RuleTitle_GoogleAds;
            Description = CookieTexts.Rule_GoogleAds;
            CookieNameRegexes = new[] { "^_gcl_.+$", "^_gac_.+$", "^FPGCL(?:AW|GB|DC|GS)$", "^FPAU$", "^FPGSID$" };
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class GoogleAdsPersonalizationRule : CookieBarRule
    {
        public GoogleAdsPersonalizationRule()
        {
            Key = "ad_personalization";
            Title = CookieTexts.RuleTitle_GoogleAdsPersonalization;
            Description = CookieTexts.Rule_GoogleAdsPersonalization;
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class GoogleAdsUserDataRule : CookieBarRule
    {
        public GoogleAdsUserDataRule()
        {
            Key = "ad_user_data";
            Title = CookieTexts.RuleTitle_GoogleAdsUserData;
            Description = CookieTexts.Rule_GoogleAdsUserData;
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class FacebookPixelRule : CookieBarRule
    {
        public FacebookPixelRule()
        {
            Key = "fbpixel_storage";
            Title = CookieTexts.RuleTitle_FacebookPixel;
            Description = CookieTexts.Rule_FacebookPixel;
            CookieNameRegexes = new[] { "^_fbp$", "^_fbc$" };
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class SmartlookRule : CookieBarRule
    {
        public SmartlookRule()
        {
            Key = "smartlook_storage";
            Title = CookieTexts.RuleTitle_Smartlook;
            Description = CookieTexts.Rule_Smartlook;
            CookieNameRegexes = new[] { "^SL$", "^SL_C_.+_(?:SID|VID|KEY|DOMAIN)$" };
        }
    }

    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class ClarityRule : CookieBarRule
    {
        public ClarityRule()
        {
            Key = "clarity_storage";
            Title = CookieTexts.RuleTitle_Clarity;
            Description = CookieTexts.Rule_Clarity;
            CookieNameRegexes = new[] { "^_clck$", "^_clsk$" };
        }
    }
}