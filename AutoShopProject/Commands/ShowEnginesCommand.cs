using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class ShowEnginesCommand : ICommandUser
    {
        private static readonly object _lock = new object();

        public void Execute() 
        {
            lock (_lock)
            {
                Catalog.ShowEngineCatalog();
            }
        }
    }
}
