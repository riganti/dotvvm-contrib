using DotVVM.Contrib.EditableForm.Tests.Core;
using OpenQA.Selenium;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.EditableForm.Tests
{
    public class ComponentTests : AppSeleniumTest
    {
        [Fact]
        public void ComponentBasicTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                var editableForm = browser.Single("#editable-form");
                var firstNameInput = editableForm.Single("#form1").Single("input", By.TagName);
                var lastNameInput = editableForm.Single("#form2").Single("input", By.TagName);
                var vipInput = editableForm.Single("#form3").Single("input", By.TagName);
                var editButton = editableForm.Single("editableform__button-edit", By.ClassName);
                var saveButton = editableForm.Single("editableform__button-save", By.ClassName);
                var cancelButton = editableForm.Single("editableform__button-cancel", By.ClassName);

                AssertUI.IsNotEnabled(firstNameInput);
                AssertUI.IsNotEnabled(lastNameInput);
                AssertUI.IsNotEnabled(vipInput);
                AssertUI.IsNotDisplayed(saveButton);
                AssertUI.IsNotDisplayed(cancelButton);

                editButton.Click();

                AssertUI.IsEnabled(firstNameInput);
                AssertUI.IsEnabled(lastNameInput);
                AssertUI.IsEnabled(vipInput);
                AssertUI.IsDisplayed(saveButton);
                AssertUI.IsDisplayed(cancelButton);

            });
        }
        [Fact]
        public void ComponentTestCustomTemplate()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var editableForm = browser.Single("#editable-form");
                var header = editableForm.Single("card-header", By.ClassName);
                AssertUI.TextEquals(header, "This is custom template.");

            });
        }
        [Fact]
        public void ComponentTest()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");

                var editableForm = browser.Single("#editable-form");
                var firstNameInput = editableForm.Single("#form1").Single("input", By.TagName);
                var lastNameInput = editableForm.Single("#form2").Single("input", By.TagName);
                var vipInput = editableForm.Single("#form3").Single("input", By.TagName);
                var editButton = editableForm.Single("editableform__button-edit", By.ClassName);
                var saveButton = editableForm.Single("editableform__button-save", By.ClassName);
                var cancelButton = editableForm.Single("editableform__button-cancel", By.ClassName);

                editButton.Click();
                firstNameInput.Clear().SendKeys("Riganti");
                lastNameInput.Clear().SendKeys("Brno");
                vipInput.Click();
                cancelButton.Click();

                AssertUI.TextEquals(firstNameInput, "Mr.");
                AssertUI.TextEquals(lastNameInput, "DotVVM");
                AssertUI.IsChecked(vipInput);

                editButton.Click();
                firstNameInput.Clear().SendKeys("Riganti");
                lastNameInput.Clear().SendKeys("Brno");
                vipInput.Click();
                saveButton.Click();

                AssertUI.TextEquals(firstNameInput, "Riganti");
                AssertUI.TextEquals(lastNameInput, "Brno");
                AssertUI.IsNotChecked(vipInput);

                //back to default state of db
                editButton.Click();
                firstNameInput.Clear().SendKeys("Mr.");
                lastNameInput.Clear().SendKeys("DotVVM");
                vipInput.Click();
                saveButton.Click();
            });
        }

        public ComponentTests(ITestOutputHelper output) : base(output)
        {
        }
    } 
}
