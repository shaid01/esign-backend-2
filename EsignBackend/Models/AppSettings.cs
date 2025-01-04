using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models
{
    public class AppSettings
    {
        public string Token { get; set; }
        public string FrontURL { get; set; }
        public int SessionExpireMinuteTime { get; set; }

        public int SqlServerWaitTimeToExecuteCommand {  get; set; }

        public int MaxRecordsForExport { get; set; }
    }
}
