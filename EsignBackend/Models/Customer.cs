using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EsignBackend.Models
{
    public partial class Customer
    {      
        [Key]
        public int Id { get; set; }
        public string Idnumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone1 { get; set; }
        public string Mobile1 { get; set; }
        [Column(TypeName = "float")]
        public int? Securityquestion { get; set; }
        public virtual Securityquestion RelatedSecurityquestion { get; set; }
        public string Securityansware { get; set; }
        public string Certificates { get; set; }
        public string Temp { get; set; }
        public double? Calleruserid { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public virtual ICollection<Certificate> CustomerCertificates { get; set; }
        public virtual ICollection<Certificateshistory> CustomerHistoryCertificates { get; set; }

        public Customer() { }
        public Customer(CustomerDTO customer)
        {
            Id = customer.Id;
            Idnumber = customer.Idnumber;
            Firstname = customer.Firstname;
            Lastname = customer.Lastname;
            Phone1 = customer.Phone1;
            Mobile1 = customer.Mobile1;
            Securityquestion = customer.Securityquestion;

            Securityansware = customer.Securityansware; 
            Certificates = customer.Certificates;
            Temp = customer.Temp;
            Calleruserid = customer.Calleruserid;   
            Address = customer.Address;
            Email = customer.Email;
            Company = customer.Company;

            //TODO add isConfirmMarketingMailing parameter to customer , after alex update db table 
            //and than:
            //IsConfirmMarketingMailing = customer.IsConfirmMarketingMailing;


        }
    }
}