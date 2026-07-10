using AutoShopProject.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Application
{
    internal class PriceManager
    {
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
        /// maps all the cars to the provided property, then filters them by the provided value,
        /// and finaly, raises the price for all those cars with the provided value. 
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void RaiseCarPrices(string property, string value)
        {
            Filtered<property.Type, Car> cars = new Filtered<property.Type, Car>(Catalog.catalog, c => c.property);
            var filteredCars = cars.FilterBy(value);

            foreach (var car in filteredCars)
            {

            }
        }

        public static void RaiseEnginePrices(string property, string value)
        {

        }
    }
}
