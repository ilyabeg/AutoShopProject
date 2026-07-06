using AutoShopProject.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace AutoShopProject.Builders
{
    internal class EngineBuilder : IEngineBuilder
    {
        private Engine _engine;

        // constructor
        public EngineBuilder(Engine engine) // <-- constructor DI
        {
            _engine = engine;
        }

        // interface methods
        public IEngineBuilder SetID(string id)
        {
            _engine.id = id;
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

        // return product method
        public Engine Build() => _engine;
    }
}
