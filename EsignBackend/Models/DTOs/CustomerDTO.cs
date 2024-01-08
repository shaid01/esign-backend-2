using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Models.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO(Customer customer)
        {
            if (customer == null)
                return;

            Id = customer.Id;
            Idnumber = customer.Idnumber;
            Firstname = customer.Firstname;
            Lastname = customer.Lastname;
            Phone1 = customer.Phone1;
            Mobile1 = customer.Mobile1;
            Securityquestion = customer.Securityquestion;
            RelatedSecurityquestion = new SecurityquestionDTO(customer.RelatedSecurityquestion);
            Securityansware = customer.Securityansware;
            Certificates = customer.Certificates;
            Temp = customer.Temp;
            Calleruserid = customer.Calleruserid;
            Address = customer.Address;
            Email = customer.Email;
            Company = customer.Company;
            ImpersonateAttempt = customer.ImpersonateAttempt;
        }
        public int Id { get; set; }
        public string Idnumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone1 { get; set; }
        public string Mobile1 { get; set; }
        public int? Securityquestion { get; set; }
        public SecurityquestionDTO RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Certificates { get; set; }
        public string Temp { get; set; }
        public double? Calleruserid { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public bool ImpersonateAttempt { get; set; }
    }
}
