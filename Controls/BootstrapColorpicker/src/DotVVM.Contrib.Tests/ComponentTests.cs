using System;
using DotVVM.Contrib.Tests.Core;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var textBox = browser.ElementAt("input[type=text]", 0);
                var picker = browser.ElementAt("input[type=text]", 1);
                var button = browser.ElementAt("input[type=button]", 0);

                // check initial value
                AssertUI.TextEquals(picker, "#AAAAAA");

                // open picker
                picker.Click();

                // make sure the popup is open
                var popup = browser.Single(".colorpicker");
                AssertUI.IsDisplayed(popup);

                // click somewhere in the picker
                var actions = new Actions(browser.Driver);
                actions.MoveToElement(popup.WebElement, 50, 50);
                actions.Click();
                actions.Perform();

                // make sure the color has changed
                AssertUI.TextNotEquals(picker, "#AAAAAA");
                AssertUI.TextEquals(picker, textBox.GetValue());

                // click outside to hide the picker
                textBox.Click();

                // make sure the popup is not present in the page
                browser.FindElements(".colorpicker").ThrowIfDifferentCountThan(0);

                // click the button
                button.Click();

                // make sure the value has changed
                AssertUI.TextEquals(picker, "#BBBBBB");
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
