using AutoShopProject.Application;
using AutoShopProject.Command_Helpers;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class RemoveCommand : ICommandUser
    {
        private readonly RemoveCommandHelper _helper = new RemoveCommandHelper();

        public void Execute()
        {
            var print = new Dictionary<int, string>() {
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

            if (input == "C")
                _helper.RemoveCar();
            else
                _helper.RemoveEngine();
        }
    }
}
