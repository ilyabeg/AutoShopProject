using AutoShopProject.Application;
using System.Linq;

namespace AutoShopProject.Filters
{
    internal class FilteredCars<TKey>
    {
        public ILookup<TKey, Car> _cars { get; private set; }
        private static readonly object _lock = new object();

        /// <summary>
        /// 
        /// Constructor that gets a Delegade method as an input to determine which property
        /// to filter the cars by. ToLookup accesses the cars catalog which isn't thread safe,
        /// hence the lock statement.
        /// 
        /// </summary>
        /// <param name="keyselector"></param>
        public FilteredCars(Func<Car, TKey> keyselector) 
        {
            lock (_lock)
            {
                _cars = Catalog.catalog.ToLookup(keyselector);
            }
        }

        /// <summary>
        /// 
        /// returns the Enumerable collection of cars filtered by the same key.
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<Car> FilterBy(TKey key)
        {
            return _cars[key];
        }
    }
}
