//using NUnit.Framework;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Appium;
//using OpenQA.Selenium.Appium.Android;
//using WebDriverManager;
//using WebDriverManager.DriverConfigs.Impl;
//using WebDriverManager.Helpers;
//using System;
//using OpenQA.Selenium.Support.UI;

//namespace SoundStreamAppiumTests
//{
//    [TestFixture]
//    public class WebAppTest : IDisposable
//    {
//        private AndroidDriver driver;
//        private WebDriverWait wait;

//        [SetUp]
//        public void Setup()
//        {
//            // Ensure ChromeDriver is available
//            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);

//            // Set Appium capabilities
//            var capabilities = new AppiumOptions
//            {
//                PlatformName = "Android",  // ✅ Directly using properties instead of AddAdditionalAppiumOption
//                DeviceName = "Pixel_7",    // ✅ No need to use AddAdditionalAppiumOption
//                AutomationName = "UiAutomator2",

//            };
//            // ✅ Enable automatic ChromeDriver download
//            capabilities.AddAdditionalAppiumOption("appium:chromedriverAutodownload", true);
//            capabilities.AddAdditionalAppiumOption("browserName", "Chrome");

//            // Start Appium session
//            driver = new AndroidDriver(new Uri("http://127.0.0.1:4723"), capabilities);

//            // Initialize WebDriverWait with 60 seconds timeout
//            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
//        }

//        [Test]
//        public void VerifyWebAppLoads()
//        {
//            driver.Navigate().GoToUrl("https://soundstream-backend.onrender.com/");

//            // Wait until the page is fully loaded
//            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

//            // Verify page title or URL
//            string currentUrl = driver.Url;
//            Console.WriteLine("Current URL: " + currentUrl);
//            Assert.That(currentUrl, Does.Contain("soundstream-backend.onrender.com"), "Website did not load correctly.");
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            Dispose();
//        }

//        public void Dispose()
//        {
//            if (driver != null)
//            {
//                driver.Quit();
//                driver.Dispose();
//            }
//        }
//    }
//}
