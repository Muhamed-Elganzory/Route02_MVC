using Route02.DAL.DB.Context;
using Route02.DAL.Models.Shared;
using Route02.DAL.Repositories.Interfaces;

namespace Route02.DAL.Repositories.Shared;

public class GenericRepository <TEntity> (ApplicationDbContext dbContext): IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    // Get all employees
    public IEnumerable<TEntity> GetAll (bool withTracking = false)
    {
        if (withTracking)
        {
            return _dbContext.Set<TEntity>().Where(emp => emp.IsDeleted != true).ToList();
        }
        
        return _dbContext.Set<TEntity>().Where(emp => emp.IsDeleted != true).AsNoTracking().ToList();
    }
    
    // Get employee by id
    public TEntity? GetById (int? id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    // Add employee
    public void Add (TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    // Update employee
    public void Update (TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    // Delete employee
    public void Delete (TEntity entity)
    {
        // Hard Delete
        // _dbContext.Set<TEntity>().Remove(entity);

        // Soft Delete
        entity.IsDeleted = true;
        _dbContext.Set<TEntity>().Update(entity);
    }
}