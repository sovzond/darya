using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Drawing.Imaging;

namespace TestRange
{
   public class login
    {
        private IWebDriver driver;
      
        public login(IWebDriver driver)
        {
            this.driver = driver;
            

        }
  



         public login get1()
        {
           driver.Navigate().GoToUrl("http://91.143.44.249/sovzond/portal/login.aspx");
            return this;
        }

        public login click(IWebDriver driver)
        {

            driver.FindElement(By.Id("cmdLogin")).Click();
            return new login(driver);
        }


        public login loGin(String login, String passwd)
        {
           
            Thread.Sleep(2000);
            driver.FindElement(By.Id("txtUser")).SendKeys(login);
            driver.FindElement(By.Id("txtPsw")).SendKeys(passwd);
            return this;
        }

    }

}
