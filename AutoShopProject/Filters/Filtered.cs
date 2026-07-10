using AutoShopProject.Application;
using AutoShopProject.Interfaces;
using System.Linq;

namespace AutoShopProject.Filters
{
    internal class Filtered<TKey, TValue> : IFiltered
    {
        public ILookup<TKey, TValue> filteredLookUp { get; private set; }
        private static readonly object _lock = new object();

        /// <summary>
        /// 
        /// Constructor that gets a generic List Delegade method as an input to determine which property
        /// to filter the list by. ToLookup accesses the generic list which isn't thread safe,
        /// hence the lock statement.
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="keyselector"></param>
        public Filtered(List<TValue> list, Func<TValue, TKey> keyselector) 
        {
            lock (_lock)
            {
                filteredLookUp = list.ToLookup(keyselector);
            }
        }

        /// <summary>
        /// 
        /// returns the Enumerable collection of cars filtered by the same key.
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<TValue> FilterBy(TKey key)
        {
            return filteredLookUp[key];
        }
    }
}
