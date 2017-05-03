using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Interactions;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class SwitchTests : SeleniumTestBase
    {
        [TestMethod]
        public void Switch_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Switch_Sample1");
                browser.Wait(2000);

                var slider1 = browser.ElementAt(".noUi-base", 0);
                var slider2 = browser.ElementAt(".noUi-base", 1);
                var valueCheckbox = browser.ElementAt("input[type=checkbox]", 0);
                var enabledCheckbox = browser.ElementAt("input[type=checkbox]", 1);
                var button = browser.Single("input[type=button]");
                var handle = browser.ElementAt(".noUi-handle", 0);

                // switch the switch on
                new Actions(browser.Browser).MoveToElement(slider1.WebElement).MoveByOffset(30, 5).Click().Perform();
                browser.Wait(2000);
                valueCheckbox.CheckIfIsChecked();

                // switch the switch off
                new Actions(browser.Browser).MoveToElement(slider2.WebElement).MoveByOffset(5, 10).Click().Perform();
                browser.Wait(2000);
                valueCheckbox.CheckIfIsNotChecked();

                // disable the switch
                enabledCheckbox.Click();

                // switch the switch on (it shouldn't work now)
                new Actions(browser.Browser).MoveToElement(slider1.WebElement).MoveByOffset(30, 5).Click().Perform();
                browser.Wait(2000);
                valueCheckbox.CheckIfIsNotChecked();
                
                // switch from the server
                button.Click().Wait(2000);
                Assert.IsTrue(handle.WebElement.Location.X > 50);
            });
        }
    }
}
