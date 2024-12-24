using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateIssuer
{
    public interface ICertificateIssuerService
    {
        ServiceResponse<List<IsscertDTO>> GetCertificateIssuers(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificateIssuer(Isscert updatedCertificateIssuer);
        Task<ServiceResponse<int>> AddNewCertificateIssuer(Isscert certificateIssuer);
        ServiceResponse<List<IsscertDTO>> GetAllCertificateIssuers();
    }
}
