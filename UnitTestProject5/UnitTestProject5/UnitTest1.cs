using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject4
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;

        public object MessageBox { get; private set; }

        [TestMethod]
        public void TheXTest()
        {
            const string login = "txtUser";
            const string password = "txtPsw";
            string login1 = "gust";
            driver = new FirefoxDriver();

            driver.Navigate().GoToUrl("http://91.143.44.249/sovzond_test/portal/login.aspx?ReturnUrl=%2fsovzond_test%2fportal%2f");
            driver.FindElement(By.Id(login)).SendKeys(login1);
            driver.FindElement(By.Id(password)).SendKeys(login1);
            driver.FindElement(By.Id("cmdLogin")).Click();
            driver.FindElement(By.Id("lbLoginError"));

            Screenshot  = ((ITakesScreenshot)driver).GetScreenshot();

        }
            

        }
    }

    
    




 
