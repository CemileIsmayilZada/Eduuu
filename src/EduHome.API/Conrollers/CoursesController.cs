using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace EduHome.API.Conrollers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseService _courseService;
		private readonly ICourseRepository _courseRepository;

		public CoursesController(ICourseService courseService)
		{
			_courseService = courseService;
		}
		//[HttpGet,Route("GetCourses")]
		[HttpGet("")]
		public async Task<IActionResult> Get()
		{
			try
			{
				var courses = await _courseService.GetAllAsync();
				return Ok(courses);
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}

		}

		[HttpPost]

		public async Task<IActionResult> Post(CoursePostDTO coursePostDTO)
		{
			try
			{
				await _courseService.CreateAsync(coursePostDTO);
				//return Ok();
				//or
				return StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception)
			{

				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var course = await _courseService.FindById(id);
				return Ok(course);
			}
			//catch (FormatException)
			//{
			//	throw new NotFoundException("not found");
			//}
			catch (NotFoundException)
			{
				throw new NotFoundException("not found");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}

		}
		[HttpGet("getbyname")]
		public async Task<IActionResult> GetByCondition(string name)
		{

			try
			{
				var result=await _courseService.FindByCondition(c => c.Name.Contains(name));

				return Ok(result);
			}
			catch (Exception)
			{
				throw new NotFoundException("not found");
			}
		}
	}
}
