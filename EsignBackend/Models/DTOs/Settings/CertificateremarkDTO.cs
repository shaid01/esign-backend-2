using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs.Settings
{
    public class CertificateremarkDTO
    {
        public CertificateremarkDTO(Certificatermeark certificatermeark)
        {
            if (certificatermeark == null)
                return;
            Id = certificatermeark.Id;
            Title = certificatermeark.Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
