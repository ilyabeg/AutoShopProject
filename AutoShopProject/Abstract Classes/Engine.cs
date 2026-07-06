using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal abstract class Engine
    {
        public string Type { get; set; }
        public double Volume { get; set; }
        public int Horsepower { get; set; }
    }
}
