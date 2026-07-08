using AutoShopProject.Application;
using AutoShopProject.Commands;
using AutoShopProject.Interfaces;
using System.Collections.Concurrent;

namespace AutoShopProject.Command_Managers
{
    internal class ManagerCommandsUI
    {
        private ConcurrentDictionary<string, ICommandUser> _commands = new ConcurrentDictionary<string, ICommandUser>()
        {
            ["show c"] = new ShowCatalogCommand(),
            ["show e"] = new ShowEnginesCommand(),
            ["remove"] = new RemoveCommand(),
            ["add"] = new AddCommand(),
            ["search"] = new SearchCommand()
        };

        public void Run()
        {
            string command;
            bool flag = true;

            Console.WriteLine("[MANAGER] What would you like to perform?");
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
                "\n\tadd    -> Add new car to the catalog." +
                "\n\tremove -> Remove an existing car/engine." +
                "\n\tsearch -> Search for a car/engine." +
                "\n\texit   -> Return to main screen.\n");
        }
    }
}
