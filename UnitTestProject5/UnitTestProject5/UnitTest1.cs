using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Drawing.Imaging;

namespace GetMapTest
{

    [TestClass]
    public class UnitTestLogin
    {
        private IWebDriver driver = new FirefoxDriver();
        private TransformJS j = new TransformJS(new FirefoxDriver());
        private IWebElement getElementByText(IList<IWebElement> els, string text)
        {
            foreach (IWebElement el in els)
            {
                if (el.Text == text)
                {
                    return el;
                }
            }
            return null;
        }
        [TestMethod]
        public void GoToCoord()
        {
            Login u = new Login(driver);
            u.get1().login("guest", "guest").click(driver);//вход на сайт
            string startPoint = j.getMapCenter();// вычисляем координаты начальной точки центра. Получаем: "lon=70.51196941946516,lat=60.70837295920218"
            driver.FindElement(By.ClassName("gotoCoordsButton")).Click(); //делаем клик по иконке XY
            IList<IWebElement> elsTitle = driver.FindElements(By.ClassName("containerTitle"));
            //ищем текст "ПЕРЕХОД ПО КООРДИНАТАМ", при не нахождении возникает ошибка
            if (getElementByText(elsTitle, "ПЕРЕХОД ПО КООРДИНАТАМ") == null)
            {
                Assert.Fail("ПЕРЕХОД ПО КООРДИНАТАМ не найден");
            }
            InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(69, 59, 0).click();//нажимаем клавишу найти
            IList<IWebElement> img = driver.FindElements(By.ClassName("olAlphaImg"));//находим указатель
            int x = img[0].Location.X; //ищем координаты картинки по x
            int y = img[0].Location.Y; // ищем координаты картинки по y
            x = x + img[0].Size.Width / 2;//смещение по x картинки
            y = y - img[0].Size.Height / 3;//смещение по y картинки
            string Latimg1 = j.getLonLatFromPixel(x, y);//переводим экранные координаты
            LonLat imgCoord = new LonLat(Latimg1);// находи lon и lat кaртинки в неправильном формате
            string imgPoint = j.transferFrom(imgCoord.getLon(), imgCoord.getLat(), 900913, 4326);//находим правильный lon и lat 
            LonLat coord5 = new LonLat(imgPoint);
            double imgLon = coord5.getLon(); //находи lon кaртинки
            double imgLat = coord5.getLat();//находи lat кaртинки
            string changedPoint = j.getMapCenter();  // вычисляем координаты изменившегося центра. Получаем:"lon=69.9833333333329,lat=60.84722222222229"
            if (changedPoint == startPoint)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("центр не изменен");
            }
            LonLat changedCoord = new LonLat(changedPoint);
            double changedLon = changedCoord.getLon();//находим lon получившегося цента
            double changedLat = changedCoord.getLat();//находим lat получившегося цента
            DegreeFormat df1 = new DegreeFormat(69, 59, 0);
            double specLon1 = Math.Round(df1.getDecimalDegree(), 2);// находим lon введенный нами
            DegreeFormat df = new DegreeFormat(60, 50, 50);
            double specLat1 = Math.Round(df.getDecimalDegree(), 2);//находим lat введенный нами
            if (changedLon != specLon1 || changedLat != specLat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");
            }
            // проверяем находится ли указатель в координатах заданными нами
            if (imgLon != specLon1 || imgLat != specLat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");      
            }
        }
    }
}



















