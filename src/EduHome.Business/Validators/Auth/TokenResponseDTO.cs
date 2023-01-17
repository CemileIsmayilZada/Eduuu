using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validators.Auth
{
    public class TokenResponseDTO
    {
        public string? Token { get; set; }  
        public DateTime Expires { get; set; }
        public string? UserName { get; set; }   
    }
}
