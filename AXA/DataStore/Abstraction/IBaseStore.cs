using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AXA.DataStore.Abstraction
{
    public interface IBaseStore<T> where T : BaseDataObject
    {
        Task<IEnumerable<T>> GetItemsAsync(bool refresh = false);
    }
}
