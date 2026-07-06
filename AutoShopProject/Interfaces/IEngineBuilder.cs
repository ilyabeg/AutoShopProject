using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Interfaces
{
    internal interface IEngineBuilder
    {
        public IEngineBuilder Init();
        public IEngineBuilder SetType(string type);
        public IEngineBuilder SetVolume(double volume);
        public IEngineBuilder SetHorsepower(int horsepower);
    }
}
