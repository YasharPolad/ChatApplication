using Microsoft.EntityFrameworkCore;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Infrastructure.Repositories;
public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : Entity, new()
    where TContext : DbContext
{
    protected readonly TContext _context;

    public GenericRepository(TContext context)
    {
        _context = context;
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        var entityToDelete = await GetAsync(e => e.Id == entity.Id);
        _context.Set<TEntity>().Remove(entityToDelete);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                         params Expression<Func<TEntity, object>>[] includes)
    {      
        return QueryBuilder(predicate, includes).ToList();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                                 params Expression<Func<TEntity, object>>[] includes)
    {
        var query = QueryBuilder(default, includes);
        return query.FirstOrDefault(predicate);
    }

    private IQueryable<TEntity> QueryBuilder(Expression<Func<TEntity, bool>> predicate = null,
                                             params Expression<Func<TEntity, object>>[] includes)
    {
        var query = predicate is null
                    ? _context.Set<TEntity>().AsQueryable()
                    : _context.Set<TEntity>().Where(predicate);

        return includes.Aggregate(query, (currentQuery, includeProperty) => currentQuery.Include(includeProperty));
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        var entityToUpdate = await GetAsync(e => e.Id == entity.Id);
        entityToUpdate = entity;
        _context.Set<TEntity>().Update(entityToUpdate);
        await _context.SaveChangesAsync();
    }
}
