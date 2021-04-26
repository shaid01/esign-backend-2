using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificatesStatus
{
    public interface ICertificatesstatusService
    {
        Task<ServiceResponse<List<Certificatesstatus>>> GetCertificatesStatus(int skip,int take);
        Task<ServiceResponse<int>> GetAmountOfCertificatesStatus();
        Task<ServiceResponse<int>> UpdateCertificatesStatus(Certificatesstatus updatedCertificatestatus);
        Task<ServiceResponse<int>> AddNewCertificatesStatus(Certificatesstatus certificatestatus);
        Task<ServiceResponse<List<Certificatesstatus>>> GetAllCertificatesStatus();
    }
}
