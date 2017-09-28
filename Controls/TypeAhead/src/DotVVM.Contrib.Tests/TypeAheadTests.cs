using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class TypeAheadTests : SeleniumTestBase
    {
        [TestMethod]
        public void TypeAhead_Sample1_ListOfStrings()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(500);

                var input = browser.ElementAt("#section1 input[type=text]", 1);
                var result1 = browser.ElementAt("#section1 .result", 0);

                input.Clear();
                input.SendKeys("Cze");
                browser.SendEnterKey();
                input.CheckIfValue("Czech Republic");
                result1.CheckIfInnerTextEquals("Czech Republic");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("");
                result1.CheckIfInnerTextEquals("");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("Country 5");
                result1.CheckIfInnerTextEquals("Country 5");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section1 .tt-selectable").Click();
                input.CheckIfValue("Germany");
                result1.CheckIfInnerTextEquals("Germany");

                browser.ElementAt("#buttons input", 1).Click();

                input.CheckIfValue("Country 5");
                result1.CheckIfInnerTextEquals("Country 5");
            });
        }

        [TestMethod]
        public void TypeAhead_Sample1_ListOfObjects_DisplayMember()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(500);

                var input = browser.ElementAt("#section2 input[type=text]", 1);
                var result1 = browser.ElementAt("#section2 .result", 0);
                var result2 = browser.ElementAt("#section2 .result", 1);

                input.Clear();
                input.SendKeys("Cze");
                browser.SendEnterKey();
                input.CheckIfValue("Czech Republic");
                result1.CheckIfInnerTextEquals("1");
                result2.CheckIfInnerTextEquals("Czech Republic");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("");
                result1.CheckIfInnerTextEquals("");
                result2.CheckIfInnerTextEquals("");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("Country 5");
                result1.CheckIfInnerTextEquals("6");
                result2.CheckIfInnerTextEquals("Country 5");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section2 .tt-selectable").Click();
                input.CheckIfValue("Germany");
                result1.CheckIfInnerTextEquals("2");
                result2.CheckIfInnerTextEquals("Germany");
            });
        }


        [TestMethod]
        public void TypeAhead_Sample1_ListOfObjects_DisplayMember_ValueMember()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(500);

                var input = browser.ElementAt("#section3 input[type=text]", 1);
                var result1 = browser.ElementAt("#section3 .result", 0);

                input.Clear();
                input.SendKeys("Cze");
                browser.SendEnterKey();
                input.CheckIfValue("Czech Republic");
                result1.CheckIfInnerTextEquals("1");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("");
                result1.CheckIfInnerTextEquals("");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                input.CheckIfValue("Country 5");
                result1.CheckIfInnerTextEquals("6");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section3 .tt-selectable").Click();
                input.CheckIfValue("Germany");
                result1.CheckIfInnerTextEquals("2");

                browser.ElementAt("#buttons input", 2).Click();

                input.CheckIfValue("Country 5");
                result1.CheckIfInnerTextEquals("6");
            });
        }

    }
}
