using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
namespace News_Appium
{
    [TestClass]
    public class NewsTest
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement> NewsApp;
        protected static WindowsDriver<WindowsElement> DesktopSession;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.BingNews_8wekyb3d8bbwe!AppexNews");
            NewsApp = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(NewsApp);
            NewsApp.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);
            Assert.IsNotNull(DesktopSession);
            ReturnToMainPage();
        }
        private static void ReturnToMainPage()
        {
            // Try to return to main page in case application is started in nested view
            try
            {
                AppiumWebElement backButton = null;
                do
                {
                    backButton = NewsApp.FindElementByAccessibilityId("Back");
                    backButton.Click();
                }
                while (backButton != null);
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
        }

        [ClassCleanup]
        public static void TearDown()
        {
            ReturnToMainPage();
            NewsApp.Quit();
        }

        [TestMethod]
        public void TopNav()
        {
            NewsApp.FindElementByName("My News").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("My Sources").Click();
            Thread.Sleep(2000);
            string text = NewsApp.FindElementByName("Your News. Your Sources.").Text.ToString();
            Assert.AreEqual("Your News. Your Sources.", text);
            NewsApp.FindElementByName("Top Stories").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("US").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("World").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Good News").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Politics").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Opinion").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Crime").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Technology").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Entertainment").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Money").Click();
            Thread.Sleep(2000);
            NewsApp.FindElementByName("Sports").Click();
            Thread.Sleep(2000);
            AppiumWebElement searchbox = NewsApp.FindElementByName("Search");
            searchbox.Clear();
            searchbox.SendKeys("Trump\r\n");
            Thread.Sleep(3000);
            string txtBlock = NewsApp.FindElementByClassName("TextBlock").Text;
            Assert.AreEqual(txtBlock, "Trump");
        }
    }
}
