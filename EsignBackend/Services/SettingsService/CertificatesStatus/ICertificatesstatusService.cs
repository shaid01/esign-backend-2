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
        Task<ServiceResponse<List<CertificatesstatusDTO>>> GetCertificatesStatus(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificatesStatus(Models.CertificatesStatus updatedCertificatestatus);
        Task<ServiceResponse<int>> AddNewCertificatesStatus(Models.CertificatesStatus certificatestatus);
        Task<ServiceResponse<List<CertificatesstatusDTO>>> GetAllCertificatesStatus();
    }
}
