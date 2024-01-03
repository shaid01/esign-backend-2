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
        Task<ServiceResponse<List<SecurityquestionDTO>>> GetSecurityQuestions(int skip, int take);
        Task<ServiceResponse<int>> UpdateSecurityQuestion(SecurityGuestion updatedSecurityQuestion);
        Task<ServiceResponse<int>> AddNewSecurityQuestion(SecurityGuestion securityQuestion);
        Task<ServiceResponse<List<SecurityquestionDTO>>> GetAllSecurityQuestions();
    }

}