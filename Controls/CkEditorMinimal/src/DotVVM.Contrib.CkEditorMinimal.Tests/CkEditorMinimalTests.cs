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
                browser.Single("#cke_CkEditorMinimal");
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
                browser.Single("#cke_CkEditorMinimal_1");
                browser.Single("#cke_CkEditorMinimal_2");
            });
        }
    }
}