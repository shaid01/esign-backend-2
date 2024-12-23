using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificatesStatus
{
    public interface ICertificatesstatusService
    {
        ServiceResponse<List<CertificatesstatusDTO>> GetCertificatesStatus(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificatesStatus(Certificatesstatus updatedCertificatestatus);
        Task<ServiceResponse<int>> AddNewCertificatesStatus(Certificatesstatus certificatestatus);
        ServiceResponse<List<CertificatesstatusDTO>> GetAllCertificatesStatus();
    }
}
