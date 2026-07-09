using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AutoShopProject.Application
{
    internal class SearchHistory
    {
        private static SearchHistory _instance;
        public static List<string>? search_history { get; private set; }
        private static readonly object _lock = new object();

        private SearchHistory() 
        {
            if (_instance == null)
            {
                lock (_instance)
                {
                    if (_instance == null)
                    {
                        _instance = new SearchHistory();
                        search_history = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("search_history.json"));
                    }
                }
            }              
        }

        public static SearchHistory GetInstance() { return _instance; }

        public static void Handle(string search)
        {
            lock (_lock)
            {
                search_history.Add(search);
            }
        }

        public static void LogHistory()
        {
            lock (_lock)
            {
                string str = JsonSerializer.Serialize(search_history);
                File.WriteAllText("search_history.json", str);
            }
        }
    }
}
