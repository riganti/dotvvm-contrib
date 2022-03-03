using System;
using System.Text.RegularExpressions;
using DotVVM.Contrib.Tests.Core;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class GoogleAnalyticsJavascriptTests : AppSeleniumTest
    {
        public GoogleAnalyticsJavascriptTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void GoogleAnalyticsJavascript_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var mainScript = browser.ElementAt("body script", 1);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Z");
                Assert.False(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        [Fact]
        public void GoogleAnalyticsJavascript_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var mainScript = browser.ElementAt("body script", 0);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Y");
                Assert.True(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        [Fact]
        public void GoogleAnalyticsJavascript_Sample3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample3");

                var mainScript = browser.ElementAt("body script", 0);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Z");
                Assert.False(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        private string CheckIfScriptExists(IElementWrapper mainScript, string trackingId)
        {
            var scriptContent = mainScript.GetJsInnerHtml();
            var match = Regex.Match(scriptContent, @"ga\('create', '(.+)', 'auto'\)");

            Assert.NotNull(mainScript);
            Assert.True(match.Success);
            Assert.Equal(match.Groups[1].Value, trackingId);
            return scriptContent;
        }

    
    }
}
