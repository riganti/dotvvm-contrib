using DotVVM.Contrib.Tests.Core;
using OpenQA.Selenium;
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
                browser.NavigateToUrl("/Sample2");
                browser.Wait(2000);

                var dp1 = browser.Single("dp1", By.Id);
                Assert.Equal(string.Empty, dp1.GetValue());

                dp1.Clear();
                dp1.SendKeys("13.1.2019");
                dp1.SendEnterKey();

                Assert.Equal("13.01.2019", dp1.GetValue());

                var dp2 = browser.Single("dp2", By.Id);
                Assert.Equal("01/13/2019", dp2.GetValue());

                var dp3 = browser.Single("dp3", By.Id);
                Assert.Equal("13/01/2019", dp3.GetValue());

            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
