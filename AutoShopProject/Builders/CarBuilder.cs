using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Resources
{
    internal class CarBuilder : ICarBuilder
    {
        private Car _car;

        // constructor
        public CarBuilder(Car car) // <-- constructor dependency injection
        {
            _car = car;
        }

        // interface mehtods
        public ICarBuilder Init() 
        {
            return this;
        }
            
        public ICarBuilder SetManufacturer(string manufacturer)
        {
            _car.Manufacturer = manufacturer;
            return this;
        }

        public ICarBuilder SetModel(string model)
        {
            _car.Model = model;
            return this;
        }

        public ICarBuilder SetEngine(Engine engine) 
        {
            _car.Engine = engine;
            return this;
        }

        public ICarBuilder SetYear(int year)
        {
            _car.Year = year;
            return this;
        }

        public ICarBuilder SetDrivetrain(string drivetrain)
        {
            _car.Drivetrain = drivetrain;
            return this;
        }

        public ICarBuilder SetSeats(int seats)
        {
            _car.Seats = seats;
            return this;
        }

        public ICarBuilder SetDoors(int doors)
        {
            _car.doors = doors;
            return this;
        }

        // return product method
        public Car Build()
        {
            return _car;
        }
    }
}
