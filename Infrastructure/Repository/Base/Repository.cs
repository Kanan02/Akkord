using Application.Interfaces.IRepository.Base;
using Application.Models.Request.Base;
using Application.Spesifications.Base;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AkkordDbContext _context;

        public Repository(AkkordDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() =>  await _context.Set<T>().ToListAsync();
        
        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec,bool trackDb = true) =>  await ApplySpecification(spec, trackDb).ToListAsync();

        public async Task<int> CountAsync(ISpecification<T> spec) => await ApplySpecification(spec).CountAsync();
        
        private IQueryable<T> ApplySpecification(ISpecification<T> spec, bool trackDb = true) => SpecificationEvaluator<T>.GetQuery( trackDb ? _context.Set<T>().AsQueryable() 
            : _context.Set<T>().AsQueryable().AsNoTracking() , spec);
        
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(List<Expression<Func<T, bool>>> predicates = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicates != null) query = predicates.Aggregate(query, (current, include) => current.Where(include)); 

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync<TId>(TId id) => await _context.Set<T>().FindAsync(id);
        

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity) => _context.Update(entity);
        public void Update(T @new, T old)
        {
            _context.Entry(old).CurrentValues.SetValues(@new);
            Update(old);
        }

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public void RemoveAll(List<T> list) => _context.RemoveRange(list);

        public Task AddAllAsync(List<T> list) => _context.AddRangeAsync(list);

    }
}
