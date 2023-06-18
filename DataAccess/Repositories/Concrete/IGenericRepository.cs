using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> GetById(int id);
        Task<IEnumerable<TModel>> GetAll();
        Task<bool> Add(TModel entity);
        Task<TModel> AddModel(TModel entity);
        bool Delete(TModel entity);
        bool Update(TModel entity);
        Task<TModel> UpdateModel(TModel entity);
        IQueryable<TModel> GetAll(bool tracking = true);

        IQueryable<TModel> GetWhere(Expression<Func<TModel, bool>> method, bool tracking = true);

        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> method, bool tracking = true);

        IQueryable<TModel> GetWhereWithInclude(Expression<Func<TModel, bool>> method, bool tracking = true, params Expression<Func<TModel, object>>[] include);

        IQueryable<TModel> GetAllWithInclude(bool tracking = true, params Expression<Func<TModel, object>>[] include);

    }
}
