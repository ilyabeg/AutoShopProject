using AutoShopProject.Interfaces;
using AutoShopProject.Builders;
using AutoShopProject.Resources;
using AutoShopProject.Factories;
using System.Text.Json;

namespace AutoShopProject.Application
{
    internal class Catalog
    {
        // car catalog list from json file
        private readonly List<Car>? _catalog = JsonSerializer.Deserialize<List<Car>>(File.ReadAllText("car_catalog.json"));

        // engines from json file
        private List<Engine>? _engines = JsonSerializer.Deserialize<List<Engine>>(File.ReadAllText("engines.json"));

        public void ShowCarCatalog()
        { 

        }
    }
}
