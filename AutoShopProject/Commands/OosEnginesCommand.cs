using AutoShopProject.Application;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Commands
{
    internal class OosEnginesCommand : ICommandUser
    {
        private static readonly object _lock = new object();
        public void Execute()
        {
            lock (_lock)
            {
                Catalog.ShowOOSEngines();
            }
        }
    }
}
