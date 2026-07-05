using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal abstract class Engine
    {
        protected string Type { get; set; }
        protected double Volume { get; set; }
        protected int Horsepower { get; set; }
    }
}
