using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotVVM.Contrib.Tests2
{
    [TestClass]
    public class CkEditorMinimalTests : AppSeleniumTest
    {
        [TestMethod]
        public void CkEditorMinimal_Sample1()
        {
            Run(browser =>
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
            Run(browser =>
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
