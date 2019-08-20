using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProvaTecnicaApi.Service.Data;
using ProvaTecnicaApi.Service.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProvaTecnicaApi.Service.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProvaTecnicaApiContext _context;
        private DbSet<T> _table;

        public GenericRepository(ProvaTecnicaApiContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task Delete(object id)
        {
            T exists = await _table.FindAsync(id);
            _table.Remove(exists);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> _query = _table;
            if (includes != null && includes.Count() > 0)
                foreach (var include in includes)
                    _query = _query.Include(include);

            return await _query.Where(expression).FirstOrDefaultAsync<T>();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, string[] includes = null)
        {
            IQueryable<T> _query = _table;
            if (expression != null)
                _query = _query.Where(expression);

            if (includes != null && includes.Count() > 0)
                foreach(var include in includes)
                    _query = _query.Include(include);

            return await _query.ToListAsync<T>();
        }


        public async Task Insert(T obj)
        {
            _table.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
