using AutoShopProject.Commands;

namespace AutoShopProject.Command_Managers
{
    internal class CustomerCommandsUI
    {
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
            switch (command)
            {
                case "show c":
                    new ShowCatalogCommand().Execute();
                    break;

                case "show e":
                    new ShowEnginesCommand().Execute();
                    break;

                case "buy":
                    new RemoveCommand().Execute();
                    break;

                case "exit":
                    return false;

                default:
                    Console.WriteLine("[CUSTOMER] Invalid input.");
                    break;
            }
            return true;
        }

        private void DisplayCommandsList()
        {
            Console.WriteLine("Commands List:" +
                "\n\tshow c -> Shows Car Catalog." +
                "\n\tshow e -> Shows Engines Catalog." +
                "\n\tbuy    -> Buy a car." +
                "\n\texit   -> Return to main screen.\n");
        }
    }
}
