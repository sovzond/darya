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
            return Math.Round(lon, 2);
        }
        public double getLat()
        {
            return Math.Round(lat, 2);
        }
        private double lon;
        private double lat;
    }
}
