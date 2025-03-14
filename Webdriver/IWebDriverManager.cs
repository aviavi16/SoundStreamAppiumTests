using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;

public interface IWebDriverManager
{
    AndroidDriver InitWebDriver();
    IWebDriver? GetDriver(); 
    void Dispose();
}
