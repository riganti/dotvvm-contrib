using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class ControlNameTests : SeleniumTestBase
    {
        [TestMethod]
        public void ControlName_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                // test the functionality
            });
        }
    }
}
