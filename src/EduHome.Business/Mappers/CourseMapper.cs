using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Mappers;

public class CourseMapper:Profile
{
	public CourseMapper()
	{
		CreateMap<Course, CourseDTO>().ReverseMap();
		CreateMap<Course, CoursePostDTO>().ReverseMap();
	}
}
