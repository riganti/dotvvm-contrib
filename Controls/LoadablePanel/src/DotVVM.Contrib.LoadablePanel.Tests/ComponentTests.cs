using DotVVM.Contrib.LoadablePanel.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.LoadablePanel.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentBasicTest1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var loadablePanel = browser.Single("#loadable-panel1");
                var content = loadablePanel.Single("div", By.TagName);

                AssertUI.IsNotDisplayed(content);

                browser.Wait(3000);

                AssertUI.IsDisplayed(content);
            });
        }
        [Fact]
        public void ComponentBasicTest2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var loadablePanel = browser.Single("#loadable-panel2");
                var content = loadablePanel.FindElements("div", By.TagName);

                var spinner = content[0].Single("update-progress", By.ClassName);
                AssertUI.IsDisplayed(spinner);
                Assert.DoesNotContain("Loaded content in 3000 ms", content[9].GetText());

                browser.Wait(3000);

                AssertUI.IsNotDisplayed(spinner);
                Assert.Contains("Loaded content in 3000 ms", content[9].GetText());
            });
        }
        [Fact]
        public void ComponentPostbackTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample3");

                var panels = browser.FindElements("panel", By.ClassName);
                browser.Wait(6000);
                foreach (var pan in panels)
                {
                    AssertUI.IsDisplayed(pan.Single("box", By.ClassName));
                }
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
