using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projection.Shared.Data.Contexts;
using Projection.Shared.Data.Interfaces;
using Projection.Shared.Data.Specifications;
using Projection.Shared.Entities;
using System.Linq.Expressions;

namespace Projection.Shared.Data.Repositories;

/// <summary>
/// Represents a base repository implementation for entities that inherit from <see cref="BaseEntity{TKey}"/>, using Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
/// <typeparam name="TContext">The type of the database context.</typeparam>
public class BaseEntityEfRepository<TEntity, TKey, TContext>(TContext ctx, ILogger<BaseEntityEfRepository<TEntity, TKey, TContext>> logger) : IBaseEntityAsyncRepository<TEntity, TKey, TContext> where TEntity : BaseEntity<TKey> where TContext : BaseDbContext
{
    #region properties
    protected readonly TContext _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    private readonly ILogger<BaseEntityEfRepository<TEntity, TKey, TContext>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    #endregion

    #region interface implementation
    public IUnitOfWork UnitOfWork { get { return _ctx; } }

    /// <summary>
    /// Adds an entity asynchronously, saving changes based on the doSave parameter.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="doSave">Whether to save changes to the context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added entity.</returns>
    public async Task<TEntity> AddAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        if (doSave)
        {
            try
            {
                var result = await _ctx.Set<TEntity>().AddAsync(entity, cancellationToken);
                entity = result.Entity;
                await _ctx.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding entity: {typeof(TEntity).Name}");
            }
        }
        else
        {
            return _ctx.Set<TEntity>().Add(entity).Entity;
        }

        return entity;

    }


    /// <summary>
    /// !-- Bulk Insert --!
    /// Saves a list of entities to the database.
    /// </summary>
    /// <param name="entities">list of entities to add</param>
    /// <param name="doSave">Whether to save the changes to the database</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Count of entities saved to the database.</returns>
    public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool doSave = true, CancellationToken cancellationToken = default)
    {
        if (doSave)
        {
            try
            {
                //await _ctx.Set<TEntity>().AddRangeAsync(entities);
                //return await _ctx.SaveChangesAsync();

                await _ctx.BulkInsertAsync(entities, cancellationToken: cancellationToken);

                return entities.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding entities: {typeof(TEntity).Name}");
                return 0;
            }
        }
        else
        {
            await _ctx.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return 0;
        }

    }

    
    /// <summary>
    /// Counts the number of entities that match the specified criteria.
    /// </summary>
    /// <param name="specification">The specification that defines the criteria for the entities to count.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of entities that match the specified criteria.</returns>
    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).CountAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Marks an entity as inactive and updates it in the database.
    /// </summary>
    /// <param name="entity">The entity to be marked as inactive.</param>
    /// <param name="doSave">Whether to save the changes to the database.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        entity.IsActive = false;
        await this.UpdateAsync(entity, doSave);
    }

    /// <summary>
    /// Deletes an entity by its ID and optionally saves the changes to the database.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="doSave">Whether to save the changes to the database.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteByIdAsync(TKey id, bool doSave = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        await this.DeleteAsync(entity, doSave);
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The entity with the specified identifier, or null if no such entity exists.</returns>
    public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    /// <summary>
    /// Retrieves an entity that matches the specified criteria.
    /// </summary>
    /// <param name="specification">The specification that defines the criteria for the entity to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The entity that matches the specified criteria, or null if no such entity exists.</returns>
    public async Task<TEntity> GetByIdAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Checks if an entity with the specified predicate already exists in the database.
    /// </summary>
    /// <param name="entity">The entity to check for existence.</param>
    /// <param name="predicate">The predicate to use for checking the existence of the entity.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>True if an entity matching the specified predicate exists, false otherwise.</returns>
    public async Task<bool> IsExists(TEntity entity, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().AnyAsync(predicate, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Retrieves a list of all entities of the specified type.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of all entities of the specified type.</returns>
    public async Task<List<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _ctx.Set<TEntity>().ToListAsync();
    }


    /// <summary>
    /// Retrieves a list of all entities that match the specified criteria.
    /// </summary>
    /// <param name="specification">The specification that defines the criteria for the entities to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A list of all entities that match the specified criteria.</returns>
    public async Task<List<TEntity>> ListAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        try
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching entity: {ex.Message}");
            return [];
        }
    }


    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="doSave">If true, the changes will be saved to the database immediately. If false, the changes will be tracked but not saved.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The updated entity.</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity, bool doSave = true, CancellationToken cancellationToken = default)
    {
        if (doSave)
        {
            try
            {
                var result = _ctx.Entry<TEntity>(entity);
                result.State = EntityState.Modified;
                entity = result.Entity;
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating entity: {typeof(TEntity).Name}");
            }
        }
        else
        {
            return _ctx.Set<TEntity>().Update(entity).Entity;
        }

        return entity;
    }

    #endregion

    /// <summary>
    /// Applies the specified <see cref="ISpecification{TEntity}"/> to the <see cref="DbSet{TEntity}"/> and returns the resulting <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <param name="spec">The specification that defines the criteria for the entities to retrieve.</param>
    /// <returns>An <see cref="IQueryable{TEntity}"/> that represents the entities matching the specified criteria.</returns>
    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(_ctx.Set<TEntity>().AsQueryable(), spec);
    }
}
