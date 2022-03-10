using DotVVM.Contrib.Select2.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Select2.Tests
{
    public class Select2Tests : AppSeleniumTest
    {
        [Fact]
        public void ComponentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
            });
        }

        public Select2Tests(ITestOutputHelper output) : base(output)
        {
        }


        [Fact]
        public void Select2_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                PerformSelect2Test(browser, browser.ElementAt("fieldset", 0));
                PerformSelect2Test(browser, browser.ElementAt("fieldset", 1));
            });
        }

        private static void PerformSelect2Test(IBrowserWrapper browser, IElementWrapper fieldset)
        {
            var select2 = fieldset.Single(".select2");
            var input = select2.First("input");
            var result = fieldset.Single(".result");
            var addItemsButton = fieldset.ElementAt("button", 0);
            var changeSelectionButton = fieldset.ElementAt("button", 1);
            var submitButton = fieldset.ElementAt("button", 2);

            // verify the selection at the beginning
            select2.FindElements("li").ThrowIfDifferentCountThan(2);
            AssertUI.TextEquals(result, "Prague", true);

            // append tag
            input.SendKeys("Ne");
            input.SendKeys(Keys.Return);
            select2.FindElements("li").ThrowIfDifferentCountThan(3);

            // submit and check the selection on the server
            submitButton.Click().Wait();

            // verify the new tag appeared in the result
            AssertUI.TextEquals(result, "Prague,New York", true);

            // check the items offered in the list
            input.Click().Wait();
            browser.FindElements(".select2-container--open li.select2-results__option").ThrowIfDifferentCountThan(4);
            fieldset.Click();

            // add new items to the collection
            addItemsButton.Click().Wait();

            // check the items offered in the list were updated
            input.Click().Wait();
            browser.FindElements(".select2-container--open li.select2-results__option").ThrowIfDifferentCountThan(5);

            // select last item from the list
            browser.Last(".select2-container--open li.select2-results__option").Click();

            // submit and check the selection on the server
            submitButton.Click().Wait();
            AssertUI.TextEquals(result, "Prague,New York,Berlin", true);

            // replace the selection on server
            changeSelectionButton.Click().Wait();
            submitButton.Click().Wait();
            AssertUI.TextEquals(result, "New York,Paris", true);
        }

        [Fact]
        public void Select2_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var select2 = browser.Single(".select2");
                var input = select2.First("input");
                var result = browser.Single(".number-of-requests");

                input.SendKeys("c");
                input.SendKeys(Keys.Return);

                browser.Wait();

                browser.FindElements(".select2-selection__choice").ThrowIfDifferentCountThan(1);
                Assert.Equal("1", result.GetInnerText());
            });
        }

        [Fact]
        public void Select2_Sample3()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample3");

                IElementWrapper OpenSelectInput()
                {
                    browser.Single(".select2").Click();
                    return browser.First(".select2-search input");
                }
                var requestCount = browser.Single(".number-of-requests");
                var selectedValue = browser.Single(".selected-value");
                var displayValue = browser.Single(".select2-selection__rendered");

                var input = OpenSelectInput();
                input.SendKeys("c");
                input.SendKeys(Keys.Return);

                browser.Wait();

                Assert.Equal("c", displayValue.GetInnerText());
                Assert.Equal("2", selectedValue.GetInnerText());
                Assert.Equal("1", requestCount.GetInnerText());

                input = OpenSelectInput();
                input.SendKeys("a");
                input.SendKeys(Keys.Return);

                browser.Wait();

                Assert.Equal("a", displayValue.GetInnerText());
                Assert.Equal("0", selectedValue.GetInnerText());
                Assert.Equal("2", requestCount.GetInnerText());

                browser.Single("[data-ui=change-in-postback]").Click();
                Assert.Equal("c", displayValue.GetInnerText());
                Assert.Equal("2", selectedValue.GetInnerText());
                Assert.Equal("2", requestCount.GetInnerText());

                browser.Single("[data-ui=change-in-static-command]").Click();
                Assert.Equal("b", displayValue.GetInnerText());
                Assert.Equal("1", selectedValue.GetInnerText());
                Assert.Equal("2", requestCount.GetInnerText());
            });
        }
    }
}
