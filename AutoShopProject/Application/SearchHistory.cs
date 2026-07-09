using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Application
{
    internal class SearchHistory
    {
        public static List<string> _search_history { get; private set; } = new List<string>();
        private static readonly object _lock = new object();

        public static void Handle(string search)
        {
            lock (_lock)
            {
                _search_history.Add(search);
            }
        }
    }
}
