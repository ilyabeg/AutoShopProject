using AutoShopProject.Application;

namespace AutoShopProject.Command_Helpers
{
    internal class OrderCommandHelper
    {
        private readonly static object _lock = new object();

        private Dictionary<int, Action> _car_actions = new Dictionary<int, Action>()
        {
            [1] = InStockCarOrder,
            [2] = OutOfStockCarOrder
        };
        private Dictionary<int, Action> _engine_actions = new Dictionary<int, Action>()
        {
            [1] = InStockEngineOrder,
            [2] = OutOfStockEngineOrder
        };

        public void OrderCar()
        {            
            Console.WriteLine("[COMMAND] Would you like to order 1 - Cars in stock? or 2 - Cars out of stock? >>");
            int input = GetUserInput();
            _car_actions[input]();
            Console.WriteLine("[COMMAND] Order process complete.");
        }

        public void OrderEngine()
        {
            Console.WriteLine("[COMMAND] Would you like to order 1 - Engines in stock? or 2 - Engines out of stock? >>");
            int input = GetUserInput();
            _engine_actions[input]();
            Console.WriteLine("[COMMAND] Order process complete.");
        }

        private static void OutOfStockCarOrder()
        {
            AddCommandHelper _helper = new AddCommandHelper();
            if (Catalog.oosCars.Count == 0)
            {
                Console.WriteLine("[COMMAND] No cars are out of stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowOOSCatalog();
                int input = GetDesition(Catalog.oosCars.Count);
                lock (_lock)
                {
                    var car = Catalog.oosCars[input];
                    _helper.TryAddCar(car);
                    _helper.TryAddEngine(car.Engine);

                    if (Catalog.oosCars.Contains(car)) Catalog.oosCars.Remove(car);
                    if (Catalog.oosEngines.Contains(car.Engine)) Catalog.oosEngines.Remove(car.Engine);
                }
            }
        }
        private static void OutOfStockEngineOrder()
        {
            AddCommandHelper _helper = new AddCommandHelper();
            if (Catalog.oosEngines.Count == 0)
            {
                Console.WriteLine("[COMMAND] No Engines are out of stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowOOSEngines();
                int input = GetDesition(Catalog.oosEngines.Count);
                lock (_lock)
                {
                    var engine = Catalog.oosEngines[input];
                    _helper.TryAddEngine(engine);

                    if (Catalog.oosEngines.Contains(engine)) Catalog.oosEngines.Remove(engine);
                }
            }
        }        

        private static void InStockCarOrder()
        {
            AddCommandHelper _helper = new AddCommandHelper();
            if (Catalog.catalog.Count == 0)
            {
                Console.WriteLine("[COMMAND] No cars in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowCarCatalog();
                int input = GetDesition(Catalog.catalog.Count);
                lock (_lock)
                {
                    var car = Catalog.catalog[input];
                    _helper.TryAddCar(car);
                    _helper.TryAddEngine(car.Engine);
                }
            }
        }

        private static void InStockEngineOrder()
        {
            AddCommandHelper _helper = new AddCommandHelper();
            if (Catalog.engines.Count == 0)
            {
                Console.WriteLine("[COMMAND] No Engines are in stock. Exiting Command...");
                return;
            }
            else
            {
                Catalog.ShowEngineCatalog();
                int input = GetDesition(Catalog.engines.Count);
                lock (_lock)
                {
                    var engine = Catalog.engines[input];
                    _helper.TryAddEngine(engine);
                }
            }
        }

        private int GetUserInput()
        {
            int input = -1;
            while (input != 1 && input != 2)
            {
                Console.WriteLine("[COMMAND] >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return input;
        }

        private static int GetDesition(int count)
        {
            int input = -1;
            while (input < 0 || input > count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return input;
        }
    }
}
