using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SecurityQuestions
{
    public interface ISecurityQuestionsService
    {
        ServiceResponse<List<SecurityquestionDTO>> GetSecurityQuestions(int skip, int take);
        Task<ServiceResponse<int>> UpdateSecurityQuestion(Securityquestion updatedSecurityQuestion);
        Task<ServiceResponse<int>> AddNewSecurityQuestion(Securityquestion securityQuestion);
        ServiceResponse<List<SecurityquestionDTO>> GetAllSecurityQuestions();
    }

}