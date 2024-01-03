using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateRemarks
{
    public interface ICertificateRemarksService
    {
        Task<ServiceResponse<List<CertificateremarkDTO>>> GetCertificateRemarks(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificateRemarks(CertificaterMeark updatedCertificateRemarks);
        Task<ServiceResponse<int>> AddNewCertificateRemarks(CertificaterMeark certificateRemark);
    }
}
