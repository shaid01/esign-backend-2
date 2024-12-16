using EsignBackend.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.CacheHandlers
{
    public enum CacheType
    {
        Certificate,
        Customers,
        Users
    }
    public class CacheHandler : ICache
    {
        private static object _locker = new object();
        private IServiceScopeFactory _scopeFactory;
        private ILogger _logger;
        private Dictionary<CacheType, int> _dbTableToCounterDictionary;

        public CacheHandler(ILogger logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _dbTableToCounterDictionary = new Dictionary<CacheType, int>();
        }

        private void incrementTableCounter(CacheType cashType)
        {
            _logger.Debug("CacheHandler ~ IncrementTableCounter: " + cashType);
            using (var scope = _scopeFactory.CreateScope())
            {
                var dependencyService = scope.ServiceProvider.GetService<AppDbContext>();
                _dbTableToCounterDictionary.Remove(cashType);

                if (cashType == CacheType.Certificate)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Certificates.Count());
                }
                else if (cashType == CacheType.Customers)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Customers.Count());
                }
                else if (cashType == CacheType.Users)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Buusers.Count());
                }
            }
        }

        private void readDataFromDB(CacheType cashType)
        {
            _logger.Debug("CacheHandler ~ ReadDataFromDB: " + cashType);
            using (var scope = _scopeFactory.CreateScope())
            {
                var dependencyService = scope.ServiceProvider.GetService<AppDbContext>();

                if (cashType == CacheType.Certificate)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Certificates.Count());
                }
                else if (cashType == CacheType.Customers)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Customers.Count());
                }
                else if (cashType == CacheType.Users)
                {
                    _dbTableToCounterDictionary.Add(cashType, dependencyService.Buusers.Count());
                }
            }
        }

        // responsibility of every client
        //private void readSearchDataCount(CacheType cashType)
        //{
        //}

        public int GetCounterByType(CacheType cashType)
        {
            _logger.Debug("CacheHandler ~ GetCounterByType: " + cashType);
            lock (_locker)
            {
                if (!_dbTableToCounterDictionary.ContainsKey(cashType))
                {
                    readDataFromDB(cashType);
                }
            }
            return _dbTableToCounterDictionary[cashType];
        }

        public void Increment(CacheType cashType)
        {
            _logger.Debug("CacheHandler ~ Increment: " + cashType);
            lock (_locker)
            {
                incrementTableCounter(cashType);
            }
        }
    }
}
