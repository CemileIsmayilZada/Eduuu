using EduHome.Business.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validators.Auth;

public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
{
	public RegisterDTOValidator()
	{
		
		RuleFor(u=>u.Username).NotNull().NotEmpty().MaximumLength(100);
		RuleFor(u=>u.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
		RuleFor(u=>u.Password).NotNull().NotEmpty().MaximumLength(100);
	}
}
