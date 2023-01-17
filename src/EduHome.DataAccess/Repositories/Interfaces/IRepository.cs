using EduHome.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T:class,IEntity,new()
{
	IQueryable<T> FindAll();
	IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
	T FinddById(int id);
	Task Create(T entity);
	void Update(T entity);
	void Delete(T entity);
	Task Save();
	DbSet<T> Table { get; }
}

