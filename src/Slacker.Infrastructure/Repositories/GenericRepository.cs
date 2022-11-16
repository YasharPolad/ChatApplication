using Microsoft.EntityFrameworkCore;
using Slacker.Application.Interfaces.RepositoryInterfaces;
using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public virtual async Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if(predicate == null)
            return await _context.Set<TEntity>().ToListAsync();
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        var entityToUpdate = await GetAsync(e => e.Id == entity.Id);
        entityToUpdate = entity;
        _context.Set<TEntity>().Update(entityToUpdate);
        await _context.SaveChangesAsync();
    }
}
