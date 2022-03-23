using System;
using DotVVM.Contrib.GoogleMap.Tests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;

namespace DotVVM.Contrib.GoogleMap.Tests
{
    public class ZoomTests : AppSeleniumTest
    {

        [Theory]
        [InlineData("zoomHardcoded")]
        [InlineData("zoomBindable")]
        public void ZoomBasic(string renderMode)
        {
            string sampleUrl = "/Zoom";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single(renderMode, By.Id);
                AssertUI.HasClass(map, "dotvvm-google-map");
            });
        }

        [Fact]
        public void ZoomMouse()
        {
            string sampleUrl = "/Zoom";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("zoomBindable", By.Id);
                var currentZoom = browser.Single("currentZoom", By.Id);
                var jsCurrentZoom = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('zoomBindable').Map.zoom"));

                //browser.Actions().MoveToElement(map.WebElement).Perform();
                map.Click();

                Assert.Equal("20", currentZoom.GetText());
                Assert.Equal(20, jsCurrentZoom);

                new Actions(browser.Driver).SendKeys("++").Perform();

                Assert.Equal("22", currentZoom.GetText());

                new Actions(browser.Driver).SendKeys("-----").Perform();
                jsCurrentZoom = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('zoomBindable').Map.zoom"));

                Assert.Equal("17", currentZoom.GetText());
                Assert.Equal(17, jsCurrentZoom);

            });
        }

        [Fact]
        public void ZoomMarker()
        {
            string sampleUrl = "/Zoom";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("zoomHardcoded", By.Id);
                var markerMap = map.Single("gmimap0", By.Id);
                var markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap0");
                AssertUI.Attribute(markerMap, "name", "gmimap0");

                map = browser.Single("zoomBindable", By.Id);
                markerMap = map.Single("gmimap1", By.Id);
                markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap1");
                AssertUI.Attribute(markerMap, "name", "gmimap1");

                browser.Driver.Quit();
            });
        }

        public ZoomTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
