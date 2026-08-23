using DotVVM.Contrib.CkEditor5Minimal.Tests.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.CkEditor5Minimal.Tests
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
