using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.MainServices.Certificates
{
    public interface ICertificatesService
    {

        Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCertificatesDetails(int skip, int take);
        Task<ServiceResponse<int>> UpdateCertificate(Certificate updatedCertificate);      
        Task<ServiceResponse<List<HistoryCertificateDTO>>> GetHistoryCertificates(double certificateId);
        Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCustomerCertificates(double customerId);        
        Task<ServiceResponse<IEnumerable<CertificateDetailsDTO>>> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take);
        Task<ServiceResponse<bool>> CheckSecurityAnswer(int cerId, string secAns,int question);
        Task<ServiceResponse<int>> AddCertificate(Certificate certificate);
        Task<ServiceResponse<int>> UpdateExpiredCertificates();
        byte[] GenerateXlsxFile(IEnumerable<CertificateDetailsDTO> certificateDetails);

    }
}
