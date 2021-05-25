using EsignBackend.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.CashHandlers
{

    public enum CashType
    {
        Certificate,
        Customers,
        Users
    }
    public class CashHandler : ICash
    {
        private static object _locker = new object();
        private IServiceScopeFactory _scopeFactory;
        private ILogger _logger;
        private Dictionary<CashType, int> _dbTableToCounterDictionary;

        public CashHandler(ILogger logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _dbTableToCounterDictionary = new Dictionary<CashType, int>();

        }

        public int GetCounterByType(CashType cashType)
        {
            lock (_locker)
            {
                if (!_dbTableToCounterDictionary.ContainsKey(cashType))
                {
                    ReadDataFromDB(cashType);
                }                    
            }
            return _dbTableToCounterDictionary[cashType];
        }

        public void Increment(CashType cashType)
        {
            lock (_locker)
            {
                IncrementTableCounter(cashType);
            }
        }

        private void IncrementTableCounter(CashType cashType)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dependencyService = scope.ServiceProvider.GetService<AppDbContext>();
                
                if (cashType == CashType.Certificate)
                {
                    _dbTableToCounterDictionary.Remove(cashType);
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Certificates.Count());
                }
            }
        }

        private void ReadDataFromDB(CashType cashType)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dependencyService = scope.ServiceProvider.GetService<AppDbContext>();
                
                    if (cashType == CashType.Certificate)
                    {
                        _dbTableToCounterDictionary.Add(cashType, dependencyService.Certificates.Count());
                    }                
            }
        }
    }
}
