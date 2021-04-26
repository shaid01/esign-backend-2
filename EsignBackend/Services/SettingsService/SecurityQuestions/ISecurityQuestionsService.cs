using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SecurityQuestions
{
    public interface ISecurityQuestionsService
    {
        Task<ServiceResponse<int>> GetAmountOfSecurityQuestions();
        Task<ServiceResponse<List<Securityquestion>>> GetSecurityQuestions(int skip, int take);
        Task<ServiceResponse<int>> UpdateSecurityQuestion(Securityquestion updatedSecurityQuestion);
        Task<ServiceResponse<int>> AddNewSecurityQuestion(Securityquestion securityQuestion);
        Task<ServiceResponse<List<Securityquestion>>> GetAllSecurityQuestions();
    }
}