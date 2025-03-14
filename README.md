# SoundStreamAppiumTests2

## Project Description
SoundStreamAppiumTests2 is an automated testing framework using **Appium** and **Selenium WebDriver** for testing the web application [SoundStream Backend](https://soundstream-backend.onrender.com/) on Android devices.

![image](https://github.com/user-attachments/assets/ce7b537e-e9f3-4215-8c68-21bd91529f54)

## Features
- Web automation testing using **Appium** and **Selenium**
- **Page Object Model (POM)** for better code structure
- **WebDriver Helper** for reusable browser operations
- **Screenshot Capture** on test failure
- **Infra based on interfaces** for easy extension

## Installation & Setup

### **1. Clone the Repository**
```sh
 git clone https://github.com/your-repo/SoundStreamAppiumTests2.git
 cd SoundStreamAppiumTests2
```

### **2. Install Dependencies**
Ensure you have the required **NuGet** packages installed:
```sh
 dotnet restore
```

### **3. Setup Appium**
Start Appium Server:
```sh
 appium --relaxed-security --allow-insecure chromedriver_autodownload
```

### **4. Start Android Emulator**
If you’re using an Android emulator:
```sh
 C:\Users\erani\AppData\Local\Android\Sdk\emulator\emulator -avd Pixel_7
```

### **5. Run Tests**
Execute the tests using NUnit:
```sh
 dotnet test
```

## Troubleshooting
### **Git Permission Denied Issue**
If you encounter a `Permission denied` error when running `git add .`, follow these steps:
1. **Close Visual Studio**
2. Run:
   ```sh
   git rm -r --cached .vs/
   echo ".vs/" >> .gitignore
   git add .
   git commit -m "Ignoring .vs folder"
   git push -u origin main
   ```

### **Appium Issues**
- Ensure `ANDROID_HOME` is correctly set in environment variables.
- Ensure Appium Server is running with the correct permissions.
- If **ChromeDriver not found**, use:
  ```sh
  appium --relaxed-security --allow-insecure chromedriver_autodownload
  ```

## Folder Structure
```
SoundStreamAppiumTests2/
│-- src/
│   ├── Managers/        # WebDriverManager & BrowserHelper
│   ├── Tests/           # Test cases
│   ├── Utils/           # Helper classes
│-- output/              # Screenshots & logs
│-- .gitignore           # Ignore unnecessary files
│-- README.md            # Project documentation
```

## Contributing
Feel free to fork this project and submit a **pull request**! 🚀

## License
MIT License

