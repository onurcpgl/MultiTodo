using DataAccess.Context;
using DataAccess.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repositories.Abstract
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly TodoDbContext _dbContext;
      
        public GenericRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<TModel> Table => _dbContext.Set<TModel>();
        public async Task<TModel> GetById(int id)
        {
            return await _dbContext.Set<TModel>().FindAsync(id);
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _dbContext.Set<TModel>().ToListAsync();
        }

        public async Task<bool> Add(TModel entity)
        {
            EntityEntry<TModel> entityEntry = await Table.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > -1;
        }

        public bool Delete(TModel entity)
        {
            Table.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(TModel entity)
        {
            EntityEntry<TModel> entityEntry = Table.Update(entity);
            _dbContext.SaveChanges();
            return entityEntry.State == EntityState.Modified;
        }

        public IQueryable<TModel> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }


        public async Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public IQueryable<TModel> GetWhereWithInclude(Expression<Func<TModel, bool>> method, bool tracking = true, params Expression<Func<TModel, object>>[] include)
        {
            IQueryable<TModel> query = Table;

            foreach (var item in include)
            {
                query = query.Include(item);
            }

            query = query.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public IQueryable<TModel> GetAllWithInclude(bool tracking = true, params Expression<Func<TModel, object>>[] include)
        {
            IQueryable<TModel> query = Table.AsQueryable();
            foreach (var item in include)
            {
                query = query.Include(item);
            }

            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

      
    }
}
