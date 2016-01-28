using System;
using System.Collections.Generic;
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
        IList<IWebElement> element;
        private double A= 0;
        private double B = 0;
        private double C = 0;
        private double A1 = 0;
        private double B1 = 0;
        private double C1 = 0;
        public object MessageBox { get; private set; }
        class DegreeFormat
        {
            public DegreeFormat(double deg)
            {
                this.deg = deg;
                this.min = min;
                this.sec = sec;
            }
            public DegreeFormat(int deg, int min, int sec)
            {
                this.deg = (int)deg;
                this.min = (int)((deg - this.deg) * 60);
                this.sec = (int)(((deg - this.deg) * 60 - this.min) * 60);
            }

            public double getDecimalDegree()
            {
                return deg + min / 60.0 + sec / 3600.0;
            }

            public int getDegree() { return deg; }
            public int getMinutes() { return min; }
            public int getSeconds() { return sec; }

            private int deg;
            private int min;
            private int sec;
        }



        [TestMethod]
        public void TheXTest()
        {
            const string login = "txtUser";
            const string password = "txtPsw";
            string login1 = "guest";
            driver = new FirefoxDriver();

            driver.Navigate().GoToUrl("http://91.143.44.249/sovzond/portal/login.aspx");
            driver.FindElement(By.Id(login)).SendKeys(login1);
            driver.FindElement(By.Id(password)).SendKeys(login1);
            driver.FindElement(By.Id("cmdLogin")).Click();


            driver.FindElement(By.ClassName("gotoCoordsButton")).Click();
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("60");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys("69");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys("59");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys("0");
            Click();



            //driver.FindElement(By.Id("OpenLayers_Layer_Vector_123_svgRoot")).
           // IMouse.MouseMove();

            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
         
            string point = (string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
           "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
           "var point1 = window.portal.stdmap.map.getCenter(); " +
           "var point2 = point1.transform(proj900913, projWGS84); " +
           "return point2.toString()}");
            DegreeFormat df = new DegreeFormat(60, 49, 26);


        }




    }
             

           


        }

    


    
    




 
