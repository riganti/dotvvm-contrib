using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class CkEditorMinimalTests : SeleniumTestBase
    {
        [TestMethod]
        public void CkEditorMinimal_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");
                browser.Wait(2000);

                browser.Single("#ClEditorMinimal");
                browser.Single("#cke_EditorMinimal");
           
            });
        }


        [TestMethod]
        public void CkEditorMinimal_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");
                browser.Wait(2000);

                browser.Single("#ClEditorMinimal_1");
                browser.Single("#ClEditorMinimal_2");
                browser.Single("#cke_EditorMinimal_1");
                browser.Single("#cke_EditorMinimal_2");

            });
        }
    }
}
