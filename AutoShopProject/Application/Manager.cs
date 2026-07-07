using AutoShopProject.Command_Managers;

namespace AutoShopProject.Application
{
    internal class Manager
    {
        private readonly string _secretPassword = "123";
        private ManagerCommandsUI cmd;

        public void Login()
        {
            Console.WriteLine("[MANAGER] Enter secret password or type EXIT: ");
            string input = Console.ReadLine().Trim().ToLower();

            if (!ValidPassword(input))
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid input. Try again: ");
                    input = Console.ReadLine().Trim().ToLower();
                }
                while (!ValidPassword(input));
            }

            if (input == "exit") return;

            // initate and run commands for manager
            cmd = new ManagerCommandsUI();
            cmd.Run();
        }

        private bool ValidPassword(string input)
        {
            return input.Equals(_secretPassword) || input == "exit";
        }
    }
}
