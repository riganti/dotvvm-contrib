using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class GoogleAnalyticsJavascriptTests : SeleniumTestBase
    {
        [TestMethod]
        public void GoogleAnalyticsJavascript_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var mainScript = browser.ElementAt("body script", 1);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Z");
                Assert.IsFalse(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        [TestMethod]
        public void GoogleAnalyticsJavascript_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var mainScript = browser.ElementAt("body script", 0);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Y");
                Assert.IsTrue(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        [TestMethod]
        public void GoogleAnalyticsJavascript_Sample3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample3");

                var mainScript = browser.ElementAt("body script", 0);
                var scriptContent = CheckIfScriptExists(mainScript, "UA-XXXXX-Z");
                Assert.IsFalse(scriptContent.Contains("ga('send', 'pageview');"));
            });
        }

        private string CheckIfScriptExists(ElementWrapper mainScript, string trackingId)
        {
            var scriptContent = mainScript.GetJsInnerHtml();
            var match = Regex.Match(scriptContent, @"ga\('create', '(.+)', 'auto'\)");

            Assert.IsNotNull(mainScript);
            Assert.IsTrue(match.Success);
            Assert.AreEqual(match.Groups[1].Value, trackingId);
            return scriptContent;
        }
    }
}
