using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class ShowCatalogCommand : ICommandUser
    {
        private static readonly object _lock = new object();

        public void Execute()
        {
            lock (_lock)
            {
                Catalog.ShowCarCatalog();
            }            
        }
    }
}
