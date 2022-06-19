using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.PageModels
{
    public class DashboardPage : PageModelBase
    {
        protected override string Url => "https://www.phptravels.net/account/dashboard";
        protected override string Title => "Dashboard - PHPTRAVELS";

        public DashboardPage(WebDriver driver)
        {
            _driver = driver;
        }
        public void ClickMyProfileLink()
        {
            //Using LinkText to locate profile as it is the easiest to find it...
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            IWebElement profilelink = wait.Until(ExpectedConditions.ElementExists(By.LinkText("My Profile")));
            profilelink.Click();

            //Verify clicked page opened...
            Assert.AreEqual(_driver.Url, "https://www.phptravels.net/account/profile");
            Assert.AreEqual(_driver.Title, "Profile - PHPTRAVELS");
        }
        public void ClickAddFundLink()
        {
            //Using LinkText to locate Add funds as it is the easiest to find it...
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            IWebElement addfundlink = wait.Until(ExpectedConditions.ElementExists(By.LinkText("Add Funds")));
            addfundlink.Click();

            //Verify clicked page opened...
            Assert.AreEqual(_driver.Url, "https://www.phptravels.net/account/add_funds");
            Assert.AreEqual(_driver.Title, "Add Funds - PHPTRAVELS");
        }
        public void HoverOverCompanyMenu()
        {
            //Hover over to Company to display menu to select...
            IWebElement company = _driver.FindElement(By.XPath("//li[@class='footm']"));
            Actions actions = new Actions(_driver);
            actions.MoveToElement(company);
            actions.Perform();
        }

    }
}

