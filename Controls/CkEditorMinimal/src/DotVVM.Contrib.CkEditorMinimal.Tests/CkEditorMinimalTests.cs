using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.CkEditorMinimal.Tests
{
    public class CkEditorMinimalTests : AppSeleniumTest
    {
        public CkEditorMinimalTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CkEditorMinimal_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
            browser.NavigateToUrl("/Sample1");
            browser.Wait(2000);

            browser.Single("#CkEditorMinimal");
            var ckEditor = browser.Single("#cke_CkEditorMinimal");

            browser.Driver.SwitchTo().Frame(0);

            var ckeEditable = browser.Single("cke_editable", By.ClassName);
            AssertUI.TextEquals(ckeEditable, "Sample text");
            ckeEditable.Clear();

            browser.Driver.SwitchTo().DefaultContent();
            var strongBtn = ckEditor.Single("cke_43", By.Id);
            strongBtn.Click();
            AssertUI.HasClass(strongBtn, "cke_button_on");

            browser.Driver.SwitchTo().Frame(0);
            ckeEditable.SendKeys("Tom Holland");
            AssertUI.TextEquals(ckeEditable, "Tom Holland");

            browser.Driver.SwitchTo().DefaultContent();
            var underlineBtn = ckEditor.Single("cke_45", By.Id);
            underlineBtn.Click();
            AssertUI.HasClass(strongBtn, "cke_button_on");

            browser.Driver.SwitchTo().Frame(0);
            ckeEditable.SendKeys(" is the best");
            AssertUI.TextEquals(ckeEditable, "Tom Holland is the best");

            browser.Driver.SwitchTo().DefaultContent();
            var centermiddleBtn = ckEditor.Single("cke_58", By.Id);
            centermiddleBtn.Click();
            AssertUI.HasClass(centermiddleBtn, "cke_button_on");

            strongBtn = ckEditor.Single("cke_43", By.Id);
            strongBtn.Click();
            AssertUI.HasClass(strongBtn, "cke_button_off");

            browser.Driver.SwitchTo().Frame(0);
            ckeEditable.SendKeys(" spider-man!");
            AssertUI.TextEquals(ckeEditable, "Tom Holland is the best spider-man!");

            browser.Driver.SwitchTo().DefaultContent();
            var text = browser.Single("#text");
            AssertUI.TextEquals(text, "<p style=\"text-align: center;\"><strong>Tom Holland<u>&nbsp;is the best</u></strong><u> spider-man!</u></p>");
            });
        }

        [Fact]
        public void CkEditorMinimal_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");
                browser.Wait(2000);

                browser.Single("#CkEditorMinimal_1");
                browser.Single("#CkEditorMinimal_2");
                
                var ckEditor1 = browser.Single("#cke_CkEditorMinimal_1");
                browser.Driver.SwitchTo().Frame(ckEditor1.Single("iframe", By.TagName).WebElement);
                var ckeEditable1 = browser.Single("cke_editable", By.ClassName);
                ckeEditable1.Clear();
                ckeEditable1.SendKeys("The dark knight is the best DC movie.");

                browser.Driver.SwitchTo().DefaultContent();

                var ckEditor2 = browser.Single("#cke_CkEditorMinimal_2");
                browser.Driver.SwitchTo().Frame(ckEditor2.Single("iframe",By.TagName).WebElement);
                var ckeEditable2 = browser.Single("cke_editable", By.ClassName);
                ckeEditable2.Clear();
                ckeEditable2.SendKeys("Change my mind.");

                browser.Driver.SwitchTo().DefaultContent();

                var text1 = browser.Single("#text1");
                var text2 = browser.Single("#text2");

                var btn = browser.Single("#button");

                AssertUI.TextEquals(text1, "Text 1: <p>The dark knight is the best DC movie.</p>");
                AssertUI.TextEquals(text2, "Text 2: <p>Change my mind.</p>");
                btn.Click();
                AssertUI.TextEquals(text1, "Text 1: <p>The Suicide Squad by James Gunn is the best DC movie.</p>");
                AssertUI.TextEquals(text2, "Text 2: <p>Change my mind.</p>");
            });
        }
    }
}