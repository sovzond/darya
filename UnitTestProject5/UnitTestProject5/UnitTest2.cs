using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace GetMapTest
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        





        [TestMethod]
        public void TheXTest()
        {
            driver = new FirefoxDriver();
            Login u = new Login(driver);
            string login1 = "guest";
            u.get1().login(login1, login1).click1();//вход на сайт

            driver.FindElement(By.Id("sovzond_widget_SimpleButton_104")).Click();
            Thread.Sleep(5000);

            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));
            var builder = new Actions(driver);
            builder.Click(element).Perform();
            IList<IWebElement> el = driver.FindElements(By.ClassName("svzLayerManagerItem"));
            for (int n = 0; n < el.Count; n++)
            {
                if (el[0].Text != "Google") Assert.Fail("не найден Google");
                if (el[4].Text != "Росреестр") Assert.Fail("не найден Росреестр");
                if (el[5].Text != "OpenStreetMap") Assert.Fail("не найден OpenStreetMap");
                if (el[6].Text != "Топооснова") Assert.Fail("не найден Топооснова");
                
                Thread.Sleep(5000);

                IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_3"));
                builder.Click(element1).Perform();

                IList <IWebElement> el1 = driver.FindElements(By.ClassName("olTileImage"));
               // Thread.Sleep(5000);
               
                
            }




        }





    }
}
