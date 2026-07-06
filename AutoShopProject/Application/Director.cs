using AutoShopProject.Builders;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Application
{
    internal class Director
    {
        private ICarBuilder _cBuilder;
        private IEngineBuilder _eBuilder;

        public Director(ICarBuilder carBuilder, IEngineBuilder engineBuilder)
        {
            _cBuilder = carBuilder;
            _eBuilder = engineBuilder;
        }

        public Car CreateSportCar()
        { 

        }

        public Car CreateRaceCar()
{       {
            
        }

        public Car CreateMuscleCar()
        {

        }
    }
}
