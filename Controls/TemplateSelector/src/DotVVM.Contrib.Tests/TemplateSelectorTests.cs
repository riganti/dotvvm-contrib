using System;
using DotVVM.Contrib.Tests.Core;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class TemplateSelectorTests : AppSeleniumTest
    {
        public TemplateSelectorTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TemplateSelector_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                // add an item of each type
                browser.ElementAt(".add-row input", 0).Click().Wait();
                browser.ElementAt(".add-row input", 1).Click().Wait();
                browser.ElementAt(".add-row input", 2).Click().Wait();

                // fill unique values into all fields
                browser.ElementAt(".fs-h input", 0).SendKeys("aa");
                browser.ElementAt(".fs-p input", 0).SendKeys("bb");
                browser.ElementAt(".fs-i input", 0).SendKeys("cc");
                browser.ElementAt(".fs-i input", 1).SendKeys("dd");
                browser.ElementAt(".fs-i input", 2).SendKeys("ee");

                // move middle item down
                browser.ElementAt("table tr", 1).ElementAt("a", 1).Click().Wait();

                // check the order and values of elements
                AssertUI.HasClass(browser.ElementAt("table tr", 0).First("td").First("fieldset"),"fs-h");
                AssertUI.HasClass(browser.ElementAt("table tr", 1).First("td").First("fieldset"),"fs-i");
                AssertUI.HasClass(browser.ElementAt("table tr", 2).First("td").First("fieldset"),"fs-p");
                AssertUI.Value(browser.ElementAt(".fs-h input", 0),"aa");
                AssertUI.Value(browser.ElementAt(".fs-p input", 0),"bb");
                AssertUI.Value(browser.ElementAt(".fs-i input", 0),"cc");
                AssertUI.Value(browser.ElementAt(".fs-i input", 1),"dd");
                AssertUI.Value(browser.ElementAt(".fs-i input", 2),"ee");

                // move middle item up
                browser.ElementAt("table tr", 1).ElementAt("a", 0).Click().Wait();

                // check the order and values of elements
                AssertUI.HasClass(browser.ElementAt("table tr", 0).First("td").First("fieldset"),"fs-i");
                AssertUI.HasClass(browser.ElementAt("table tr", 1).First("td").First("fieldset"),"fs-h");
                AssertUI.HasClass(browser.ElementAt("table tr", 2).First("td").First("fieldset"),"fs-p");
                AssertUI.Value(browser.ElementAt(".fs-h input", 0),"aa");
                AssertUI.Value(browser.ElementAt(".fs-p input", 0),"bb");
                AssertUI.Value(browser.ElementAt(".fs-i input", 0),"cc");
                AssertUI.Value(browser.ElementAt(".fs-i input", 1),"dd");
                AssertUI.Value(browser.ElementAt(".fs-i input", 2),"ee");

                // delete the middle item
                browser.ElementAt("table tr", 1).ElementAt("a", 2).Click().Wait();

                // check the order and values of elements
                AssertUI.HasClass(browser.ElementAt("table tr", 0).First("td").First("fieldset"),"fs-i");
                AssertUI.HasClass(browser.ElementAt("table tr", 1).First("td").First("fieldset"),"fs-p");
                AssertUI.Value(browser.ElementAt(".fs-p input", 0),"bb");
                AssertUI.Value(browser.ElementAt(".fs-i input", 0),"cc");
                AssertUI.Value(browser.ElementAt(".fs-i input", 1),"dd");
                AssertUI.Value(browser.ElementAt(".fs-i input", 2),"ee");
            });
        }

    }
}
