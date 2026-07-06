using AutoShopProject.Factories;
using AutoShopProject.Resources;

namespace AutoShopProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RaceCarFactory f = new RaceCarFactory();
            var car = f.CreateCar();

            CarBuilder builder = new CarBuilder(car);

            builder.Init()
                .SetYear(2000)
                .SetManufacturer("Ferrari");

            var builtCar = builder.Build();
        }
    }
}
