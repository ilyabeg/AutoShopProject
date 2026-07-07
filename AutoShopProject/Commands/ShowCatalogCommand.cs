using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class ShowCatalogCommand : ICommandUser
    {
        public void Execute()
        {
            Catalog.ShowCarCatalog();
        }
    }
}
