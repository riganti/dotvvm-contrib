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
                AssertUI.ContainsElement(element, "path[d=\"M9 9L9 4.5M9 9L4.5 9M9 9L3.75 3.75M9 15L9 19.5M9 15L4.5 15M9 15L3.75 20.25M15 9H19.5M15 9V4.5M15 9L20.25 3.75M15 15H19.5M15 15L15 19.5M15 15L20.25 20.25\"]");
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
                AssertUI.ContainsElement(element, "path[d=\"M2 3C1.44772 3 1 3.44772 1 4V5C1 5.55228 1.44772 6 2 6H18C18.5523 6 19 5.55228 19 5V4C19 3.44772 18.5523 3 18 3H2Z\"]");
                AssertUI.ContainsElement(element, "path[d=\"M2 7.5H18L17.1885 15.2094C17.0813 16.2273 16.223 17 15.1995 17H4.80052C3.77701 17 2.91866 16.2273 2.81151 15.2094L2 7.5ZM7 11C7 10.4477 7.44772 10 8 10H12C12.5523 10 13 10.4477 13 11C13 11.5523 12.5523 12 12 12H8C7.44772 12 7 11.5523 7 11Z\"]");
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
