using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class Select2Tests : SeleniumTestBase
    {

        [TestMethod]
        public void Select2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/");

                RunTestSubSection("List of strings", b =>
                {
                    PerformSelect2Test(b, b.ElementAt("fieldset", 0));
                });
                RunTestSubSection("List of objects", b =>
                {
                    PerformSelect2Test(b, b.ElementAt("fieldset", 1));
                });
            });
        }

        private static void PerformSelect2Test(BrowserWrapper browser, ElementWrapper fieldset)
        {
            var select2 = fieldset.Single(".select2");
            var input = select2.First("input");
            var result = fieldset.Single(".result");
            var addItemsButton = fieldset.ElementAt("button", 0);
            var changeSelectionButton = fieldset.ElementAt("button", 1);
            var submitButton = fieldset.ElementAt("button", 2);

            // verify the selection at the beginning
            select2.FindElements("li").ThrowIfDifferentCountThan(2);
            result.CheckIfTextEquals("Prague");

            // append tag
            input.SendKeys("Ne");
            input.SendKeys(Keys.Return);
            select2.FindElements("li").ThrowIfDifferentCountThan(3);

            // submit and check the selection on the server
            submitButton.Click().Wait();

            // verify the new tag appeared in the result
            result.CheckIfTextEquals("Prague,New York");

            // check the items offered in the list
            input.Click().Wait();
            browser.FindElements(".select2-container--open li.select2-results__option").ThrowIfDifferentCountThan(4);
            fieldset.Click();

            // add new items to the collection
            addItemsButton.Click().Wait();

            // check the items offered in the list were updated
            input.Click().Wait();
            browser.FindElements(".select2-container--open li.select2-results__option").ThrowIfDifferentCountThan(5);

            // select last item from the list
            browser.Last(".select2-container--open li.select2-results__option").Click();

            // submit and check the selection on the server
            submitButton.Click().Wait();
            result.CheckIfTextEquals("Prague,New York,Berlin");

            // replace the selection on server
            changeSelectionButton.Click().Wait();
            submitButton.Click().Wait();
            result.CheckIfTextEquals("New York,Paris");
        }
    }
}
