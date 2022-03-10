using System.Text.RegularExpressions;
using DotVVM.Contrib.GoogleAnalyticsJavascript.Tests.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.GoogleAnalyticsJavascript.Tests
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

                var mainScript = browser.ElementAt("body script", 0);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Z");
                Assert.DoesNotContain("ga('send', 'pageview');", scriptContent);
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
                Assert.Contains("ga('send', 'pageview');", scriptContent);
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
                Assert.DoesNotContain("ga('send', 'pageview');", scriptContent);
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
