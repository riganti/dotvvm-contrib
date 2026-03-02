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
                var hardcodedPara = browser.Single("[data-ui='hardcoded-paragraph']");
                AssertUI.InnerText(hardcodedPara, t => t.Contains("ago"), "Expected humanized text to contain 'ago'");
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
                var boundSpan = browser.Single("[data-ui='bound-datetime']");
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
                var futureSpan = browser.Single("[data-ui='future-datetime']");
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
                var nullSpan = browser.Single("[data-ui='null-datetime']");
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
                var lastWeekButton = browser.Single("[data-ui='btn-set-lastweek']");
                lastWeekButton.Click();
                browser.Wait(2000);

                // After clicking, the bound DateTime span should show "7 days ago" or similar
                var boundSpan = browser.Single("[data-ui='bound-datetime']");
                AssertUI.InnerText(boundSpan, t => t.Contains("days ago") || t.Contains("week"), "Expected text to show 'X days ago' or 'a week ago'");
            });
        }

        [Fact]
        public void HumanizeDateTime_CzechLocalization_RendersCzechText()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1?culture=cs-CZ");
                browser.Wait(2000);

                // The bound DateTime should show Czech text (e.g., "před 2 hodinami")
                var boundSpan = browser.Single("[data-ui='bound-datetime']");
                AssertUI.InnerText(boundSpan, t => t.Contains("před") || t.Contains("hodin"), "Expected Czech localized text to contain 'před' or 'hodin'");
            });
        }

        [Fact]
        public void HumanizeDateTime_AutoUpdate_UpdatesAfterOneMinute()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                // Click "Set to Now" to set the DateTime to current time
                var setToNowButton = browser.Single("[data-ui='btn-set-now']");
                setToNowButton.Click();
                browser.Wait(2000);

                // Get the AutoUpdate span (the bound value with AutoUpdate)
                var autoUpdateSpan = browser.Single("[data-ui='bound-autoupdate']");
                
                // Initially, should show "a few seconds ago" or similar
                var initialText = autoUpdateSpan.GetInnerText();
                AssertUI.InnerText(autoUpdateSpan, t => t.Contains("second") || t.Contains("now") || t.Contains("just"), 
                    "Expected initial text to show very recent time");

                // Wait for 65 seconds (just over a minute to ensure update happens)
                browser.Wait(65000);

                // After one minute, the text should have updated to show "a minute ago" or similar
                var updatedText = autoUpdateSpan.GetInnerText();
                AssertUI.InnerText(autoUpdateSpan, t => t.Contains("minute"), 
                    "Expected text to update to show 'a minute ago' after waiting");
                
                // Verify the text actually changed
                Assert.NotEqual(initialText, updatedText);
            });
        }
    }
}
