using EduHome.Business.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Interfaces
{
	public interface IAuthService
	{
		Task RegisterAsync(RegisterDTO registerDTO);
		Task LoginAsync(LoginDTO loginDTO);
	}
}
