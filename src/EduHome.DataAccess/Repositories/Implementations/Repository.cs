using EduHome.Core.Entities;
using EduHome.Core.Interfaces;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Implementations
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		private readonly AppDbContext _context;
		//private readonly DbSet<T> _table;

		public Repository(AppDbContext context/*, DbSet<T> table*/)
		{
			_context = context;
			//_table = table;
		}
		public DbSet<T> Table => _context.Set<T>();

		public IQueryable<T> FindAll()
		{
			return Table.AsQueryable();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return Table.Where(expression).AsNoTracking();
		}

		public T FinddById(int id)
		{
			return Table.Find(id);
		}
		public async Task Create(T entity)
		{
		await Table.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			Table.Remove(entity);
		}

	
		public void Update(T entity)
		{
			Table.Update(entity);
		}

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}
	}
}
