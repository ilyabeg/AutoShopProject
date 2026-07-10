using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Interfaces
{
    internal interface IFiltered<TKey, TValue>
    {
        public IEnumerable<TValue> FilterBy(TKey key);
    }
}
