using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Business.Validators.Auth;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        public readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            AppUser user = new()
            {
                Fullname = registerDTO.Fullname,
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };

            var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
            {
                string errors = string.Empty;
                int count = 0;
                foreach (var error in identityResult.Errors)
                {
                    errors += count != 0 ? $"{error.Description}" : $",{error.Description}";
                    count++;
                }
                throw new UserCreateFailException(errors);
            }

            var result = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                int count = 0;
                foreach (var error in result.Errors)
                {
                    errors += count != 0 ? $"{error.Description}" : $",{error.Description}";
                    count++;
                }
                throw new RoleCreateFailException(errors);
            }

        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user is null) throw new AuthFailException("Username or Password are invalid");

            var check = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!check) throw new AuthFailException("Username or Password are invalid");


            //Create JWT  issue-yaradan domain,audiance - gonderen domain, claims-payload olan hisse ,

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            SymmetricSecurityKey symmetricSecurity = new(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                _configuration["JWTSettings:Issue"],
                _configuration["JWTSettings:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(1),
                signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token=jwtSecurityTokenHandler.WriteToken(securityToken);

            return new()
            {
                Token = token,
                Expires = securityToken.ValidTo,
                UserName = user.UserName,
            };
        }
    }
}
