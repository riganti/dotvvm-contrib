using System;
using DotVVM.Contrib.Tests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Riganti.Selenium.Core;
using Xunit;
using Xunit.Abstractions;




namespace DotVVM.Contrib.Tests
{
    public class AddressLocationTests : AppSeleniumTest
    {
        [Theory]
        [InlineData("addressHardcoded")]
        [InlineData("addressBindable")]
        public void AddressBasic(string renderMode)
        {
            string sampleUrl = "/Address";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single(renderMode, By.Id);
                AssertUI.HasClass(map, "dotvvm-google-map");
            });
        }

        [Fact]
        public void AddressMarker()
        {
            string sampleUrl = "/Address";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("addressHardcoded", By.Id);
                var markerMap = map.Single("gmimap0", By.Id);
                var markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap0");
                AssertUI.Attribute(markerMap, "name", "gmimap0");

                map = browser.Single("addressBindable", By.Id);
                markerMap = map.Single("gmimap1", By.Id);
                markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap1");
                AssertUI.Attribute(markerMap, "name", "gmimap1");

            });
        }

        [Fact]
        public void AddressChange()
        {
            string sampleUrl = "/Address";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("addressHardcoded", By.Id);
                var btn = browser.Single("btnAddress", By.Id);

                float longitude = Convert.ToSingle(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('addressBindable').Map.getCenter().lng()"));
                float latitude = Convert.ToSingle(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('addressBindable').Map.getCenter().lat()"));

                btn.Click();
                browser.WaitFor(() =>
                {
                    float longitudeNew = Convert.ToSingle(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('addressBindable').Map.getCenter().lng()"));
                    float latitudeNew = Convert.ToSingle(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('addressBindable').Map.getCenter().lat()"));

                    Assert.NotEqual(longitude, longitudeNew);
                    Assert.NotEqual(latitude, latitudeNew);
                }, 2000);



            });
        }

        [Theory]
        [InlineData("locationHardcoded")]
        [InlineData("locationBindable")]
        public void LocationBasic(string renderMode)
        {
            string sampleUrl = "/Location";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single(renderMode, By.Id);
                AssertUI.HasClass(map, "dotvvm-google-map");
            });
        }

        [Fact]
        public void LocationChange()
        {
            string sampleUrl = "/Location";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("locationBindable", By.Id);
                var btn = browser.Single("btnLocation", By.Id);

                int longitude = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('locationBindable').Map.getCenter().lng()"));
                int latitude = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('locationBindable').Map.getCenter().lat()"));

                Assert.Equal(-117,longitude );
                Assert.Equal(44,latitude);
                btn.Click();

                longitude = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('locationBindable').Map.getCenter().lng()"));
                latitude = Convert.ToInt32(browser.GetJavaScriptExecutor().ExecuteScript("return document.getElementById('locationBindable').Map.getCenter().lat()"));

                Assert.Equal(-116, longitude);
                Assert.Equal(35, latitude);
            });
        }

        [Fact]
        public void LocationMarker()
        {
            string sampleUrl = "/Location";
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl(sampleUrl);
                browser.Wait(1000);
                var map = browser.Single("locationHardcoded", By.Id);
                var markerMap = map.Single("gmimap0", By.Id);
                var markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap0");
                AssertUI.Attribute(markerMap, "name", "gmimap0");

                map = browser.Single("locationBindable", By.Id);
                markerMap = map.Single("gmimap2", By.Id);
                markerImg = markerMap.ParentElement.Single("img", By.TagName);
                AssertUI.Attribute(markerImg, "usemap", "#gmimap2");
                AssertUI.Attribute(markerMap, "name", "gmimap2");

                browser.Driver.Quit();
            });
        }




        public AddressLocationTests(ITestOutputHelper output) : base(output)
        {
        }
    }
}
