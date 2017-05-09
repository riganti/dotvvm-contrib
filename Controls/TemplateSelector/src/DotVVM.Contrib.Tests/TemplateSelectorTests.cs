using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class TemplateSelectorTests : SeleniumTestBase
    {
        [TestMethod]
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
                browser.ElementAt("table tr", 0).First("td").First("fieldset").CheckIfHasClass("fs-h");
                browser.ElementAt("table tr", 1).First("td").First("fieldset").CheckIfHasClass("fs-i");
                browser.ElementAt("table tr", 2).First("td").First("fieldset").CheckIfHasClass("fs-p");
                browser.ElementAt(".fs-h input", 0).CheckIfValue("aa");
                browser.ElementAt(".fs-p input", 0).CheckIfValue("bb");
                browser.ElementAt(".fs-i input", 0).CheckIfValue("cc");
                browser.ElementAt(".fs-i input", 1).CheckIfValue("dd");
                browser.ElementAt(".fs-i input", 2).CheckIfValue("ee");

                // move middle item up
                browser.ElementAt("table tr", 1).ElementAt("a", 0).Click().Wait();

                // check the order and values of elements
                browser.ElementAt("table tr", 0).First("td").First("fieldset").CheckIfHasClass("fs-i");
                browser.ElementAt("table tr", 1).First("td").First("fieldset").CheckIfHasClass("fs-h");
                browser.ElementAt("table tr", 2).First("td").First("fieldset").CheckIfHasClass("fs-p");
                browser.ElementAt(".fs-h input", 0).CheckIfValue("aa");
                browser.ElementAt(".fs-p input", 0).CheckIfValue("bb");
                browser.ElementAt(".fs-i input", 0).CheckIfValue("cc");
                browser.ElementAt(".fs-i input", 1).CheckIfValue("dd");
                browser.ElementAt(".fs-i input", 2).CheckIfValue("ee");

                // delete the middle item
                browser.ElementAt("table tr", 1).ElementAt("a", 2).Click().Wait();

                // check the order and values of elements
                browser.ElementAt("table tr", 0).First("td").First("fieldset").CheckIfHasClass("fs-i");
                browser.ElementAt("table tr", 1).First("td").First("fieldset").CheckIfHasClass("fs-p");
                browser.ElementAt(".fs-p input", 0).CheckIfValue("bb");
                browser.ElementAt(".fs-i input", 0).CheckIfValue("cc");
                browser.ElementAt(".fs-i input", 1).CheckIfValue("dd");
                browser.ElementAt(".fs-i input", 2).CheckIfValue("ee");
            });
        }
    }
}
