using AutoShopProject.Application;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Filters
{
    internal class FilteredEngines<TKey>
    {
        public ILookup<TKey, Engine> _engines { get; private set; }
        private static readonly object _lock = new object();

        /// <summary>
        /// 
        /// Constructor that gets a Delegade method as an input to determine which property
        /// to filter the engines by. ToLookup accesses the engines catalog which isn't thread safe,
        /// hence the lock statement.
        /// 
        /// </summary>
        /// <param name="keyselector"></param>
        public FilteredEngines(Func<Engine, TKey> keyselector)
        {
            lock (_lock)
            {
                _engines = Catalog.engines.ToLookup(keyselector);
            }
        }

        /// <summary>
        /// 
        /// returns the Enumerable collection of engines filtered by the same key.
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<Engine> FilterBy(TKey key)
        {
            return _engines[key];
        }
    }
}
