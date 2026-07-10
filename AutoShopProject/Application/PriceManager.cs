using AutoShopProject.Filters;
using System.Reflection;

namespace AutoShopProject.Application
{
    internal class PriceManager
    {
        private static Random rand = new Random();
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

            // get the list of cars which their property value has the provided value
            var cars = Catalog.catalog.Where(car => object.Equals(pinfo.GetValue(car), value)).ToList();

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

            // get the list of engines which their property value has the provided value
            var engines = Catalog.engines.Where(engine => object.Equals(pinfo.GetValue(engine), value)).ToList();

            foreach (var engine in engines)
                engine.Price += engine.Price * (rand.NextDouble() * 0.14 + 0.01); // raise price by 1% - 15%
        }
    }
}
