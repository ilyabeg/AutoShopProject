using AutoShopProject.Filters;
using System.ComponentModel;
using System.Reflection;

namespace AutoShopProject.Application
{
    internal class PriceManager
    {
        private static Random rand = new Random();
        private static readonly object _lock = new object();
        public static void RaisePrices()
        {
            foreach (var search in SearchHistory.search_history)
            {
                String[] search_structure = search.Split(" ");

                var obj = search_structure[0];
                var property = search_structure[1];
                var value = search_structure[2];

                if (obj == "Car")
                    RaiseCarPrices(property, value);
                else
                    RaiseEnginePrices(property, value);
            }
        }

        /// <summary>
        /// 
        /// filters all the cars to the provided property by the provided value,
        /// and finaly, raises the price for all those cars with the provided value. 
        /// 
        /// for example: filters all cars by the manufacturer where the value is 'nissan', and raises all nissan cars price
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void RaiseCarPrices(string property, string value)
        {
            PropertyInfo pinfo = typeof(Car).GetProperty(property); // the actual property

            var propType = pinfo.PropertyType; // <- the Type of the provided property
            var converter = TypeDescriptor.GetConverter(propType); // <- Converter that knows how to convert any type to the wanted type
            var actualValue = converter.ConvertFromString(value); // <- actual value of the property converted from the string format

            // get the list of cars which their property value has the provided value
            var cars = Catalog.catalog.Where(car => object.Equals(pinfo.GetValue(car), actualValue)).ToList();

            foreach (var car in cars)
                car.Price += car.Price * (rand.NextDouble() * 0.14 + 0.01); // raise price by 1% - 15%
        }

        /// <summary>
        /// 
        /// filters all the engines to the provided property by the provided value,
        /// and finaly, raises the price for all those engines with the provided value. 
        /// 
        /// for example: filters all engines by the Type where the value is 'Race', and raises all Race engines price
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void RaiseEnginePrices(string property, string value)
        {
            PropertyInfo pinfo = typeof(Engine).GetProperty(property); // the actual property

            var propType = pinfo.PropertyType; // <- the Type of the provided property
            var converter = TypeDescriptor.GetConverter(propType); // <- Converter that knows how to convert any type to the wanted type
            var actualValue = converter.ConvertFromString(value); // <- actual value of the property converted from the string format

            // get the list of engines which their property value has the provided value
            var engines = Catalog.engines.Where(engine => object.Equals(pinfo.GetValue(engine), actualValue)).ToList();

            foreach (var engine in engines)
                engine.Price += engine.Price * (rand.NextDouble() * 0.14 + 0.01); // raise price by 1% - 15%
        }

        /// <summary>
        /// 
        /// raise specific car price by provided percents
        /// 
        /// </summary>
        /// <param name="percent"></param>
        public static void RaiseCarPrice(int percent)
        {
            if (Catalog.catalog.Count == 0)
            {
                Console.WriteLine("[COMMAND] No cars in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowCarCatalog();
                int input = GetDesition(Catalog.catalog.Count);
                lock (_lock)
                {
                    var car = Catalog.catalog[input];
                    car.Price += car.Price * (percent / 100.0);
                }
            }
        }

        /// <summary>
        /// 
        /// raise specific engine price by provided percents
        /// 
        /// </summary>
        /// <param name="percent"></param>
        public static void RaiseEnginePrice(int percent)
        {
            if (Catalog.engines.Count == 0)
            {
                Console.WriteLine("[COMMAND] No Engines in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowEngineCatalog();
                int input = GetDesition(Catalog.engines.Count);
                lock (_lock)
                {
                    var car = Catalog.catalog[input];
                    car.Price += car.Price * (percent / 100.0);
                }
            }
        }

        /// <summary>
        /// 
        /// lower specific car price by provided percents
        /// 
        /// </summary>
        /// <param name="percent"></param>
        public static void LowerCarPrice(int percent)
        {
            if (Catalog.catalog.Count == 0)
            {
                Console.WriteLine("[COMMAND] No cars in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowCarCatalog();
                int input = GetDesition(Catalog.catalog.Count);
                lock (_lock)
                {
                    var car = Catalog.catalog[input];
                    car.Price -= car.Price * (percent / 100.0);
                }
            }
        }

        /// <summary>
        /// 
        /// lower specific engine price by provided percents
        /// 
        /// </summary>
        /// <param name="percent"></param>
        public static void LowerEnginePrice(int percent)
        {
            if (Catalog.engines.Count == 0)
            {
                Console.WriteLine("[COMMAND] No Engines in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowEngineCatalog();
                int input = GetDesition(Catalog.engines.Count);
                lock (_lock)
                {
                    var car = Catalog.catalog[input];
                    car.Price -= car.Price * (percent / 100.0);
                }
            }
        }

        private static int GetDesition(int count)
        {
            int input = -1;
            while (input < 0 || input > count-1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return input;
        }
    }
}
