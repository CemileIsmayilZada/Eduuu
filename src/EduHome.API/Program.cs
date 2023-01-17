//using EduHome.Business.Contexts;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Mappers;
using EduHome.Business.Services.Implementations;
using EduHome.Business.Services.Interfaces;
using EduHome.Business.Validators;
using EduHome.Core.Entities.Identity;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Implementations;
using EduHome.DataAccess.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var constr = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(constr);
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

//IoC
builder.Services.AddScoped<ICourseRepository, CourseRepository>();  //injectionda istifade olunur
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddAutoMapper(typeof(CourseMapper).Assembly);
//validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CoursePostDTOValidator).Assembly);
//builder.Services.AddScoped<IValidator<CoursePostDTO>, CoursePostDTOValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<CoursePostDTOValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var intializer=scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await intializer.InitializeAsync();
    await intializer.RoleSeedAsync();
    await intializer.UserSeedAsync();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
