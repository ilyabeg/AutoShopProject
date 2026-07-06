using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal abstract class Car
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public int Year { get; set; }
        public string Drivetrain { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public double Price { get; set; }
    }
}
