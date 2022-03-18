using DotVVM.Contrib.QrCode.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Api;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.QrCode.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void BasicTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var qrCodes = browser.FindElements("canvas", By.TagName);

                qrCodes.ThrowIfDifferentCountThan(3);
                AssertUI.All(qrCodes).IsDisplayed();
                
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
