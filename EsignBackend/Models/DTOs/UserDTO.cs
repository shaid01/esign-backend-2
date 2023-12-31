using System;
using static ClosedXML.Excel.XLPredefinedFormat;
using DateTime = System.DateTime;

#nullable disable

namespace EsignBackend.Models.DTOs
{
    public class UserDTO
    {
        public UserDTO(Buuser user, string? departmentTitle = null)
        {
            if (user == null)
                return;

            Id = user.Id;
            Username = user.Username;
            Userlevel = user.Userlevel;
            Usergroup = user.Usergroup;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Phone = user.Phone;
            Pass = user.Pass;
            Picture = user.Picture;
            Updateduserid = user.Updateduserid;
            Updateddate = user.Updateddate;
            Parentid = user.Parentid;
            Permissions = user.Permissions;
            Allowedips = user.Allowedips;
            Expires = user.Expires;
            Remarks = user.Remarks;
            Sessionvalues = user.Sessionvalues;
            Provider = user.Provider;
            Elang = user.Elang;
            Departmantid = user.Departmantid;
            if (departmentTitle != null)
            {
                DepartmantTitle = departmentTitle;
            }
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Userlevel { get; set; }
        public string Usergroup { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pass { get; set; }
        public string Picture { get; set; }
        public double? Updateduserid { get; set; }
        public DateTime? Updateddate { get; set; }
        public double? Parentid { get; set; }
        public string Permissions { get; set; }
        public string Allowedips { get; set; }
        public DateTime? Expires { get; set; }
        public string Remarks { get; set; }
        public string Sessionvalues { get; set; }
        public double? Provider { get; set; }
        public double? Elang { get; set; }
        public string Departmantid { get; set; }
        public string DepartmantTitle { get; set; }
    }
}
