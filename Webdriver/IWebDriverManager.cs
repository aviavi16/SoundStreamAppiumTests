using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using OpenQA.Selenium;

namespace SoundStreamAppiumTests2
{
    public interface IWebDriverManager
    {
        IWebDriver StartWebDriverSession();
        void TakeScreenshot(string name);
        void QuitWebDriver();
        WebDriverHelper? BrowserHelper { get; }
        ITestLogger Logger { get; }
    }
}
