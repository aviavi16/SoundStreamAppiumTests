using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public abstract class BaseWebDriverManager
{
    protected readonly string ProjectRootDirectory;

    protected BaseWebDriverManager()
    {
        // 🔥 Hardcoded project directory
        ProjectRootDirectory = @"C:\develop\SoundStreamAppiumTests2";
    }

    protected string GetScreenshotsDirectory()
    {
        string outputDirectory = Path.Combine(ProjectRootDirectory, "output");
        string screenshotsDirectory = Path.Combine(outputDirectory, "screenshots");

        if (!Directory.Exists(screenshotsDirectory))
        {
            Directory.CreateDirectory(screenshotsDirectory);
        }

        return screenshotsDirectory;
    }
}
