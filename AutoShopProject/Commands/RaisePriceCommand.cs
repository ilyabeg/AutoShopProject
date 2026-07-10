using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class RaisePriceCommand : ICommandUser
    {
        public void Execute()
        {
            var print = new Dictionary<int, string>()
            {
                [0] = "[COMMAND] What do you wish to select? 'C' (Car) or 'E' (Engine) >> ",
                [1] = "[COMMAND] Invalid input, please re-enter: 'C' (Car) or 'E' (Engine) >> "
            };
            string input = "";
            int attempt = 0;

            while (input != "C" && input != "E")
            {
                Console.WriteLine(print[attempt]);
                input = Console.ReadLine().Trim().ToUpper();
                attempt++;

                attempt = (attempt > 0) ? 1 : 0; // leave 1 as 1 ...
            }

            int percent = -1;
            while (percent < 1 || percent > 50)
            {
                Console.WriteLine("[COMMAND] Enter the percents you'd like to raise the price by (1% - 50%) >>");
                try
                {
                    percent = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }

            if (input == "C")
                PriceManager.RaiseCarPrice(percent);
            else
                PriceManager.RaiseEnginePrice(percent);
        }
    }
}
