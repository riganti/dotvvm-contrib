using DotVVM.Contrib.BootstrapDatepicker.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.BootstrapDatepicker.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void Sample1Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                var dp1 = browser.Single("#dp1");
                var dp2 = browser.Single("#dp2");
                var lit1 = browser.Single("#lit1");
                var lit2 = browser.Single("#lit2");
                var btn1 = browser.ElementAt(".btn", 0);
                var btn2 = browser.ElementAt(".btn", 1);
                var btn3 = browser.ElementAt(".btn", 2);

                AssertUI.Value(dp1, string.Empty);
                AssertUI.TextEquals(lit1, string.Empty);

                dp1.Clear();
                dp1.SendKeys("13.1.2019");
                dp1.SendEnterKey();
                AssertUI.Value(dp1, "13.01.2019");
                AssertUI.TextEquals(lit1, "13.01.2019 0:00:00");

                btn1.Click();
                AssertUI.Value(dp1, "20.01.2000");
                AssertUI.TextEquals(lit1, "20.01.2000 0:00:00");

                btn2.Click();
                AssertUI.Value(dp1, string.Empty);
                AssertUI.TextEquals(lit1, string.Empty);

                btn1.Click();
                AssertUI.Value(dp1, "20.01.2000");
                AssertUI.TextEquals(lit1, "20.01.2000 0:00:00");

                dp2.Clear();
                dp2.SendKeys("13.1.2019");
                dp2.SendEnterKey();
                AssertUI.TextEquals(lit2, "13.1.2019");

                btn3.Click();
                AssertUI.Value(dp2, "30.1.2000");
                AssertUI.TextEquals(lit2, "30.1.2000");
            });
        }

        [Fact]
        public void Sample2Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");
                browser.Wait(2000);

                var dp1 = browser.Single("#dp1");
                var dp2 = browser.Single("#dp2");
                var dp3 = browser.Single("#dp3");

                dp1.Clear();
                dp1.SendKeys("13.1.2019");
                dp1.SendEnterKey();

                AssertUI.Value(dp1, "13.01.2019");
                AssertUI.Value(dp2, "01/13/2019");
                AssertUI.Value(dp3, "13/01/2019");

                var btn = browser.Single(".btn");
                btn.Click();

                AssertUI.Value(dp1, "20.01.2000");
                AssertUI.Value(dp2, "01/20/2000");
                AssertUI.Value(dp3, "20/01/2000");
            });
        }

        [Fact]
        public void Sample3Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                var dp1 = browser.Single("#dp1");
                var dp2 = browser.Single("#dp2");
                var lit2 = browser.Single("#lit2");

                dp2.SetFocus();
                browser.Wait(500);
                dp2.SendKeys(Keys.Backspace);
                dp2.SendKeys("8");
                dp1.SetFocus();
                browser.Wait(500);
                Assert.Equal(dp2.GetValue(), lit2.GetText());
            });
        }

        [Fact]
        public void Sample4Test()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample4");
                browser.Wait(2000);

                var dp1 = browser.Single("#dp1");
                var changed = browser.Single("#changed");
                dp1.Clear();
                dp1.SendKeys("13.1.2019");
                dp1.SendEnterKey();
                browser.Wait(500);

                AssertUI.Value(dp1, "13.01.2019");

                AssertUI.InnerTextEquals(changed,"Changed");
                
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
