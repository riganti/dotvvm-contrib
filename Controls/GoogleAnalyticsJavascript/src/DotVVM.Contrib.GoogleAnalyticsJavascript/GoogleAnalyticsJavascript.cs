using System;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotVVM.Contrib.GoogleAnalyticsJavascript
{
    /// <summary>
    /// Renders a javascript tracking snippet for Google Analytics.
    /// The control should be added near the top of the <head> tag and before any other script or CSS tags
    /// For more information visit google documentation (https://developers.google.com/analytics/devguides/collection/analyticsjs/)
    /// </summary>
    public class GoogleAnalyticsJavascript : DotvvmControl
    {
        private static readonly string AsyncJavascriptSnippet = Resources.AsyncJavascriptSnippet;
        private static readonly string JavascriptSnippet = Resources.JavascriptSnippet;
        private static readonly string PageViewJavascriptSnippet = Resources.PageViewJavascriptSnippet;

        /// <summary>
        /// The tracking ID is a string like UA-000000-2. It must be included in your tracking 
        /// code to tell Analytics which account and property to send data to.
        /// For more information visit https://support.google.com/analytics/answer/7372977?hl=en
        /// </summary>
        public string TrackingId
        {
            get { return (string)GetValue(TrackingIdProperty); }
            set { SetValue(TrackingIdProperty, value); }
        }
        public static readonly DotvvmProperty TrackingIdProperty
            = DotvvmProperty.Register<string, GoogleAnalyticsJavascript>(c => c.TrackingId, null);

        /// <summary>
        /// Adds another command to the ga() command queue to send a pageview 
        /// to Google Analytics for the current page. For more information visit 
        /// https://developers.google.com/analytics/devguides/collection/analyticsjs/sending-hits
        /// </summary>
        public bool? PageViewEnabled
        {
            get { return (bool?)GetValue(PageViewEnabledProperty); }
            set { SetValue(PageViewEnabledProperty, value); }
        }
        public static readonly DotvvmProperty PageViewEnabledProperty
            = DotvvmProperty.Register<bool?, GoogleAnalyticsJavascript>(c => c.PageViewEnabled, null);

        /// <summary>
        /// Enables alternative async tracking javascript snippet, which adds support for preloading.
        /// For more information visit https://developers.google.com/analytics/devguides/collection/analyticsjs/#alternative_async_tracking_snippet
        /// </summary>
        public bool? AsyncVersionEnabled
        {
            get { return (bool?)GetValue(AsyncVersionEnabledProperty); }
            set { SetValue(AsyncVersionEnabledProperty, value); }
        }
        public static readonly DotvvmProperty AsyncVersionEnabledProperty
            = DotvvmProperty.Register<bool?, GoogleAnalyticsJavascript>(c => c.AsyncVersionEnabled, null);

        protected override void RenderControl(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var doNotTrack = false;
            if (context.HttpContext.Request.Headers.TryGetValue("DNT", out var doNotTrackHeaderValue))
            {
                doNotTrack = string.Equals(doNotTrackHeaderValue, "1");
            }

            if (!doNotTrack)
            {
                var options = ResolveOptions(context);

                string additionalJS = options.PageViewEnabled ? PageViewJavascriptSnippet :string.Empty;

                var snippet = options.AsyncVersionEnabled ? AsyncJavascriptSnippet : JavascriptSnippet;
                var script = string.Format(snippet, options.TrackingId, additionalJS);

                writer.WriteUnencodedText(script);
            }

            base.RenderControl(writer, context);
        }

        private GoogleAnalyticsOptions ResolveOptions(IDotvvmRequestContext context)
        {
            var options =  context.Services.GetRequiredService<IOptions<GoogleAnalyticsOptions>>().Value;

            var resultOptions = new GoogleAnalyticsOptions()
            {
                TrackingId = !string.IsNullOrEmpty(TrackingId) ? TrackingId : options.TrackingId,
                PageViewEnabled = PageViewEnabled ?? options.PageViewEnabled,
                AsyncVersionEnabled = AsyncVersionEnabled ?? options.AsyncVersionEnabled
            };

            if (string.IsNullOrEmpty(resultOptions.TrackingId))
            {
                throw new ArgumentException("Google Analytics options missing (TrackingId is required). " +
                    "Set control property TrackingId " +
                    "or register GoogleAnalyticsOptions service in container");
            }

            return resultOptions;
        }
    }
}
