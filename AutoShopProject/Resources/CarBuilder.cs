using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Resources
{
    internal class CarBuilder : IBuilder
    {
        private Car car;
        private ICarFactory _factory;

        // constructor
        public CarBuilder(ICarFactory carFactory)
        {
            _factory = carFactory;
        }

        // interface mehtods
        public IBuilder Init() 
        {
            //this.car = _factory.CreateCar();
            return this;
        }

        public IBuilder SetManufacturer(string manufacturer)
        {
            this.car.Manufacturer = manufacturer;
            return this;
        }

        public IBuilder SetModel(string model)
        {
            this.car.Model = model;
            return this;
        }

        public IBuilder SetEngine(Engine engine) 
        {
            this.car.Engine = engine;
            return this;
        }

        public IBuilder SetYear(int year)
        {
            this.car.Year = year;
            return this;
        }

        public IBuilder SetDrivetrain(string drivetrain)
        {
            this.car.Drivetrain = drivetrain;
            return this;
        }

        public IBuilder SetSeats(int seats)
        {
            this.car.Seats = seats;
            return this;
        }

        public IBuilder SetDoors(int doors)
        {
            this.car.doors = doors;
            return this;
        }
    }
}
