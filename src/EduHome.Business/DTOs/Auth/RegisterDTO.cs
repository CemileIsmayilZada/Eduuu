using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.DTOs.Auth;

public class RegisterDTO
{
	public string Fullname { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}
