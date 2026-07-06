using AutoShopProject.Builders;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static AutoShopProject.Interfaces.ICarFactory;

namespace AutoShopProject.Application
{
    internal class Director
    {
        private ICarFactory _factory;
        private ICarBuilder _cBuilder;
        private IEngineBuilder _eBuilder;

        public Director(ICarFactory factory)
        {
            _factory = factory;
        }

        public Car CreateSportCar()
        { 
            var engine = _factory.CreateEngine(EngineType.Sport);
            _eBuilder = new EngineBuilder(engine);
            

            var product = _factory.CreateCar(CarType.Sport);

            return product;
        }

        public Car CreateRaceCar()
        {
            var product = _factory.CreateCar(CarType.Race);
            var engine = _factory.CreateEngine(EngineType.Race);

            //

            return product;
        }

        public Car CreateMuscleCar()
        {
            var product = _factory.CreateCar(CarType.Muscle);
            var engine = _factory.CreateEngine(EngineType.Drag);

            //

            return product;
        }
    }
}
