using EsignBackend.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.CashHandlers
{
    public class CertificateCashHandler
    {
        private static object _locker = new object();
        private static int _counter = 0;
        private static CertificateCashHandler _certificateCashHandler = null;
        private static readonly AppDbContext _context;
        private readonly ILogger _logger;


/*        public CustomersService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
*/


        private CertificateCashHandler() { }

        public int Counter { get => _counter;}

        public void Increment()
        {
            lock (_locker)
            {
                Interlocked.Increment(ref _counter);
            }            
        }

        public static CertificateCashHandler GetInstance()
        {
            if (_certificateCashHandler == null)
            {               
                _certificateCashHandler = new CertificateCashHandler();
               // _counter = _context.Certificates.Count();               
            }
            return _certificateCashHandler;
        }
    }
}
