using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Commands
{
    internal class RemoveCommand : ICommandUser
    {
        private static readonly object _lock = new object();

        public void Execute()
        {
            Console.WriteLine("[COMMAND] What do you wish to select? 'C' (Car) or 'E' (Engine) >> ");
            string input = Console.ReadLine().Trim().ToUpper();

            if (input != "C" && input != "E")
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter: 'C' (Car) or 'E' (Engine) >> ");
                    input = Console.ReadLine().Trim().ToUpper();
                }
                while (input != "C" && input != "E");
            }


            if (input == "C")
                RemoveCar();
            else
                RemoveEngine();
        }

        // ---------------------------------------------------------------------------------

        // Helper methods...

        // car removal...

        private void RemoveCar()
        {
            Car wantedCar = FindWantedCar();

            if (wantedCar == null)
            {
                Console.WriteLine("[COMMAND] The car was not found. Exiting command...");
                return;
            }

            lock (_lock)
            {
                Catalog.catalog.Remove(wantedCar);
            }
            Console.WriteLine("[COMMAND] Car successfuly picked out!");
        }

        private Car FindWantedCar()
        {
            Console.WriteLine("[CAR] Step 1: Enter Car Manufacturer >>");
            string manufacturer = GetManufacturer();

            Console.WriteLine("[CAR] Step 2: Enter Car Model >>");
            string model = GetModel();

            Console.WriteLine("[CAR] Step 3: Enter Car Year >>");
            int year = GetYear();

            Console.WriteLine("[CAR] Final Step: Enter Engine ID >>");
            string engineID = GetEngineID();

            return PullCar(manufacturer, model, year, engineID);
        }

        private string GetManufacturer()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private string GetModel()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private int GetYear()
        {
            int year = 0;

            try
            {
                year = int.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (year > DateTime.Now.Year || year < 1930)
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid year input, please re-enter >>");
                    try
                    {
                        year = int.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (year > DateTime.Now.Year || year < 1930);
            }

            return year;
        }

        private string GetEngineID()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private Car PullCar(string manufacturer, string model, int year, string engineID)
        {
            foreach (Car car in Catalog.catalog)
            {
                if (car.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase) &&
                    car.Model.Equals(model, StringComparison.OrdinalIgnoreCase) &&
                    car.EngineID.Equals(engineID, StringComparison.OrdinalIgnoreCase) &&
                    car.Year == year)
                {
                    return car;
                }
            }
            return null;
        }
        
        //----------------------------------------------------------------------------------------

        // engine removal...

        private void RemoveEngine()
        {
            Engine wantedEngine = FindWantedEngine();

            if (wantedEngine == null)
            {
                Console.WriteLine("[COMMAND] The Engine was not found. Exiting command...");
                return;
            }

            lock (_lock)
            {
                Catalog.engines.Remove(wantedEngine);
            }
            Console.WriteLine("[COMMAND] Engine successfuly picked out!");
        }

        private Engine FindWantedEngine()
        {
            Console.WriteLine("[ENGINE] Step 1: Enter Engine Type >>");
            string engineType = GetEngineType();

            Console.WriteLine("[ENGINE] Step 2: Enter Engine Volume >>");
            double volume = GetEngineVolume();

            Console.WriteLine("[ENGINE] Final Step: Enter Engine ID >>");
            string engineID = GetEngineID();

            return PullEngine(engineID, engineType, volume);
        }

        private string GetEngineType()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private double GetEngineVolume()
        {
            double vol = -1.0;

            try
            {
                vol = double.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (vol < 1.0 || vol > 10.0)
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid Engine Volume, please re-enter >>");
                    try
                    {
                        vol = double.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (vol < 1.0 || vol > 10.0);
            }

            return vol;
        }

        private Engine PullEngine(string id, string type, double volume)
        {
            foreach (Engine engine in Catalog.engines)
            {
                if (engine.id.Equals(id, StringComparison.OrdinalIgnoreCase) &&
                    engine.Type.Equals(type, StringComparison.OrdinalIgnoreCase) &&
                    engine.Volume == volume)
                {
                    return engine;
                }
            }
            return null;
        }
    }
}
