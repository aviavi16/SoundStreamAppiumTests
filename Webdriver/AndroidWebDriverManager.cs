using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.IO;
using Serilog;
using Allure.NUnit.Attributes;
using SoundStreamAppiumTests2;
using Allure.Commons;
public class AndroidWebDriverManager : IWebDriverManager
{
    private AndroidDriver? _driver;
    public WebDriverHelper? BrowserHelper { get; private set; }
    public ITestLogger Logger { get; } 

    public AndroidWebDriverManager()
    {
        Logger = new TestLogger(); 
    }

    /// <summary>
    /// Starts a new WebDriver session, initializes logging, and sets up the BrowserHelper.
    /// </summary>
    public IWebDriver StartWebDriverSession()
    {
        try
        {
            Logger.LogInfo("Starting Android WebDriver session...");

            var options = new AppiumOptions
            {
                PlatformName = "Android",
                DeviceName = "Pixel_7", // Change as needed
                BrowserName = "Chrome",
                AutomationName = "UiAutomator2"
            };

            _driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), options);

            if (_driver == null)
            {
                throw new Exception("Failed to initialize Android WebDriver.");
            }

            Logger.LogInfo("Android WebDriver session started successfully.");
            BrowserHelper = new WebDriverHelper(_driver);
            Logger.LogInfo("BrowserHelper initialized successfully.");

            return _driver;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "WebDriver Initialization Error");
            throw;
        }
    }

    /// <summary>
    /// Takes a screenshot and attaches it to Allure reports.
    /// </summary>
    [AllureStep("Capture Screenshot")]

    public void TakeScreenshot(string name)
    {
        try
        {
            string screenshotsDirectory = ConfigurationHelper.GetScreenshotsDirectory();
            string screenshotFilePath = Path.Combine(screenshotsDirectory, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            if (_driver != null && _driver is ITakesScreenshot screenshotDriver)
            {
                Screenshot screenshot = _driver.GetScreenshot();
                string screenshotPath = Path.Combine("output", "screenshots", $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.png");


                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }

                File.WriteAllBytes(screenshotFilePath, screenshot.AsByteArray);
                Console.WriteLine($"Screenshot saved at: {screenshotPath}");

                Logger.LogInfo($"Screenshot captured: {screenshotPath}");

                // Attach to Allure
                AllureLifecycle.Instance.AddAttachment(name, "image/png", screenshotPath);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Screenshot Capture Error");
        }
    }

    //// ✅ Take a screenshot and save it
    //public void TakeScreenshot(string testName)
    //{
    //    try
    //    {
    //        if (_driver != null)
    //        {
    //            string screenshotsDirectory = GetScreenshotsDirectory();
    //            string screenshotFilePath = Path.Combine(screenshotsDirectory, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

    //            // Take screenshot
    //            if (_driver is ITakesScreenshot screenshotHandler)
    //            {
    //                Screenshot screenshot = screenshotHandler.GetScreenshot();
    //                string screenshotPath = Path.Combine("output", "screenshots", $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.png");

    //                // Ensure directory exists
    //                Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);

    //                File.WriteAllBytes(screenshotFilePath, screenshot.AsByteArray);
    //                Console.WriteLine($"Screenshot saved at: {screenshotPath}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Driver does not support screenshots.");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error taking screenshot: {ex.Message}");
    //    }
    //}


    /// <summary>
    /// Quits the WebDriver session.
    /// </summary>
    public void QuitWebDriver()
    {
        try
        {
            if (_driver != null)
            {
                Logger.LogInfo("Taking screenshot before closing WebDriver...");
                TakeScreenshot("TestFailureScreenshot");

                Logger.LogInfo("Closing WebDriver session...");
                _driver.Quit();
                _driver.Dispose();
                Logger.LogInfo("WebDriver session closed.");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "WebDriver Teardown Error");
        }
    }
}
