using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Builders
{
    internal class EngineBuilder : IEngineBuilder
    {
        private Engine _engine;

        public EngineBuilder(Engine engine)
        {
            _engine = engine;
        }

        public IEngineBuilder Init()
        {
            return this;
        }

        public IEngineBuilder SetType(string type)
        {
            _engine.Type = type;
            return this;
        }

        public IEngineBuilder SetVolume(double volume) 
        { 
            _engine.Volume = volume;
            return this;
        }

        public IEngineBuilder SetHorsepower(int horsepower)
        { 
            _engine.Horsepower = horsepower;
            return this;
        }

        public Engine Build()
        {
            return _engine;
        }
    }
}
