using DotVVM.Contrib.PolymorphTemplateSelector.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.PolymorphTemplateSelector.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        
        [Theory]
        [InlineData("/Sample1")]
        [InlineData("/Sample2")]
        public void BasicTest(string url)
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(url);

                var repeater = browser.Single("#repeater");

                var template0 = repeater.Single("#repeater_0_");
                AssertUI.TextEquals(template0.Single("h2", By.TagName), "#1");
                AssertUI.TextEquals(template0.First("p", By.TagName), "Do you like DotVVM?");
                AssertUI.ContainsElement(template0, "//p//label//input[@type='radio']", By.XPath);
                AssertUI.ContainsElement(template0, "//p//a[text()='Test postback']", By.XPath);

                var template1 = repeater.Single("#repeater_1_");
                AssertUI.TextEquals(template1.Single("h2", By.TagName), "#2");
                AssertUI.TextEquals(template1.First("p", By.TagName), "How satisfied are you with DotVVM?");
                AssertUI.ContainsElement(template1, "//p//input[@type='number']", By.XPath);
                AssertUI.ContainsElement(template1, "//p//a[text()='Test postback']", By.XPath);

                var template2 = repeater.Single("#repeater_2_");
                AssertUI.TextEquals(template2.Single("h2", By.TagName), "#3");
                AssertUI.TextEquals(template2.First("p", By.TagName), "Where did you learn about DotVVM?");
                AssertUI.ContainsElement(template2, "//p//select", By.XPath);
                AssertUI.ContainsElement(template2, "//p//a[text()='Test postback']", By.XPath);

                var template3 = repeater.Single("#repeater_3_");
                AssertUI.TextEquals(template3.Single("h2", By.TagName), "#4");
                AssertUI.TextEquals(template3.First("p", By.TagName), "Other comments...");
                AssertUI.ContainsElement(template3, "//p//textarea", By.XPath);
                AssertUI.ContainsElement(template3, "//p//a[text()='Test postback']", By.XPath);

                var template4 = repeater.Single("#repeater_4_");
                AssertUI.TextEquals(template4.Single("h2", By.TagName), "#5");
                AssertUI.ContainsElement(template4, "//p//a[text()='Test postback']", By.XPath);


            });
        }

        [Theory]
        [InlineData("/Sample1")]
        [InlineData("/Sample2")]
        public void MovingItemTest(string url)
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(url);

                var repeater = browser.Single("#repeater");

                var template0 = repeater.Single("#repeater_0_");
                var downBtn = template0.Single(".//p//input[@value='Move down']", By.XPath);
                downBtn.Click();

                repeater = browser.Single("#repeater");
                template0 = repeater.Single("#repeater_0_");
                AssertUI.TextEquals(template0.Single("h2", By.TagName), "#2");
                AssertUI.TextEquals(template0.First("p", By.TagName), "How satisfied are you with DotVVM?");
                AssertUI.ContainsElement(template0, "//p//input[@type='number']", By.XPath);
                AssertUI.ContainsElement(template0, "//p//a[text()='Test postback']", By.XPath);

                var template1 = repeater.Single("#repeater_1_");
                AssertUI.TextEquals(template1.Single("h2", By.TagName), "#1");
                AssertUI.TextEquals(template1.First("p", By.TagName), "Do you like DotVVM?");
                AssertUI.ContainsElement(template1, "//p//label//input[@type='radio']", By.XPath);
                AssertUI.ContainsElement(template1, "//p//a[text()='Test postback']", By.XPath);
            });
        }

        [Theory]
        [InlineData("/Sample1")]
        [InlineData("/Sample2")]
        public void DeleteItemTest(string url)
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(url);

                var repeater = browser.Single("#repeater");

                var template0 = repeater.Single("#repeater_0_");
                var deleteBtn = template0.Single(".//p//input[@value='Delete']", By.XPath);
                deleteBtn.Click();

                repeater = browser.Single("#repeater");
                template0 = repeater.Single("#repeater_0_");
                AssertUI.TextEquals(template0.Single("h2", By.TagName), "#2");
                AssertUI.TextEquals(template0.First("p", By.TagName), "How satisfied are you with DotVVM?");
                AssertUI.ContainsElement(template0, "//p//input[@type='number']", By.XPath);
                AssertUI.ContainsElement(template0, "//p//a[text()='Test postback']", By.XPath);

                var template2 = repeater.Single("#repeater_2_");
                var deleteBtn2 = template2.Single(".//p//input[@value='Delete']", By.XPath);
                deleteBtn2.Click();

                repeater = browser.Single("#repeater");
                template2 = repeater.SingleOrDefault("#repeater_2_");
                AssertUI.TextEquals(template2.Single("h2", By.TagName), "#5");
                AssertUI.ContainsElement(template2, "//p//a[text()='Test postback']", By.XPath);
            });
        }

        [Theory]
        [InlineData("/Sample1")]
        [InlineData("/Sample2")]
        public void AddNewItemsTest(string url)
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(url);
                browser.Single("#add-yesno-btn").Click();
                browser.Single("#add-choice-btn").Click();
                browser.Single("#add-number-btn").Click();
                browser.Single("#add-text-btn").Click();
                browser.Single("#add-text-btn").Click();

                var repeater = browser.Single("#repeater");

                var template5 = repeater.Single("#repeater_5_");
                AssertUI.TextEquals(template5.Single("h2", By.TagName), "#6");
                AssertUI.TextEquals(template5.First("p", By.TagName), "Do you like DotVVM?");
                AssertUI.ContainsElement(template5, "//p//label//input[@type='radio']", By.XPath);
                AssertUI.ContainsElement(template5, "//p//a[text()='Test postback']", By.XPath);

                var template6 = repeater.Single("#repeater_6_");
                AssertUI.TextEquals(template6.Single("h2", By.TagName), "#7");
                AssertUI.TextEquals(template6.First("p", By.TagName), "Where did you learn about DotVVM?");
                AssertUI.ContainsElement(template6, "//p//select", By.XPath);
                AssertUI.ContainsElement(template6, "//p//a[text()='Test postback']", By.XPath);

                var template7 = repeater.Single("#repeater_7_");
                AssertUI.TextEquals(template7.Single("h2", By.TagName), "#8");
                AssertUI.TextEquals(template7.First("p", By.TagName), "How satisfied are you with DotVVM?");
                AssertUI.ContainsElement(template7, "//p//input[@type='number']", By.XPath);
                AssertUI.ContainsElement(template7, "//p//a[text()='Test postback']", By.XPath);


                var template8 = repeater.Single("#repeater_8_");
                AssertUI.TextEquals(template8.Single("h2", By.TagName), "#9");
                AssertUI.TextEquals(template8.First("p", By.TagName), "Other comments...");
                AssertUI.ContainsElement(template8, "//p//textarea", By.XPath);
                AssertUI.ContainsElement(template8, "//p//a[text()='Test postback']", By.XPath);

                var template9 = repeater.Single("#repeater_9_");
                AssertUI.TextEquals(template9.Single("h2", By.TagName), "#10");
                AssertUI.ContainsElement(template9, "//p//a[text()='Test postback']", By.XPath);


            });
        }
        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
