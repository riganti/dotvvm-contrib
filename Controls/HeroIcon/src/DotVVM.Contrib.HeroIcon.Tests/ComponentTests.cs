using DotVVM.Contrib.HeroIcon.Tests.Core;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.HeroIcon.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void OutlineTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var element = browser.Single("outline", SelectBy.Id);
                AssertUI.TagName(element, "svg");
                AssertUI.Attribute(element, "fill", "none");
                AssertUI.ContainsElement(element, "path[d=\"M9 9V4.5M9 9H4.5M9 9L3.75 3.75M9 15v4.5M9 15H4.5M9 15l-5.25 5.25M15 9h4.5M15 9V4.5M15 9l5.25-5.25M15 15h4.5M15 15v4.5m0-4.5l5.25 5.25\"]");
            });
        }

        [Fact]
        public void MiniTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var element = browser.Single("mini", SelectBy.Id);
                AssertUI.TagName(element, "svg");
                AssertUI.Attribute(element, "fill", "currentColor");
                AssertUI.ContainsElement(element, "path[d=\"M2 3a1 1 0 00-1 1v1a1 1 0 001 1h16a1 1 0 001-1V4a1 1 0 00-1-1H2z\"]");
                AssertUI.ContainsElement(element, "path[d=\"M2 7.5h16l-.811 7.71a2 2 0 01-1.99 1.79H4.802a2 2 0 01-1.99-1.79L2 7.5zM7 11a1 1 0 011-1h4a1 1 0 110 2H8a1 1 0 01-1-1z\"]");
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
