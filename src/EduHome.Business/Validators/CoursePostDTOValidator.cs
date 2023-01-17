using EduHome.Business.DTOs.Courses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validators;

public class CoursePostDTOValidator:AbstractValidator<CoursePostDTO>
{
	public CoursePostDTOValidator()
	{
		RuleFor(c => c.Name).NotEmpty().WithMessage("Name can not be empty").NotNull().WithMessage("Name can not be empty")
			.NotNull().MaximumLength(100);
		RuleFor(c=>c.Description).NotEmpty().WithMessage("Description can not be empty").
			MaximumLength(300);
		RuleFor(c => c.Image).NotNull().NotEmpty().WithMessage("Image can not be empty");
	}
}
