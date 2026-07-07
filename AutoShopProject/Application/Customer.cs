using AutoShopProject.Command_Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Application
{
    internal class Customer
    {
        private CustomerCommandsUI cmd;

        public void Run()
        {
            // initiate and run customer commands
            cmd = new CustomerCommandsUI();
            cmd.Run();
        }
    }
}
