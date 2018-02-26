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

                browser.Single("#CkEditorMinimal");
                browser.Single("#cke_CkEditorMinimal");
           
            });
        }


        [TestMethod]
        public void CkEditorMinimal_Sample2()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample2");
                browser.Wait(2000);

                browser.Single("#CkEditorMinimal_1");
                browser.Single("#CkEditorMinimal_2");
                browser.Single("#cke_CkEditorMinimal_1");
                browser.Single("#cke_CkEditorMinimal_2");

            });
        }
    }
}
