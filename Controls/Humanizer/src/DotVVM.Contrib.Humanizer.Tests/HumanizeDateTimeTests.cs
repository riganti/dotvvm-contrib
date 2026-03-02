using System;
using DotVVM.Contrib.Humanizer.Tests.Core;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Humanizer.Tests
{
    public class HumanizeDateTimeTests : AppSeleniumTest
    {
        public HumanizeDateTimeTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void HumanizeDateTime_HardCoded_RendersHumanizedText()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // The first paragraph contains the hard-coded value - server-side rendering should show "X years ago"
                var firstPara = browser.ElementAt("p", 0);
                AssertUI.InnerText(firstPara, t => t.Contains("ago"), "Expected humanized text to contain 'ago'");
            });
        }

        [Fact]
        public void HumanizeDateTime_BoundValue_RendersHumanizedText()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // The bound DateTime (2 hours ago) - client-side dayjs should render "X hours ago"
                var boundSpan = browser.ElementAt("span[data-bind*='dotvvm-contrib-HumanizeDateTime']", 2);
                AssertUI.InnerText(boundSpan, t => t.Contains("ago"), "Expected bound DateTime to show 'ago'");
            });
        }

        [Fact]
        public void HumanizeDateTime_FutureValue_RendersHumanizedText()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // The future DateTime (3 days from now) - dayjs renders "in X days"
                var futureSpan = browser.ElementAt("span[data-bind*='dotvvm-contrib-HumanizeDateTime']", 3);
                AssertUI.InnerText(futureSpan, t => t.Contains("day") || t.Contains("in"), "Expected future DateTime to show relative future text");
            });
        }

        [Fact]
        public void HumanizeDateTime_NullValue_RendersEmpty()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // The null DateTime should render empty text
                var nullSpan = browser.ElementAt("span[data-bind*='dotvvm-contrib-HumanizeDateTime']", 4);
                AssertUI.TextEmpty(nullSpan);
            });
        }

        [Fact]
        public void HumanizeDateTime_SetToLastWeek_ShowsDaysAgo()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // Click "Set to Last Week" - should update the bound DateTime span
                var lastWeekButton = browser.ElementAt("input[type=button]", 1);
                lastWeekButton.Click();
                browser.Wait(2000);

                // After clicking, the bound DateTime span should show "7 days ago" or similar
                var boundSpan = browser.ElementAt("span[data-bind*='dotvvm-contrib-HumanizeDateTime']", 2);
                AssertUI.InnerText(boundSpan, t => t.Contains("days ago") || t.Contains("week"), "Expected text to show 'X days ago' or 'a week ago'");
            });
        }
    }
}
