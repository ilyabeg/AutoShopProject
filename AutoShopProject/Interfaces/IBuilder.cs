using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Interfaces
{
    internal interface IBuilder
    {
        public IBuilder Init();
        public IBuilder SetManufacturer(string manufacturer);
        public IBuilder SetModel(string model);
        public IBuilder SetEngine(Engine engine);
        public IBuilder SetYear(int year);
        public IBuilder SetDrivetrain(string drivetrain);
        public IBuilder SetSeats(int seats);
        public IBuilder SetDoors(int doors);
    }
}
