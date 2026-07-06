using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class ShowCatalogCommand : ICommandUser
    {
        private Catalog _catalog;
        public void Execute()
        {
            _catalog = new Catalog();
            _catalog.ShowCarCatalog();
        }
    }
}
