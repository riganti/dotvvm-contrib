using DotVVM.Contrib.MultilevelMenu.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.MultilevelMenu.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
