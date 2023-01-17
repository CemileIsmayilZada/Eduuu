using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("")] 
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            try
            {
                await _authService.RegisterAsync(register);
                return Ok("User Successfully Created");
            }
            catch (UserCreateFailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RoleCreateFailException )
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
           
        }
    }
}
