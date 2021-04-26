using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateIssuer
{
    public interface ICertificateIssuerService
    {
        Task<ServiceResponse<List<Isscert>>> GetCertificateIssuers(int skip, int take);
        Task<ServiceResponse<int>> GetAmountOfCertificateIssuers();
        Task<ServiceResponse<int>> UpdateCertificateIssuer(Isscert updatedCertificateIssuer);
        Task<ServiceResponse<int>> AddNewCertificateIssuer(Isscert certificateIssuer);
        Task<ServiceResponse<List<Isscert>>> GetAllCertificateIssuers();
    }
}
