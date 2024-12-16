using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.CacheHandlers
{
    public interface ICache
    {
        int GetCounterByType(CacheType cashType);
        void Increment(CacheType cashType);
    }
}
