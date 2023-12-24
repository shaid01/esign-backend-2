using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Dtos.Login
{
    public class LoginDto
    {
        [DefaultValue("michaelv")]
        public string UserName { get; set; }
        [DefaultValue("michaelv")]
        public string Password { get; set; }
    }
}
