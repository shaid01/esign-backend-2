using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateRemarks
{
    public interface ICertificateRemarksService
    {
        Task<ServiceResponse<int>> GetAmountOfCertificateRemarks();
        Task<ServiceResponse<List<Certificatermeark>>> GetCertificateRemarks(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificateRemarks(Certificatermeark updatedCertificateRemarks);
        Task<ServiceResponse<int>> AddNewCertificateRemarks(Certificatermeark certificateRemark);
    }
}
