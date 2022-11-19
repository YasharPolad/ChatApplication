using Slacker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.Application.Interfaces.RepositoryInterfaces;
public interface IGenericRepository<TEntity> where TEntity: Entity, new() //Abstract entity class can't be new'ed
{
    Task CreateAsync(TEntity entity);  
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);

}
