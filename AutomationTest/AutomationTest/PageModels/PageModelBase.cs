using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.PageModels
{
    public class PageModelBase
    {
        protected IWebDriver _driver;
        protected virtual string Url { get;}
        protected virtual string Title { get; }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(Url);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            if (!((_driver.Url == Url) && (_driver.Title == Title)))
            {
                throw new Exception($"Page loading failed. Page URL = {_driver.Url}. Page source = {_driver.PageSource}");
            }
        }

    }
}
