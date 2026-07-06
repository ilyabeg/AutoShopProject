using AutoShopProject.Interfaces;
using AutoShopProject.Builders;
using AutoShopProject.Resources;
using AutoShopProject.Factories;

namespace AutoShopProject.Application
{
    internal class Catalog
    {
        private readonly List<Car> _catalog = new List<Car>();

        public List<Car> GetCarCatalog()
        {
            foreach (Car in catalog.json)
            { 
            SportCarFactory sportCarFactory = new SportCarFactory();

            EngineBuilder eBuilder = new EngineBuilder(sportCarFactory.CreateEngine());
            CarBuilder cBuilder = new CarBuilder(sportCarFactory.CreateCar());

            eBuilder.SetType()
                .SetVolume()
                .SetHorsepower();
            var engine = eBuilder.Build();

            cBuilder.SetManufacturer()
                .SetModel()
                .SetEngine(engine)
                .SetYear()
                .SetDrivetrain()
                .SetSeats()
                .SetDoors();
            var product = cBuilder.Build();

            _catalog.Add(product);

            }
            return _catalog;
        }
    }
}
