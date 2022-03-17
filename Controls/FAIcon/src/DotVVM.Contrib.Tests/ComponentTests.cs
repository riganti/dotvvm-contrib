using System;
using DotVVM.Contrib.Tests.Core;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void HardcodedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var element = browser.Single("hardcoded",SelectBy.Id);
                AssertUI.TagName(element,"i");
                AssertUI.HasClass(element,"fab");
                AssertUI.HasClass(element, "fa-github");
            });
        }
        
        [Fact]
        public void BindingTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var element = browser.Single("binding",SelectBy.Id);

                AssertUI.TagName(element, "i");
                AssertUI.HasClass(element, "fab");
                AssertUI.HasClass(element, "fa-adn");
                browser.Single("change_binding",SelectBy.Id).Click();

                AssertUI.HasClass(element, "fas");
                AssertUI.HasClass(element, "fa-adjust");
            });
        }

        [Fact]
        public void ComputedTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var element = browser.Single("ternary",SelectBy.Id);

                AssertUI.TagName(element, "i");
                AssertUI.HasClass(element, "fas");
                AssertUI.HasClass(element, "fa-bug");
                browser.Single("change_ternary",SelectBy.Id).Click();

                AssertUI.HasClass(element, "fas");
                AssertUI.HasClass(element, "fa-building");
            });
        }
        
        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
