using DotVVM.Contrib.CookieBar.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.CookieBar.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentTestBasic()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var cookierBar = browser.Single("dotvvm-contrib-cookie-bar", By.ClassName);
                var resetbtn = browser.Single("#reset");
                var cookieBarPopUp = cookierBar.Single("dotvvm-contrib-cookie-bar__pop-up", By.ClassName);
                var acceptButton = cookieBarPopUp.Single("button--primary", By.ClassName);
                var editButton = cookieBarPopUp.Single("button--secondary", By.ClassName);

                AssertUI.HasClass(cookieBarPopUp, "dotvvm-contrib-cookie-bar__pop-up--open");
                acceptButton.Click();
                AssertUI.HasNotClass(cookieBarPopUp, "dotvvm-contrib-cookie-bar__pop-up--open");
            });
        }

        [Fact]
        public void ComponentTestBasic2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var cookierBar = browser.Single("dotvvm-contrib-cookie-bar", By.ClassName);
                var resetbtn = browser.Single("#reset");
                var cookieBarPopUp = cookierBar.Single("dotvvm-contrib-cookie-bar__pop-up", By.ClassName);
                var acceptButton = cookieBarPopUp.Single("button--primary", By.ClassName);
                var editButton = cookieBarPopUp.Single("button--secondary", By.ClassName);
                var options = cookierBar.Single("dotvvm-contrib-cookie-bar__options", By.ClassName);

                var dialog = browser.Single("dotvvm-contrib-cookie-bar__dialog", By.ClassName);

                AssertUI.HasClass(cookieBarPopUp, "dotvvm-contrib-cookie-bar__pop-up--open");
                editButton.Click();
                AssertUI.HasNotClass(cookieBarPopUp, "dotvvm-contrib-cookie-bar__pop-up--open");
                AssertUI.IsDisplayed(dialog);

                var details = options.FindElements("details", By.TagName);
                for (int i = 1; i < details.Count; i++)
                {
                    AssertUI.HasClass(details[i].Single("label", By.TagName), "toggle-button--on");
                }

                var onlyNecessryButton = cookierBar.Single("dotvvm-contrib-cookie-bar__footer", By.ClassName).Single("a", By.TagName);
                var saveAndCloseButton = cookierBar.Single("dotvvm-contrib-cookie-bar__footer", By.ClassName).Single("button", By.TagName);
                onlyNecessryButton.Click();
                AssertUI.IsDisplayed(dialog);

                for (int i = 1; i < details.Count; i++)
                {
                    AssertUI.HasNotClass(details[i].Single("label", By.TagName), "toggle-button--on");
                }

                saveAndCloseButton.Click();

                AssertUI.IsNotDisplayed(dialog);
                AssertUI.HasNotClass(cookieBarPopUp, "dotvvm-contrib-cookie-bar__pop-up--open");


            });
        }



        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
