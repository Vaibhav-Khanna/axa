using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using AXA.DataStore.Abstraction;
using System.Reactive.Linq;

namespace AXA.DataStore.Implementation
{
    public class BaseStore<T> : IBaseStore<T> where T : BaseDataObject
    {
        protected IBlobCache Storage = BlobCache.UserAccount;
        
        public virtual Task<IEnumerable<T>> GetItemsAsync(bool refresh = false)
        {
            return null;
        }
    }
}
