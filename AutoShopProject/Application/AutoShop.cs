
namespace AutoShopProject.Application
{
    internal class AutoShop
    {
        private Manager _manager = new Manager();
        private Customer _cutomer = new Customer();

        public void Run()
        {
            Catalog globalCatalog = Catalog.GetInstance();
            SearchHistory searchHistory = SearchHistory.GetInstance();

            while (true)
            {
                Console.WriteLine("Welcome to the Auto Shop! Please register as 'M' (MANAGER) or 'C' (CUSTOMER) or type 'EXIT' to quit >>");
                string input = Console.ReadLine().Trim().ToUpper();

                if (!IsValid(input))
                {
                    do
                    {
                        Console.WriteLine("Invalid input! Please register as 'M' (MANAGER) or 'C' (CUSTOMER) or type 'EXIT' >>");
                        input = Console.ReadLine().Trim().ToUpper();
                    }
                    while (!IsValid(input));
                }

                if (input == "EXIT")
                {
                    SearchHistory.LogHistory();
                    Catalog.SaveAllData();
                    return;
                }
                else if (input == "M")
                {
                    _manager.Login();
                }
                else
                {
                    _cutomer.Run();
                }
            }
        }

        private bool IsValid(string input)
        {
            return input == "M" || input == "C" || input == "EXIT";
        }
    }
}
