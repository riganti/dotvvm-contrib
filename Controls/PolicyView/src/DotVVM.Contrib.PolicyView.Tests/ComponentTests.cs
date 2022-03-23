using DotVVM.Contrib.PolicyView.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.PolicyView.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                var body = browser.Single("body", By.TagName);

                var signBtn = browser.Single("#sign-in-button");

                AssertUI.NotContainsElement(body, "#in-role-requirements-met");
                AssertUI.NotContainsElement(body, "#in-role-requirements-not-met");

                AssertUI.NotContainsElement(body, "#autenticated-req-met");
                AssertUI.ContainsElement(body, "#autenticated-req-not-met");

                AssertUI.NotContainsElement(body, "#combinated-req-met");
                AssertUI.NotContainsElement(body, "#combinated-req-not-met");

                signBtn.Click();
                body = browser.Single("body", By.TagName);

                AssertUI.NotContainsElement(body, "#in-role-req-met");
                AssertUI.ContainsElement(body, "#in-role-req-not-met");

                AssertUI.ContainsElement(body, "#autenticated-req-met");
                AssertUI.NotContainsElement(body, "#autenticated-req-not-met");

                AssertUI.ContainsElement(body, "#combinated-req-met");
                AssertUI.NotContainsElement(body, "#combinated-req-not-met");

                browser.Single("#sign-role-button").Click();
                body = browser.Single("body", By.TagName);

                AssertUI.ContainsElement(body, "#in-role-req-met");
                AssertUI.NotContainsElement(body, "#in-role-req-not-met");

                AssertUI.ContainsElement(body, "#autenticated-req-met");
                AssertUI.NotContainsElement(body, "#autenticated-req-not-met");

                AssertUI.ContainsElement(body, "#combinated-req-met");
                AssertUI.NotContainsElement(body, "#combinated-req-not-met");

                browser.Single("#sign-out-button").Click();
                body = browser.Single("body", By.TagName);

                AssertUI.NotContainsElement(body, "#in-role-requirements-met");
                AssertUI.NotContainsElement(body, "#in-role-requirements-not-met");

                AssertUI.NotContainsElement(body, "#autenticated-req-met");
                AssertUI.ContainsElement(body, "#autenticated-req-not-met");

                AssertUI.NotContainsElement(body, "#combinated-req-met");
                AssertUI.NotContainsElement(body, "#combinated-req-not-met");

            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
