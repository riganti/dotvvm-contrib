using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Contrib.Tests
{
    [TestClass]
    public class ReactBridgeTests : SeleniumTestBase
    {
        [TestMethod]
        public void ReactBridge_Sample1()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/Sample1");

                // test the functionality
            });
        }
    }
}
