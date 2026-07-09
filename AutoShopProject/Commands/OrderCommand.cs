using AutoShopProject.Command_Helpers;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Commands
{
    internal class OrderCommand : ICommandUser
    {
        private OrderCommandHelper _helper = new OrderCommandHelper();
        public void Execute()
        {
            var print = new Dictionary<int, string>()
            {
                [0] = "[COMMAND] What do you wish to Order? 'C' (Car) or 'E' (Engine) >> ",
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

            if (input == "C")
                _helper.OrderCar();
            else
                _helper.OrderEngine();
        }
    }
}
