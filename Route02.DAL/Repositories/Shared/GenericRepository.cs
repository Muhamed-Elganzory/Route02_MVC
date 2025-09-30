using Route02.DAL.DB.Context;
using Route02.DAL.Models.Shared;
using Route02.DAL.Repositories.Interfaces;

namespace Route02.DAL.Repositories.Shared;

public class GenericRepository <TEntity> (ApplicationDbContext dbContext): IGenericRepository<TEntity> where TEntity : BaseEntity
{
    // Get all employees
    public IEnumerable<TEntity> GetAll (bool withTracking = false)
    {
        if (withTracking)
        {
            return dbContext.Set<TEntity>().ToList();
        }
        
        return dbContext.Set<TEntity>().AsNoTracking().ToList();
    }
    
    // Get employee by id
    public TEntity? GetById (int? id)
    {
        return dbContext.Set<TEntity>().Find(id);
    }

    // Add employee
    public int Add (TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
        return dbContext.SaveChanges();
    }

    // Update employee
    public int Update (TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        return dbContext.SaveChanges();
    }

    // Delete employee
    public int Delete (TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        return dbContext.SaveChanges();
    }
}