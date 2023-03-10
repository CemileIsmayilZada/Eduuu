using EduHome.Business.DTOs.Auth;
using EduHome.Business.Validators.Auth;
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
        Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO);
	}
}
