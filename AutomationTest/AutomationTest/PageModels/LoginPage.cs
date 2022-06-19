using AutomationTest.PageModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationTest.PageModels
{
    public class LoginPage : PageModelBase
    {      
        protected override string Url => "https://www.phptravels.net/login";
        protected override string Title => "Login - PHPTRAVELS";

        public LoginPage(WebDriver driver)
        {
            _driver = driver;
        }
        public void EnterEmail(string email)
        { 
            _driver.FindElement(By.Name("email")).SendKeys(email);
        }
        public void EnterPassword(string password)
        {
            _driver.FindElement(By.Name("password")).SendKeys(password);
        }
        public void ClickLogIn()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            By locator = By.XPath("//button[@type='submit']");
            //Wait for login button to be clickable incase not loaded...
            IWebElement submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            submitButton.Click();           
        }
    }   
}