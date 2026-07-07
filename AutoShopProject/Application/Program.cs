using AutoShopProject.Application;
using AutoShopProject.Factories;
using AutoShopProject.Resources;

namespace AutoShopProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // initiate shop and execute program
            AutoShop autoShop = new AutoShop();
            autoShop.Run();
        }
    }
}
