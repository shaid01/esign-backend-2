using System;

namespace EsignBackend.Models.Tools
{
    public class CertificateAdvancedSearch
    {
        public string? HpNumber { get; set; }
        public string? Company { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string? CustomerId { get; set; }
        public int? Project { get; set; }
        public int? SubProject { get; set; }
        public string CustomerIdNumber { get; set; }
        public int CertificateStatus { get; set; }
        public double CertificateIssuer { get; set; }
        public double CustomerIdentifier { get; set; }
        public DateTime? StartExpDate { get; set; }
        public DateTime? EndExpDate { get; set; }
        public DateTime? StartIssueDate { get; set; }
        public DateTime? EndIssueDate { get; set; }
    }
}
