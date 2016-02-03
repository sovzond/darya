using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
namespace GetMapTest
{
    [TestClass]
    public class UnitTestLogin
    {
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
        private void GoToCoordWnd(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("gotoCoordsButton")).Click(); //делаем клик по иконке XY

            IList<IWebElement> elsTitle = driver.FindElements(By.ClassName("containerTitle"));
            //ищем текст "ПЕРЕХОД ПО КООРДИНАТАМ", при не нахождении возникает ошибка
            if (getElementByText(elsTitle, "ПЕРЕХОД ПО КООРДИНАТАМ") == null)
            {
                Assert.Fail("ПЕРЕХОД ПО КООРДИНАТАМ не найден");
            }
            InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(69, 59, 0).click();//нажимаем клавишу найти
        }
      [TestMethod]
        public void GoToCoord()
        {
            IWebDriver driver = new FirefoxDriver();
            TransformJS js = new TransformJS(driver);
            Login login = new Login(driver);
            login.get().login("guest", "guest").click();//вход на сайт
            LonLat startPoint = js.getMapCenter();//находим центр
            GoToCoordWnd(driver);// ищем по заданным координатам
            IList<IWebElement> img = driver.FindElements(By.ClassName("olAlphaImg"));//находим указатель
            int x = img[0].Location.X + img[0].Size.Width / 2; //ищем координаты картинки по x
            int y = img[0].Location.Y - img[0].Size.Height / 3; // ищем координаты картинки по y
            string Latimg1 = js.getLonLatFromPixel(x, y);//переводим экранные координаты
            LonLat imgCoord = new LonLat(Latimg1);// находи lon и lat кaртинки в неправильном формате
            string imgPoint = js.transferFrom(imgCoord.getLon(), imgCoord.getLat(), 900913, 4326);//находим правильный lon и lat 
            LonLat coord5 = new LonLat(imgPoint);
            double imgLon = coord5.getLon(); //находи lon кaртинки
            double imgLat = coord5.getLat();//находи lat кaртинки
            LonLat changedPoint = js.getMapCenter();  // вычисляем координаты изменившегося центра. Получаем:"lon=69.9833333333329,lat=60.84722222222229"
            if (LonLat.equalLonLat(changedPoint, startPoint)==false)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("центр не изменен");
            }
            double changedLon = changedPoint.getLon();//находим lon получившегося цента
            double changedLat = changedPoint.getLat();//находим lat получившегося цента
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



















