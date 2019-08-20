using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProvaTecnicaApi.Service.Models.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, string[] includes = null);
        Task<T> Get(Expression<Func<T, bool>> expression, string[] includes = null);
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
    }
}
