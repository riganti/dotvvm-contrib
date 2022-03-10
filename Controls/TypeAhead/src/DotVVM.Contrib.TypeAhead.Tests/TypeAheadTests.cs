using System;
using DotVVM.Contrib.TypeAhead.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.TypeAhead.Tests
{
    public class TypeAheadTests : AppSeleniumTest
    {
        public TypeAheadTests(ITestOutputHelper output) : base(output)
        {
        }

        //Do not use input.Clear() on autocomplete elements. Input.Clear() will not work properly when element using auto complete.
        private void InputClearFix(IElementWrapper element, int keyInputsMaxCount = 100)
        {
            for (int i = 0; i < keyInputsMaxCount; i++)
            {
                if (element.GetValue().Length == 0)
                {
                    return;
                }
                element.SendKeys(Keys.Backspace);
            }

            throw new InvalidOperationException($"Element {element.GetTagName()} was not fully cleaned.");
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

                InputClearFix(input);
                input.SendKeys("Cze");
                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "Czech Republic");

                InputClearFix(input);
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");

                browser.ElementAt("#buttons input", 0).Click();

                InputClearFix(input);
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "Country 5");

                InputClearFix(input);
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

                InputClearFix(input);
                input.SendKeys("Cze");
                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "1");
                AssertUI.InnerTextEquals(result2, "Czech Republic");

                InputClearFix(input);
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");
                AssertUI.InnerTextEquals(result2, "");

                browser.ElementAt("#buttons input", 0).Click();

                InputClearFix(input);
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "6");
                AssertUI.InnerTextEquals(result2, "Country 5");

                InputClearFix(input);
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

                InputClearFix(input);
                input.SendKeys("Cze");

                input.SendEnterKey();
                AssertUI.Value(input, "Czech Republic");
                AssertUI.InnerTextEquals(result1, "1");

                InputClearFix(input);
                input.SendKeys("xxx");
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "");
                AssertUI.InnerTextEquals(result1, "");

                browser.ElementAt("#buttons input", 0).Click();

                InputClearFix(input);
                input.SendKeys("Cou");
                input.SendKeys(Keys.Tab);
                input.SendKeys(Keys.Tab);
                AssertUI.Value(input, "Country 5");
                AssertUI.InnerTextEquals(result1, "6");

                InputClearFix(input);
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
                InputClearFix(input2);
                input2.SendKeys("a");
                input2.SendKeys(Keys.ArrowDown);
                input2.SendKeys(Keys.ArrowDown);
                input2.SendKeys(Keys.Return);
                AssertUI.Value(input2, "A2");

                input2.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result2, "1");

                // select first item
                InputClearFix(input2);
                input2.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result2, "2");
                input2.SendKeys("b");
                input2.SendKeys(Keys.Return);
                AssertUI.Value(input2, "B1");

                input2.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result2, "3");

                // select first item in first list
                InputClearFix(input1);
                input1.SendKeys("a");
                input1.SendKeys(Keys.Return);
                AssertUI.Value(input1, "A1");

                input1.SendKeys(Keys.Tab);
                AssertUI.InnerTextEquals(result1, "1");
            });
        }
    }
}
