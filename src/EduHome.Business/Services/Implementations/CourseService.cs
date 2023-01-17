using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Implementations;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly IMapper _mapper;

		public CourseService(ICourseRepository courseRepository, IMapper mapper)
		{
			_courseRepository = courseRepository;
			_mapper = mapper;
		}

	

		public async Task<CourseDTO> FindById(int id)
		{
			var course = _courseRepository.FinddById(id);
			if(course == null)
			{
				throw new NotFoundException("not found!");
			}
			var result=_mapper.Map<CourseDTO>(course);
			return result;
		}

		public async Task<List<CourseDTO>> GetAllAsync()
		{
			var courses = _courseRepository.FindAll().ToList();
			var result = _mapper.Map<List<CourseDTO>>(courses);
			//if (courses == null || courses.Count == 0) throw new NotFoundException("Not found!");
			return result;
		}

		public async Task<List<Course>> FindByCondition(Expression<Func<Course, bool>> expression)
		{
			var courses= await _courseRepository.FindByCondition(expression).ToListAsync();
			var result=_mapper.Map<List<Course>>(courses);
			return result;
		}
		public async Task CreateAsync(CoursePostDTO entity)
		{
			var result=_mapper.Map<Course>(entity);
			await _courseRepository.Create(result);
			await _courseRepository.Save();
		}

		public void Update(Course entity)
		{
			_courseRepository.Update(entity);
		}

		public void Delete(Course entity)
		{
			_courseRepository.Delete(entity);
		}




	}
}
