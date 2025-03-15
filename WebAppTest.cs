using Allure.NUnit;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog.Core;
using Assert = NUnit.Framework.Assert;

[AllureNUnit]
[TestFixture]
public class WebAppTest
{
    private AndroidWebDriverManager? _manager;
    private IWebDriver? webDriver;

    [SetUp]
    public void Setup()
    {
        try
        {
            _manager = new AndroidWebDriverManager();
            _manager.Logger.LogInfo("Starting test setup..."); // ✅ Correct usage

            webDriver = _manager.StartWebDriverSession(); // ✅ Ensure correct method name

            if (_manager == null || webDriver == null)
            {
                throw new Exception("WebDriver manager or WebDriver initialization failed.");
            }
        }
        catch (Exception ex)
        {
            _manager?.Logger.LogError(ex, "Setup Failed");
            throw;
        }
    }

    [Test]
    [AllureStep("Verify Home Screen Loads Successfully")]
    public void VerifyWebAppLoads()
    {
        try
        {
            if (_manager == null || _manager.BrowserHelper == null)
            {
                throw new Exception("WebDriver manager or BrowserHelper is not initialized.");
            }

            _manager.BrowserHelper.NavigateTo("https://soundstream-backend.onrender.com/");
            IWebElement? bodyElement = _manager.BrowserHelper.FindElementByTag("body");

            Assert.That(bodyElement, Is.Not.Null, "Website did not load correctly.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Test failed: " + ex.Message);
            throw;
        }
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (_manager != null)
            {
                _manager.Logger.LogInfo("Tearing down WebDriver session...");

                // Capture a screenshot if the test failed
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    _manager.TakeScreenshot("TestFailureScreenshot");
                }

                // Quit WebDriver properly
                _manager.QuitWebDriver();
            }

            if (webDriver != null)
            {
                _manager?.Logger.LogInfo("Disposing WebDriver...");
                webDriver.Dispose();
            }
        }
        catch (Exception ex)
        {
            _manager?.Logger.LogError(ex, "TearDown Error");
        }
    }
}
