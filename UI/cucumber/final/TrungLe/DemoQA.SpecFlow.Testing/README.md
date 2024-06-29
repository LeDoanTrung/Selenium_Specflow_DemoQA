# Automation Test Project for TMS

An automation test project for TMS web, built on .NET 6 (C# is the main programming language), NUnit 3, and SpecFlow for behavior-driven development (BDD).

## Projects in this Solution

There are 2 projects in this solution:

1. **TMS.Core**: Contains all things that help to work with browser, reading configuration file, creating test reports, etc.
3. **TMS.Testing**: You write tests here and it depends on TMS.Core.

## Dependency Packages

| Package         | Description                               | Link                                             |
|-----------------|-------------------------------------------|--------------------------------------------------|
| ExtentReports   | Beautifully crafted reports               | [https://www.extentreports.com/docs/versions/4/net](https://www.extentreports.com/docs/versions/4/net) |
| SpecFlow        | Enables BDD with .NET                     | https://specflow.org/                  |         

## Development Tools

This project is set up by using Visual Studio 2022, so you can use it as the main IDE.  
You can also use Visual Code for this project, but you need to install .NET 5 SDK and some extensions for C# language to run this project effectively.

## Configuration Files

- The `appsetting.json` file is the main config file of this project, it allows you to configure the application URL and browser:
{
  "application": {
    "Browser": "chrome",
    "Enviroment": "staging",
    "url": "http://192.168.237.15:3000",
    "timeout.webdriver.wait.seconds": 10,
    "timeout.webdriver.pageLoad.seconds": 120,
    "timeout.webdriver.asyncJavaScript.seconds": 30
  }
}

- The `extent-config.xml` file is the config file of the ExtentReports library, it allows you to customize the report template like title, theme, etc.

## How to Run Tests

1. **Visual Studio 2022**:
   - Use Test Explorer to select tests to run.
2. **Visual Code**:
   - Install the .NET Core Test Explorer extension and then select tests to run.
3. **Command Lines**:
   - Restore all dependency packages: `dotnet restore`
   - Build project: `dotnet build`
   - Run tests: `dotnet test`

## Account Information
To access the system, please contact us via email at ledoantrung1999@gmail.com to obtain the necessary account credentials.