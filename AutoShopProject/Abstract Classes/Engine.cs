using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal abstract class Engine
    {
        public string id { get; set; }
        public string Type { get; set; }
        public double Volume { get; set; }
        public int Horsepower { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }

        public override string ToString()
        {
            return
                $"\t{"Engine ID:",-20} {this.id}" +
                $"\n\t{"Engine Type:",-20} {this.Type}" +
                $"\n\t{"Engine Volume:",-20} {this.Volume}L" +
                $"\n\t{"Engine Horsepower:",-20} {this.Horsepower}" +
                $"\n\t{"Engine Price:",-20} ${this.Price}" +
                $"\n\t{"In Stock:",-20} {this.InStock}" +
                "\n\n";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            Engine other = (Engine)obj;
            return this.id.Equals(other.id, StringComparison.OrdinalIgnoreCase) &&
                   this.Type.Equals(other.Type, StringComparison.OrdinalIgnoreCase) &&
                   this.Volume == other.Volume;
        }
    }
}
