using DotVVM.Contrib.MultilevelMenu.Tests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using System;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.MultilevelMenu.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void BasicTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var menu = browser.Single("#multilevel-menu");
                AssertUI.IsNotDisplayed(menu.Single("ul", By.TagName));
                var actions = new Actions(browser.Driver);
                actions.MoveToElement(menu.FindElements("li",By.TagName)[1].WebElement);
                actions.Perform();

                AssertUI.IsDisplayed(menu.Single("ul", By.TagName));

                actions.Click();
                actions.Perform();

                string actualUrl = browser.CurrentUrl;
                Assert.Equal("http://localhost:46582/Sample2", actualUrl);
            });
        }
        [Fact]
        public void BasicTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var menu = browser.Single("#multilevel-menu");
                AssertUI.IsNotDisplayed(menu.Single("ul", By.TagName));
                var actions = new Actions(browser.Driver);
                actions.MoveToElement(menu.FindElements("li", By.TagName)[1].WebElement);
                actions.Perform();
                actions.MoveToElement(menu.FindElements("li", By.TagName)[1].First("li", By.TagName).WebElement);
                actions.Perform();
                AssertUI.IsDisplayed(menu.Single("ul", By.TagName));

                actions.Click();
                actions.Perform();

                string actualUrl = browser.CurrentUrl;
                Assert.Equal("http://localhost:46582/Sample2_Child1", actualUrl);
            });
        }
        [Fact]
        public void IconTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample3");

                var menu = browser.Single("#multilevel-menu");

                var items = menu.FindElements("li", By.TagName);
                AssertUI.ContainsElement(items[0], "fa-address-book", By.ClassName);
                AssertUI.ContainsElement(items[1], "fa-arrow-alt-circle-up", By.ClassName);
                AssertUI.ContainsElement(items[2], "fa-balance-scale", By.ClassName);
                AssertUI.ContainsElement(items[3], "fa-address-card", By.ClassName);
                AssertUI.ContainsElement(items[4], "fa-biking", By.ClassName);
                AssertUI.ContainsElement(items[5], "fa-book-open", By.ClassName);
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
