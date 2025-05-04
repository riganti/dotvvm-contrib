﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Riganti.Selenium.AssertApi;
using Riganti.Selenium.Core;
using Riganti.Selenium.Core.Abstractions;
using Xunit.Abstractions;

namespace DotVVM.Contrib.Tests.Core
{
    public class AppSeleniumTest : SeleniumTest
    {
        public AppSeleniumTest(ITestOutputHelper output) : base(output)
        {
        }

        public void RunInAllBrowsers(Action<IBrowserWrapper> testBody, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            AssertApiSeleniumTestExecutorExtensions.RunInAllBrowsers(this, testBody, callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}
