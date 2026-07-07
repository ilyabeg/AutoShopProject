using AutoShopProject.Application;
using AutoShopProject.Commands;

namespace AutoShopProject.Command_Managers
{
    internal class ManagerCommandsUI
    {
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
            switch (command)
            {
                case "show c":
                    new ShowCatalogCommand().Execute();
                    break;

                case "show e":
                    new ShowEnginesCommand().Execute();
                    break;

                case "add":
                    new AddCommand().Execute();
                    break;

                case "remove":
                    new RemoveCommand().Execute();
                    break;

                case "exit":
                    return false;

                default:
                    Console.WriteLine("[MANAGER] Invalid input.");
                    break;
            }
            return true;
        }

        private void DisplayCommandsList()
        {
            Console.WriteLine("Commands List:" +
                "\n\tshow c -> Shows Car Catalog." +
                "\n\tshow e -> Shows Engines Catalog." +
                "\n\tadd    -> Add new car to the catalog." +
                "\n\tremove -> Remove an existing car/engine." +
                "\n\texit   -> Return to main screen.\n");
        }
    }
}
