using System;
using DotVVM.Contrib.Tests.Core;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class SliderTests : AppSeleniumTest
    {
        public SliderTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Slider_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Slider_Sample1");
                browser.Wait(2000);

                var slider1 = browser.ElementAt(".noUi-base", 0);
                var slider2 = browser.ElementAt(".noUi-base", 1);
                var valueInput = browser.ElementAt("input[type=text]", 0);
                var enabledCheckbox = browser.ElementAt("input[type=checkbox]", 0);
                var button = browser.Single("input[type=button]");
                var handle = browser.ElementAt(".noUi-handle", 0);

                // switch the switch on
                new Actions(browser.Driver).MoveToElement(slider1.WebElement).MoveByOffset(50, 5).Click().Perform();
                browser.Wait(2000);
                AssertUI.Value(valueInput, "70");

                // switch the switch off
                new Actions(browser.Driver).MoveToElement(slider2.WebElement).MoveByOffset(5, -30).Click().Perform();
                browser.Wait(2000);
                AssertUI.Value(valueInput, "60");

                // disable the switch
                enabledCheckbox.Click();

                // switch the switch on (it shouldn't work now)
                new Actions(browser.Driver).MoveToElement(slider1.WebElement).MoveByOffset(50, 5).Click().Perform();
                browser.Wait(2000);
                AssertUI.Value(valueInput, "60");

                // switch from the server
                var oldX = handle.WebElement.Location.X;
                button.Click().Wait(2000);
                Assert.True(handle.WebElement.Location.X < oldX);
            });
        }
    }
}