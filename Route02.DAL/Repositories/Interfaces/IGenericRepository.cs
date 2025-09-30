using Route02.DAL.Models.Shared;

namespace Route02.DAL.Repositories.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    // Get all employee 
    IEnumerable<T> GetAll (bool withTracking = false);
    
    //  Get By id
    T? GetById (int? id);
    
    // Add
    int Add (T entity);
    
    // Update
    int Update (T entity);
    
    // Delete
    int Delete (T entity);
}