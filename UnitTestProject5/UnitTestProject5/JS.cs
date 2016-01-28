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
   public class TransformJS
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        public TransformJS(IWebDriver driver, IJavaScriptExecutor js)
        {
            this.driver = driver;
            this.js = js;

        }
        





        public string   getMapCenter()
        {
           
          return   (string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
         "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
         "var point1 = window.portal.stdmap.map.getCenter(); " +
         "var point2 = point1.transform(proj900913, projWGS84); " +
         "return point2.toString()}");
            
        }


        public string transferFrom(double getLon1, double getLat1)
        {
            
             return (string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
        "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
        string.Format("var point1 = new OpenLayers.LonLat({0},{1});", getLon1, getLat1) +
        "var point2 = point1.transform(proj900913, projWGS84); " +
        "return point2.toString()}");
          
        }

        public string getLonLatFromPixel(int x, int y)
        {
            
             return (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel( " + x + ", " + y + " )).toString()");//переводим экранные координаты
            
        }
    
       

    }
  }

