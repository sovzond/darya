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
       
    
        public object MessageBox { get; private set; }

        class DegreeFormat
        {
            public DegreeFormat(int deg, int min, int sec)
            {
                this.deg = deg;
                this.min = min;
                this.sec = sec;
            }

            public DegreeFormat(double deg)
            {
                this.deg = (int)deg;
                this.min = (int)Math.Round((deg - this.deg) * 60);
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

        class LonLat
        {
            public LonLat(string lonlat)
            {
                String[] arr = lonlat.Split(new Char[] { ',', '=' });
                this.lon = Double.Parse(arr[1]);
                this.lat = Double.Parse(arr[3]);
            }
            public LonLat(double lon, double lat)
            {
                this.lon = (double)lon;
                this.lat = (double)lat;
            }
             public double getLon()
            {
                return Math.Round(lon,2);
            }
            public double getLat()
            {
                return Math.Round(lat,2);
            }
            private double lon;
            private double lat;
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

            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string point = (string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
         "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
         "var point1 = window.portal.stdmap.map.getCenter(); " +
         "var point2 = point1.transform(proj900913, projWGS84); " +
         "return point2.toString()}");// вычисляем координаты начальной точки центра. Получаем: "lon=70.51196941946516,lat=60.70837295920218"
            /*
            string m=s1(point);
            string m1 = s2(point);
            double k= Convert.ToDouble(m);
            */
            LonLat coord = new LonLat(point);
            double lon2 = coord.getLon();
            double lat2 = coord.getLat(); 

            driver.FindElement(By.ClassName("gotoCoordsButton")).Click(); //делаем клик по иконке XY

            IList<IWebElement> element1 = driver.FindElements(By.ClassName("containerTitle"));

            //ищем текст "ПЕРЕХОД ПО КООРДИНАТАМ", при не нахождении возникает ошибка
            for (int n = 0; n < element1.Count; n++)
            {
                if (element1[n].Text.Equals("ПЕРЕХОД ПО КООРДИНАТАМ"))
                {
                    Assert.AreEqual(element1[n].Text, "ПЕРЕХОД ПО КООРДИНАТАМ");
                }

            }
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys("60");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys("50");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys("69");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys("59");
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys("0");
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_3")).Click();//нажимаем клавишу найти

            IList<IWebElement> p = driver.FindElements(By.ClassName("olAlphaImg"));
            
            int x = p[0].Location.X;
            int y = p[0].Location.Y;
            x = x + p[0].Size.Width/2;
            y = y - p[0].Size.Height /3;
            // string Latimg = (string)js.ExecuteScript("return window.portal.stdmap.map.OpenLayers_Map_6_OpenLayers_ViewPort.OpenLayers_Map_6_OpenLayers_Container.OpenLayers_Layer_Markers_100.OL_Icon_921.OL_Icon_921_innerImage.getPixelFromLonLat(new OpenLayers.Pixel( images/location_2.png )).toString()"); 
            string Latimg = (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel( " + x + ", " + y + " )).toString()");
            LonLat coord3 = new LonLat(Latimg);
            string script = (string)js.ExecuteScript( "{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
         "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
         string.Format("var point1 = new OpenLayers.LonLat({0},{1});", coord3.getLon(), coord3.getLat()) +
         "var point2 = point1.transform(proj900913, projWGS84); " +
         "return point2.toString()}");
           

            LonLat coord5 = new LonLat(script);
            double lon4 = coord5.getLon();
            double lat4 = coord5.getLat();



            string point1 = (string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
           "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
           "var point1 = window.portal.stdmap.map.getCenter(); " +
           "var point2 = point1.transform(proj900913, projWGS84); " +
           "return point2.toString()}");  // вычисляем координаты изменившегося центра. Получаем:"lon=69.9833333333329,lat=60.84722222222229"
            if(point1==point)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("центр не изменен");

            }
            LonLat coord1 = new LonLat(point1);
            double lon = coord1.getLon();//находим lon получившегося цента
            double lat = coord1.getLat();//находим lat получившегося цента
            DegreeFormat df1 = new DegreeFormat(69, 59, 0);
            double lon1 = Math.Round(df1.getDecimalDegree(),2 );// находим lon введенный нами
            DegreeFormat df = new DegreeFormat(60, 50, 50);
            double lat1 = Math.Round(df.getDecimalDegree(), 2);//находим lat введенный нами
            if (lon != lon1 || lat != lat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");

            }
            // проверяем находится ли указатель в координатах заданными нами
            if (lon4 != lon1 || lat4 != lat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");

            }

        }




    }
             

           


        }

    


    
    




 
