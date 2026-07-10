using AutoShopProject.Commands;
using AutoShopProject.Interfaces;
using System.Collections.Concurrent;

namespace AutoShopProject.Command_Managers
{
    internal class CustomerCommandsUI
    {
        private ConcurrentDictionary<string, ICommandUser> _commands = new ConcurrentDictionary<string, ICommandUser>() 
        { 
            ["show c"] = new ShowCatalogCommand(),
            ["show e"] = new ShowEnginesCommand(),
            ["buy"] = new RemoveCommand(),
            ["search"] = new SearchCommand(true), // true because this is the customer
            ["hist"] = new ShowHistoryCommand()
        };

        public void Run()
        {
            string command;
            bool flag = true;

            Console.WriteLine("[CUSTOMER] What would you like to perform?");
            DisplayCommandsList();

            while (flag)
            {
                command = Console.ReadLine().Trim().ToLower();
                flag = DetermineCommand(command);
            }
        }

        private bool DetermineCommand(string command)
        {
            if (command == "exit")
                return false;

            if (_commands.ContainsKey(command))
                _commands[command].Execute();
            else
                Console.WriteLine("[COMMAND] Invalid command, please re-enter >>");

            return true;
        }

        private void DisplayCommandsList()
        {
            Console.WriteLine("Commands List:" +
                "\n\tshow c -> Shows Car Catalog." +
                "\n\tshow e -> Shows Engines Catalog." +
                "\n\tbuy    -> Buy a car." +
                "\n\tsearch -> Search for a car/engine." +
                "\n\thist   -> Show Search History." + 
                "\n\texit   -> Return to main screen.\n");
        }
    }
}
