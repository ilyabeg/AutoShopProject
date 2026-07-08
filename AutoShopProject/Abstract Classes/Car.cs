using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal abstract class Car
    {
        public string CarType { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public int Year { get; set; }
        public string Drivetrain { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }

        public override string ToString()
        {
            return
                $"\t{"Car Type:",-20} {this.CarType}" +
                $"\n\t{"Car Manufacturer:",-20} {this.Manufacturer}" +
                $"\n\t{"Car Model:",-20} {this.Model}" +
                $"\n\t{"Car Engine ID:",-20} {this.Engine.id}" +
                $"\n\t{"Car Year:",-20} {this.Year}" +
                $"\n\t{"Car Drivetrain:",-20} {this.Drivetrain}" +
                $"\n\t{"Car Seat number:",-20} {this.Seats}" +
                $"\n\t{"Car Door number:",-20} {this.Doors}" +
                $"\n\t{"Car Price:",-20} ${this.Price}" +
                $"\n\t{"In Stock:",-20} {this.InStock}" +
                "\n\n";
        }

        public override bool Equals(object? obj)
        {            
            if (obj == null) return false;

            Car other = (Car)obj;
            return this.Manufacturer.Equals(other.Manufacturer, StringComparison.OrdinalIgnoreCase) &&
                   this.Model.Equals(other.Model, StringComparison.OrdinalIgnoreCase) &&
                   this.Engine.id.Equals(other.Engine.id, StringComparison.OrdinalIgnoreCase) &&
                   this.Year == other.Year;
        }
    }
}
