using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class ShowEnginesCommand : ICommandUser
    {
        public void Execute() 
        { 
            Catalog.ShowEngineCatalog();
        }
    }
}
