# SeleniumTestProject
## Overview
    This application i.e. AutomationTest contains test scenarios mentioned in Assignment file. It contains total of 2 test cases regarding Login -> Profile Update, Select fund type (add fund)-> Open how to book. Test case automation done using selenium (Chrome webdriver) in c# using NUnit test framework on .NET 6.0 application.

## Architecture
Project consist of Page models i.e. </br>
1. LoginPage.
2. DashboardPage.
3. PageModelBase.

Along with test file i.e. "PhpTravelsTestScenarios.cs" that basically uses these above page models. Also user information to update profile is read from Json file 'UserInfo.json' to make inputs dynamic that is used by UserInfo.cs class for internal use.

## Application Info
	Type: Console Application.
	Language: c#.
	Framework: .NET 6.0.
	Automation Tool: Selenium.
	Test Framework: NUnit.

> Note: Kindly place userinfo.json file inside 'appdir\AutomationTest\AutomationTest\bin\Debug\net6.0' before execution.
