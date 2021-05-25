using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.CashHandlers
{
    public interface ICash
    {
        int GetCounterByType(CashType cashType);
        void Increment(CashType cashType);
    }
}
