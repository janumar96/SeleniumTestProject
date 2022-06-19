using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using AutomationTest.PageModels;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace AutomationTest
{
    public class Tests
    {
        WebDriver driver = null;

        [OneTimeSetUp]
        public void Setup()
        {
            //Credentials information...
            string email = "user@phptravels.com";
            string password = "demouser";
            //Initializing chrome driver for test cases execution...
            driver = new ChromeDriver();
            // Maximizing Windows..
            driver.Manage().Window.Maximize();

            // Initializing Login Page Model to login to website for further test executions...
            LoginPage login = new LoginPage(driver);
            login.NavigateTo();
            login.EnterEmail(email);
            //login.EnterEmail("janumar96@gmail.com");
            login.EnterPassword(password);
            login.ClickLogIn();
            //driver.FindElement(By.Id("cookie_stop")).Click();
        }

        [Test]
        public void UpdateProfile()
        {
            UserInfo userInfo = null;
            // Read user info from Json File to make it support Dynamin input values. It does not require to rebuild app for input change.
            try
            {
                string json = File.ReadAllText("UserInfo.json");
                userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
            }
            catch (DirectoryNotFoundException ex)
            {
                Assert.Fail("Unable to read user information because directory not found :" +ex.ToString());
            }
            catch (IOException ioexp)
            {
                Assert.Fail("Unable to read user information from file due to IO exception : " + ioexp.ToString());
            }

            // Initialize dashboard model to further perform actions on dashboard...
            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.EnsurePageLoaded();
            dashboard.ClickMyProfileLink();

            //Now profile page is opened, so
            //Fill out form to complete user information...
            if (userInfo != null)
            {
                //Fill firstName
                IWebElement firstname = driver.FindElement(By.XPath("//input[@name='firstname']"));
                firstname.Clear();
                firstname.SendKeys(userInfo.firstname);

                //Fill lastName after clearing it as text was already there.
                IWebElement lastname = driver.FindElement(By.XPath("//input[@name='lastname']"));
                lastname.Clear();
                lastname.SendKeys(userInfo.lastname);

                //Fill Phone
                IWebElement phone = driver.FindElement(By.XPath("//input[@name='phone']"));
                phone.Clear();
                phone.SendKeys(userInfo.phone);

                //Fill email
                IWebElement email = driver.FindElement(By.XPath("//input[@name='email']"));
                email.Clear();
                email.SendKeys(userInfo.email);

                //Fill password
                IWebElement password = driver.FindElement(By.XPath("//input[@name='password']"));
                password.Clear();
                password.SendKeys(userInfo.password);

                //Fill country
                SelectElement country = new SelectElement(driver.FindElement(By.Id("from_country")));
                country.SelectByValue(userInfo.countryCode);

                //Fill state
                IWebElement state = driver.FindElement(By.XPath("//input[@name='state']"));
                state.Clear();
                state.SendKeys(userInfo.state);

                //Fill city
                IWebElement city = driver.FindElement(By.XPath("//input[@name='city']"));
                city.Clear();
                city.SendKeys(userInfo.city);

                //Fill fax
                IWebElement fax = driver.FindElement(By.XPath("//input[@name='fax']"));
                fax.Clear();
                fax.SendKeys(userInfo.fax);

                //Fill postal
                IWebElement postalCode = driver.FindElement(By.XPath("//input[@name='zip']"));
                postalCode.Clear();
                postalCode.SendKeys(userInfo.zip);

                //Fill address1
                IWebElement address1 = driver.FindElement(By.XPath("//input[@name='address1']"));
                address1.Clear();
                address1.SendKeys(userInfo.address1);

                //Fill address2
                IWebElement address2 = driver.FindElement(By.XPath("//input[@name='address2']"));
                address2.Clear();
                address2.SendKeys(userInfo.address2);


                //Submit form to update profile...
                By updatebutton = By.XPath("//button[@type='submit']");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                IWebElement submitButton = wait.Until(ExpectedConditions.ElementIsVisible(updatebutton));

                //Adding sleep before javascript execution so that prior task is done.
                Thread.Sleep(3000);
                
                //Executing javascript to scroll page down until update profile button is visible... 
                var script = "arguments[0].scrollIntoView(true);";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script, submitButton);

                //Waiting for script to execute before button click...
                Thread.Sleep(5000);
                submitButton.Click();
                
                //Verify profile updated successfully or not...
                Assert.AreEqual(driver.FindElement(By.ClassName("alert-success")).Text, "Profile updated successfully.");
            }
            else
            {
                Assert.Fail("User Information is null");
            }
        }

        [Test]
        public void SelectFundAndClickHowToBook()
        {
            //Go to dashboard page...
            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.NavigateTo();

            dashboard.ClickAddFundLink();

            dashboard.HoverOverCompanyMenu();
            //Find and click on How to Book from dropdown...
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement howToBook = wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.XPath("//a[contains(text(),'How to Book')]"))));
            howToBook.Click();
            Assert.AreEqual(driver.Title, "How to Book - PHPTRAVELS");
            Assert.AreEqual(driver.Url, "https://www.phptravels.net/how-to-book");
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Dispose();
                driver.Quit();
            }

        }
    }
}