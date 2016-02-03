using System;
using OpenQA.Selenium;
using System.Threading;
namespace GetMapTest
{
   public class Login
    {
        private IWebDriver driver;  
        public Login(IWebDriver driver)
        {
            this.driver = driver;
        }
        public Login get()
        {
           driver.Navigate().GoToUrl("http://91.143.44.249/sovzond/portal/login.aspx");
            return this;
        }
        public void click()
        {
             driver.FindElement(By.Id("cmdLogin")).Click();         
        }
        public Login login(String login, String passwd)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.Id("txtUser")).SendKeys(login);
            driver.FindElement(By.Id("txtPsw")).SendKeys(passwd);
            return this;
        }
    }
}
