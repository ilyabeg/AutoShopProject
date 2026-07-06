using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Application
{
    internal class AutoShop
    {
        private Manager _manager;
        private Customer _cutomer;

        public void Run()
        {
            Console.WriteLine("Welcome to the Auto Shop! Please register as 'M' (MANAGER) or 'C' (Customer) >>");
            char input = Console.ReadKey().KeyChar;

            if (!IsValid(input))
            {
                do
                {
                    Console.WriteLine("Invalid input! Please register as 'M' (MANAGER) or 'C' (Customer) >>");
                    input = Console.ReadKey().KeyChar;
                }
                while (!IsValid(input));
            }

            if (input == 'M' || input == 'm')
            {
                _manager.Login();
            }
            else
            {

            }


        }

        private bool IsValid(char input)
        {
            return input == 'M' || input == 'C' || input == 'm' || input == 'c';
        }
    }
}
