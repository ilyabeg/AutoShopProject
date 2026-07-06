using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Interfaces
{
    internal interface ICarBuilder
    {
        public ICarBuilder SetManufacturer(string manufacturer);
        public ICarBuilder SetModel(string model);
        public ICarBuilder SetEngine(Engine engine);
        public ICarBuilder SetYear(int year);
        public ICarBuilder SetDrivetrain(string drivetrain);
        public ICarBuilder SetSeats(int seats);
        public ICarBuilder SetDoors(int doors);
        public ICarBuilder SetPrice(double price);
    }
}
