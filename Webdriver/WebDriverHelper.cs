using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class WebDriverHelper : BaseWebDriverManager
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public WebDriverHelper(IWebDriver webDriver, int timeoutInSeconds = 10)
    {
        driver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
    }

    // ✅ Improved Navigation method
    public void NavigateTo(string url)
    {
        Console.WriteLine($"Navigating to: {url}");
        driver.Navigate().GoToUrl(url);
    }

    // ✅ Improved Element Finder
    public IWebElement? FindElementByTag(string tagName)
    {
        try
        {
            return driver.FindElement(By.TagName(tagName));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine($"Element <{tagName}> not found on the page.");
            return null;
        }
    }

    // ✅ Find element by text (case insensitive)
    public IWebElement? FindByText(string text)
    {
        try
        {
            return wait.Until(drv => drv.FindElement(By.XPath($"//*[contains(text(), '{text}')]")));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine($"Element with text '{text}' not found.");
            return null;
        }
    }

    // ✅ Find element by ID
    public IWebElement? FindById(string id)
    {
        try
        {
            return wait.Until(drv => drv.FindElement(By.Id(id)));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine($"Element with ID '{id}' not found.");
            return null;
        }
    }

    // ✅ Find element by XPath
    public IWebElement? FindByXPath(string xpath)
    {
        try
        {
            return wait.Until(drv => drv.FindElement(By.XPath(xpath)));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine($"Element with XPath '{xpath}' not found.");
            return null;
        }
    }

    // ✅ Click an element
    public void ClickElement(IWebElement element)
    {
        try
        {
            wait.Until(drv => element.Displayed && element.Enabled);
            element.Click();
            Console.WriteLine("Element clicked successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to click element: {ex.Message}");
            throw;
        }
    }

    // ✅ Enter text into an input field
    public void EnterText(IWebElement element, string text)
    {
        try
        {
            wait.Until(drv => element.Displayed && element.Enabled);
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine($"Entered text: {text}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to enter text: {ex.Message}");
            throw;
        }
    }

    // ✅ Wait for an element to be visible
    public IWebElement? WaitForElement(By by, int timeoutInSeconds = 10)
    {
        try
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds))
                .Until(drv => drv.FindElement(by));
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine($"Element {by} not found after waiting.");
            return null;
        }
    }

    // ✅ Take a screenshot and save it
    public void TakeScreenshot(string testName)
    {
        try
        {
            if (driver != null)
            {
                string screenshotsDirectory = GetScreenshotsDirectory();
                string screenshotFilePath = Path.Combine(screenshotsDirectory, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                // Take screenshot
                if (driver is ITakesScreenshot screenshotHandler)
                {
                    Screenshot screenshot = screenshotHandler.GetScreenshot();
                    string screenshotPath = Path.Combine("output", "screenshots", $"TestRun_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);

                    File.WriteAllBytes(screenshotFilePath, screenshot.AsByteArray);
                    Console.WriteLine($"Screenshot saved at: {screenshotPath}");
                }
                else
                {
                    Console.WriteLine("Driver does not support screenshots.");
                }            
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error taking screenshot: {ex.Message}");
        }
    }

    // ✅ Scroll to an element
    public void ScrollToElement(IWebElement element)
    {
        try
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Console.WriteLine("Scrolled to element.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to scroll: {ex.Message}");
        }
    }
}
