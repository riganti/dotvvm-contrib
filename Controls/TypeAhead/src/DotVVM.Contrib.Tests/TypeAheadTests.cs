using DotVVM.Contrib.Tests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests
{
    public class TypeAheadTests : AppSeleniumTest
    {
        public TypeAheadTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
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
                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "Czech Republic");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "Country 5");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section1 .tt-selectable").Click();
                AssertUI.Value(input, "Germany");
                AssertUI.InnerTextEquals(result1, "Germany");

                browser.ElementAt("#buttons input", 1).Click();

                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "Country 5");
            });
        }

        [Fact]
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
                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "1");
                AssertUI.InnerTextEquals(result2, "Czech Republic");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");
                AssertUI.InnerTextEquals(result2, "");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "6");
                AssertUI.InnerTextEquals(result2, "Country 5");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section2 .tt-selectable").Click();
                AssertUI.Value(input, "Germany");
                AssertUI.InnerTextEquals(result1, "2");
                AssertUI.InnerTextEquals(result2, "Germany");
            });
        }


        [Fact]
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

                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "1");

                input.Clear();
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");

                browser.ElementAt("#buttons input", 0).Click();

                input.Clear();
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "6");

                input.Clear();
                input.SendKeys("Ger");
                browser.First("#section3 .tt-selectable").Click();
                AssertUI.Value(input, "Germany");
                AssertUI.InnerTextEquals(result1, "2");

                browser.ElementAt("#buttons input", 2).Click();

                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "6");
            });
        }



        [Fact]
        public void TypeAhead_Sample2_SelectItemOnCursor()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");
                browser.Wait(500);

                var input1 = browser.ElementAt("#section1 input[type=text]", 1);
                var result1 = browser.ElementAt("#section1 .result", 0);
                var input2 = browser.ElementAt("#section2 input[type=text]", 1);
                var result2 = browser.ElementAt("#section2 .result", 0);

                // select using arrows
                input2.Clear();
                input2.SendKeys("a");
                input2.SendKeys(Keys.ArrowDown);
                input2.SendKeys(Keys.ArrowDown);
                input2.SendKeys(Keys.Return);
                AssertUI.Value(input2, "A2");

                input2.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result2, "1");

                // select first item
                input2.Clear();
                AssertUI.InnerTextEquals(result2, "2");
                input2.SendKeys("b");
                input2.SendKeys(Keys.Return);
                AssertUI.Value(input2, "B1");

                input2.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result2, "3");

                // select first item in first list
                input1.Clear();
                input1.SendKeys("a");
                input1.SendKeys(Keys.Return);
                AssertUI.Value(input1, "A1");

                input1.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result1, "1");
            });
        }
    }
}
