using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Application
{
    internal class Manager
    {
        private readonly string _secretPassword = "123";
        private ManagerCommandsUI cmd;

        public void Login()
        {
            Console.WriteLine("[MANAGER] Enter secret password: ");
            string input = Console.ReadLine().Trim().ToUpper();

            if (!ValidPassword(input))
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid input. Try again: ");
                    input = Console.ReadLine().Trim().ToUpper();
                }
                while (!ValidPassword(input));
            }

            if (input == "EXIT") return;

            cmd = new ManagerCommandsUI();
            cmd.Run();
        }

        private bool ValidPassword(string input)
        {
            return input == _secretPassword && input != "EXIT";
        }
    }
}
