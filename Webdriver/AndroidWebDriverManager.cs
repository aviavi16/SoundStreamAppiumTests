using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.IO;

public class AndroidWebDriverManager : IWebDriverManager
{
    private AndroidDriver? _driver;
    public WebDriverHelper? BrowserHelper { get; private set; }

    public AndroidDriver InitWebDriver()
    {
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

        BrowserHelper = new WebDriverHelper(_driver);
        return _driver;
    }

    public IWebDriver? GetDriver() => _driver;

    public void Dispose()
    {
        try
        {
            if (_driver != null)
            {
                BrowserHelper?.TakeScreenshot("TestRun");
                _driver.Quit();
                Console.WriteLine("WebDriver closed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while closing WebDriver: {ex.Message}");
        }
        finally
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }

}
