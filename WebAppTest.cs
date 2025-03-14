using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;


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
            webDriver = _manager.InitWebDriver();

            if (_manager == null || webDriver == null)
            {
                throw new Exception("WebDriver manager or WebDriver initialization failed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Setup failed: " + ex.Message);
            throw;
        }
    }

    [Test]
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
                _manager.Dispose();  // Automatically takes a screenshot
            }

            if (webDriver != null)
            {
                webDriver.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("TearDown failed: " + ex.Message);
        }
    }
}
