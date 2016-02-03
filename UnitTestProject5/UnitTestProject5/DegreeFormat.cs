using System;
namespace GetMapTest
{
    class DegreeFormat
    {
        public DegreeFormat(int deg, int min, int sec)//ввод чисел перезагрузка
        {
            this.deg = deg;
            this.min = min;
            this.sec = sec;
        }
        public DegreeFormat(double deg)//ввод числа перезагрузка
        {
            this.deg = (int)deg;
            this.min = (int)Math.Round((deg - this.deg) * 60);
            this.sec = (int)(((deg - this.deg) * 60 - this.min) * 60);
        }
        public double getDecimalDegree()//вывод числа из 1 перезагрузки
        {
            return deg + min / 60.0 + sec / 3600.0;
        }
        public int getDegree()//вывод 2
        { return deg; }
        public int getMinutes()//вывод 2
        { return min; }
        public int getSeconds()//вывод2
        { return sec; }
        private int deg;
        private int min;
        private int sec;
    }
}
